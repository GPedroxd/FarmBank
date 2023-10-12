using System.Diagnostics.CodeAnalysis;
using FarmBank.Application.Models;
using FarmBank.Integration.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FarmBank.Integration.Database;

public class MongoContext : ContextBase
{
    public MongoContext([NotNull] string connectionString, [NotNull] string databaseName) : base(connectionString, databaseName)
    {   
        if(!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
            BsonClassMap.RegisterClassMap<Transaction>(cm => {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
            });
    }

    public DbSet<Transaction> Transaction { get => GetDbSet<Transaction>("transactions"); }
}
