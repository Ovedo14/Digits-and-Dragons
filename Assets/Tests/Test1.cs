//Test to verify the EventBus is handling publishing and subscribing/unsubscribing correctly

using NUnit.Framework;
using UnityEngine;

public class EventBusTests
{
    private bool _eventReceived;

    [SetUp]
    public void Setup()
    {
        _eventReceived = false;
    }

    [Test]
    public void EventBus_PublishAndSubscribe_EventIsReceived()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTestEvent);
        EventBus.Publish(new OnTurnStarted());

        Assert.IsTrue(_eventReceived, "event was not received");
        EventBus.Unsubscribe<OnTurnStarted>(HandleTestEvent);
    }

    [Test]
    public void EventBus_Unsubscribe_EventNotReceived()
    {
        EventBus.Subscribe<OnTurnStarted>(HandleTestEvent);
        EventBus.Unsubscribe<OnTurnStarted>(HandleTestEvent);

        EventBus.Publish(new OnTurnStarted());
        Assert.IsFalse(_eventReceived, "event should not receive after unsubscribing");
    }

    private void HandleTestEvent(OnTurnStarted evt)
    {
        _eventReceived = true;
    }
}