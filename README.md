# Marten eventstore proof of concepts

Run solution
1. Run postgress in docker 
```
$ docker run --name event-store-postgres -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres
```
2. Build and run Marten.ProofOfConcept.sln


Resources
* [Marten event store](http://jasperfx.github.io/marten/)
* [Mediatr In-process messaging](https://github.com/jbogard/MediatR)
