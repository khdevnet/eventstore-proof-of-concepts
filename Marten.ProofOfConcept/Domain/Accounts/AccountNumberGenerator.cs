﻿using System;

namespace Marten.ProofOfConcept.Domain.Accounts
{
    public interface IAccountNumberGenerator
    {
        string Generate();
    }

    public class RandomAccountNumberGenerator : IAccountNumberGenerator
    {
        Random _random = new Random();

        public string Generate()
        {
            return string.Format("{0:00} {0:00000} {0:00000} {0:00000} {0:00000} {0:00000} {0:00000}",
                _random.Next(99), _random.Next(9999), _random.Next(9999), _random.Next(9999), _random.Next(9999), _random.Next(9999), _random.Next(9999));
        }
    }
}
