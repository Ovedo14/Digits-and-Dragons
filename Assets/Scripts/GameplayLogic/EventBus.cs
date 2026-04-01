using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, Delegate> _handlers = new();

    public static void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
            _handlers[type] = Delegate.Combine(_handlers[type], handler);
        else
            _handlers[type] = handler;
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
            _handlers[type] = Delegate.Remove(_handlers[type], handler);
    }

    public static void Publish<T>(T evt)
    {
        if (_handlers.TryGetValue(typeof(T), out var handler))
            (handler as Action<T>)?.Invoke(evt);
    }
}