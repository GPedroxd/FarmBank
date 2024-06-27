using FarmBank.Core.Member;
using FarmBank.Core.Transaction;
using FarmBank.Integration.Mongo;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace FarmBank.Integration.Database;

public class MongoContext : ContextBase
{
    public MongoContext([NotNull] string connectionString, [NotNull] string databaseName) : base(connectionString, databaseName)
    {   
        if(!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
            BsonClassMap.RegisterClassMap<Transaction>(cm => {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
                cm.SetIgnoreExtraElements(true);
            });

        if(!BsonClassMap.IsClassMapRegistered(typeof(Member)))
            BsonClassMap.RegisterClassMap<Member>(cm => {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
                cm.SetIgnoreExtraElements(true);
            });    
    }

    public DbSet<Transaction> Transaction { get => GetDbSet<Transaction>("transactions"); }

    public DbSet<Member> Member { get => GetDbSet<Member>("members"); }
}
