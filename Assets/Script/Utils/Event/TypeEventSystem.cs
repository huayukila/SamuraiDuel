using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ITypeEventSystem
{
    void Send<T>() where T : new();
    void Send<T>(T e);

    IUnregister Register<T>(Action<T> onEvent);
    void Unregister<T>(Action<T> onEvent);
}
public interface IUnregister
{
    void Unregister();
}
public struct TypeEventSystemUnregister<T> : IUnregister
{
    public ITypeEventSystem TypeEventSystem;
    public Action<T> OnEvent;
    public void Unregister()
    {
        TypeEventSystem.Unregister<T>(OnEvent);
        TypeEventSystem = null;
        OnEvent = null;
    }
}
/// <summary>
/// MonoBehaviour�̐��������ɂ���āA�����I�Ɏ�������
/// </summary>
public class UnregisterOnDestroyTrigger : MonoBehaviour
{
    private HashSet<IUnregister> mUnregistered = new HashSet<IUnregister>();

    public void AddUnregister(IUnregister unregister)
    {
        mUnregistered.Add(unregister);
    }

    private void OnDestroy()
    {
        foreach (var unRegister in mUnregistered)
        {
            unRegister.Unregister();
        }

        mUnregistered.Clear();
    }
}
/// <summary>
/// ���������ÓI�G�N�X�e���V����
/// </summary>
public static class UnregisterExtension
{
    public static void UnregisterWhenGameObjectDestroyed(this IUnregister unRegister, GameObject gameObject)
    {
        gameObject.GetOrAddComponent<UnregisterOnDestroyTrigger>().AddUnregister(unRegister);
    }
}
public class TypeEventSystem :ITypeEventSystem
{
    public interface IRegistrations
    {

    }
    public class Registrations<T> : IRegistrations
    {
        public Action<T> OnEvent = e => { };
    }
    Dictionary<Type, IRegistrations> mEventRegistration = new Dictionary<Type, IRegistrations>();
    public IUnregister Register<TEvent>(Action<TEvent> onEvent)
    {
        var type = typeof(TEvent);
        IRegistrations registrations;

        if (mEventRegistration.TryGetValue(type, out registrations))
        {

        }
        else
        {
            registrations = new Registrations<TEvent>();
            mEventRegistration.Add(type, registrations);
        }
        (registrations as Registrations<TEvent>).OnEvent += onEvent;
        return new TypeEventSystemUnregister<TEvent>()
        {
            OnEvent = onEvent,
            TypeEventSystem = this
        };
    }
    public void Send<T>() where T : new()
    {
        var e = new T();
        Send<T>(e);
    }
    public void Send<T>(T e)
    {
        var type = typeof(T);
        IRegistrations registrations;

        if (mEventRegistration.TryGetValue(type, out registrations))
        {
            (registrations as Registrations<T>).OnEvent(e);
        }
    }
    public void Unregister<T>(Action<T> onEvent)
    {
        var type = typeof(T);
        IRegistrations registrations;
        if (mEventRegistration.TryGetValue(type, out registrations))
        {
            (registrations as Registrations<T>).OnEvent -= onEvent;
        }
    }
}