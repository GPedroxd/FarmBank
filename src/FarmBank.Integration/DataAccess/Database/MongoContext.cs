using FarmBank.Core.Base;
using FarmBank.Core.Event;
using FarmBank.Core.Member;
using FarmBank.Core.Transaction;
using FarmBank.Integration.DataAccess.Mongo;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace FarmBank.Integration.DataAccess.Database;

public class MongoContext : ContextBase
{
    public MongoContext([NotNull] string connectionString, [NotNull] string databaseName) : base(connectionString, databaseName)
    { }

    public DbSet<Transaction> Transaction { get => GetDbSet<Transaction>("transactions"); }

    public DbSet<Member> Member { get => GetDbSet<Member>("members"); }

    public DbSet<Event> Event { get => GetDbSet<Event>("events"); }
}
