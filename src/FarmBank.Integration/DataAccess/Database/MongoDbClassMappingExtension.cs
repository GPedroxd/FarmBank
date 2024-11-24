using FarmBank.Core.Base;
using FarmBank.Core.Event;
using FarmBank.Core.Member;
using FarmBank.Core.Transaction;
using FarmBank.Integration.DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FarmBank.Integration.DataAccess.Database;

public static class MongoDbClassMappingExtension
{
    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

        if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot)))
            BsonClassMap.RegisterClassMap<AggregateRoot>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

        if (!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
            BsonClassMap.RegisterClassMap<Transaction>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

        if (!BsonClassMap.IsClassMapRegistered(typeof(Member)))
            BsonClassMap.RegisterClassMap<Member>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

        if (!BsonClassMap.IsClassMapRegistered(typeof(Event)))
            BsonClassMap.RegisterClassMap<Event>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapProperty(el => el.Deposits);
                cm.MapProperty(el => el.TotalDeposited)
                    .SetSerializer(new DecimalSerializer(BsonType.Decimal128));

            });

        var iConfig = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddSingleton(
            _ => new MongoContext(iConfig["MongoDbConnectionString"], iConfig["DatabaseName"])
        );

        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IMemberRepository, MemberRepository>();
        services.AddTransient<IEventRepository, EventRepository>();
        
        return services;
    }
}
