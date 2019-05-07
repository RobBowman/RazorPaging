# Persistence Layer

This layer contains classes for accessing data sources such as SQL, InMemory, SQLLite, Azure Cosmos DB, and so on.
It will also contain concrete implementations of the repositories required by the application layer.
References to ORM Framework(s) (Entity Framework Core, Dapper, NHibernate, and so on) should be isolated to this project.
