using FarmBank.Core.Event;
using FluentAssertions;

namespace FarmBank.Core.Tests.Event;

public class EventTests
{
    [Fact]
    public void CreateANewEventGeneratesAEventCreatedDomainEvent()
    {
        var @event = new Core.Event.Event("test", DateTime.Now, DateTime.Now.AddDays(1));

        @event.Events.Count.Should().Be(1);
        @event.Events.First().Should().BeOfType<Core.Event.Events.EventCreatedEvent>();
    }

    [Fact]
    public void DepositShouldHappenWhenValidInput()
    {
        var @event = new Core.Event.Event("test", DateTime.Now, DateTime.Now.AddDays(1));
        var deposit = new Deposit("12345", 1, Guid.Empty, string.Empty);
        
        @event.Deposit(deposit);

        @event.Deposits.Count.Should().Be(1);
        @event.Events.Count.Should().Be(2);
        @event.Events.Last().Should().BeOfType<Core.Event.Events.DepositMadeEvent>();
    }

    [Fact]
    public void DepositShouldNotHappenWhenEventIsNotActive()
    {
        var @event = new Core.Event.Event("test", DateTime.Now, DateTime.Now.AddDays(1));
        @event.deactivate();
        var deposit = new Deposit(string.Empty, 0, Guid.Empty, string.Empty);

        @event.Deposit(deposit);

        @event.Deposits.Count.Should().Be(0);
        @event.Events.Count.Should().Be(1);
    }

    [Fact]
    public void DepositShouldNotHappenToTheSameTransactionId()
    {
        var @event = new Core.Event.Event("test", DateTime.Now, DateTime.Now.AddDays(1));
        var deposit = new Deposit("12345", 1, Guid.Empty, string.Empty);
        @event.Deposit(deposit);

        @event.Deposit(deposit);

        @event.Deposits.Count.Should().Be(1);
        @event.Events.Count.Should().Be(2);
    }

    
}
