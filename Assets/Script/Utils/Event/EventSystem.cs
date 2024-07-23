using System;

public static class EventSystem
{
    private static TypeEventSystem instance = new TypeEventSystem();

    public static IUnregister Register<TEvent>(Action<TEvent> onEvent)
    {
        return instance.Register<TEvent>(onEvent);
    }

    public static void Send<T>() where T : new()
    {
        var e = new T();
        Send<T>(e);
    }

    public static void Send<T>(T e)
    {
        instance.Send<T>(e);
    }
}