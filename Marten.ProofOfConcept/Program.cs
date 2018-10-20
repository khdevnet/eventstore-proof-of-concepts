using System;
using System.Linq;
using System.Threading;
using Marten.ProofOfConcept.Domain.Accounts;
using Marten.ProofOfConcept.Domain.Accounts.Events;
using Marten.ProofOfConcept.Domain.Accounts.Transactions.Commands;
using Marten.ProofOfConcept.Domain.Accounts.Transactions.Events;
using Marten.ProofOfConcept.Domain.Accounts.ValueObjects;
using Marten.ProofOfConcept.Views.Accounts.AccountSummary;
using Newtonsoft.Json;

namespace Marten.ProofOfConcept
{
    class Program
    {

        private static string connectionString = "PORT = 5432; HOST = localhost; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'postgres'; PASSWORD = '123456'; USER ID = 'postgres'";
        private static string schemaName = "public";

        static void Main(string[] args)
        {

            var documentStore = DocumentStore.For(options =>
            {

                options.Connection(connectionString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
                options.Events.DatabaseSchemaName = schemaName;
                options.DatabaseSchemaName = schemaName;

                options.Events.InlineProjections.AggregateStreamsWith<Account>();
                //options.Events.InlineProjections.Add(new AllAccountsSummaryViewProjection());
                options.Events.InlineProjections.Add(new AccountSummaryViewProjection());
                //options.Events.InlineProjections.Add(new ClientsViewProjection());

                options.Events.AddEventType(typeof(NewAccountCreated));
                options.Events.AddEventType(typeof(NewInflowRecorded));
                options.Events.AddEventType(typeof(NewOutflowRecorded));

                //options.Events.AddEventType(typeof(ClientCreated));
                //options.Events.AddEventType(typeof(ClientUpdated));
                //options.Events.AddEventType(typeof(ClientDeleted));
            });

            // CreateClientAccount(documentStore);

            var mainAccountId = Guid.Parse("4293c6f7-ad24-47f1-8650-3a7ab6fc085d");
            var secondAccountId = Guid.Parse("8ea4bc68-e9cf-41b4-aa3a-c63a4e63b674");

            MakeTransfer(documentStore, new MakeTransfer(10, mainAccountId, secondAccountId));
            Thread.Sleep(5000);


            var mainAccountAccountSummury = ReadAccount(documentStore, mainAccountId);
            DisplayAccountSummary("Main Account summury!", mainAccountAccountSummury);

            var targetAccountSummury = ReadAccount(documentStore, secondAccountId);
            DisplayAccountSummary("Target Account summury!", targetAccountSummury);

            Console.WriteLine("Read from store done!");

            Console.ReadLine();
        }

        private static void DisplayAccountSummary(string header, AccountSummary accountSummary)
        {
            Console.WriteLine(header);
            Console.WriteLine(JsonConvert.SerializeObject(accountSummary));
        }

        private static AccountSummary ReadAccount(DocumentStore documentStore, Guid accountId)
        {
            using (var session = documentStore.OpenSession())
            {
                var account = session
                   .Query<AccountSummaryView>()
                   .Select(a => new AccountSummary
                   {
                       AccountId = a.AccountId,
                       Balance = a.Balance,
                       ClientId = a.ClientId,
                       Number = a.Number,
                       TransactionsCount = a.TransactionsCount
                   })
                   .FirstOrDefault(p => p.AccountId == accountId);

                return account;

            }
        }

        private static void CreateClientAccount(DocumentStore documentStore)
        {
            var clientId = Guid.NewGuid();

            using (var session = documentStore.OpenSession())
            {
                var account = new Account(clientId, new RandomAccountNumberGenerator());

                session.Events.Append(account.Id, account.PendingEvents.ToArray());
                session.SaveChanges();
                Console.WriteLine("Append to store done!");
            }
        }

        private static void MakeTransfer(DocumentStore documentStore, MakeTransfer moneyTransfer)
        {
            using (var session = documentStore.OpenSession())
            {
                var accountFrom = session.Events.AggregateStream<Account>(moneyTransfer.FromAccountId);

                accountFrom.RecordOutflow(moneyTransfer.ToAccountId, moneyTransfer.Ammount);
                session.Events.Append(accountFrom.Id, accountFrom.PendingEvents.ToArray());

                var accountTo = session.Events.AggregateStream<Account>(moneyTransfer.ToAccountId);

                accountTo.RecordInflow(moneyTransfer.FromAccountId, moneyTransfer.Ammount);
                session.Events.Append(accountTo.Id, accountTo.PendingEvents.ToArray());

                session.SaveChanges();
            }
        }
    }
}
