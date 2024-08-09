using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventEnum, List<UnityAction>> voidListeners = new Dictionary<EventEnum, List<UnityAction>>(); 
    static Dictionary<EventEnum, List<Delegate>> oneParameterListeners = new Dictionary<EventEnum, List<Delegate>>(); 
    

    public static void AddListener(EventEnum eventEnum, UnityAction listener)
    {
        if (!voidListeners.ContainsKey(eventEnum)) voidListeners.Add(eventEnum, new List<UnityAction>());
        voidListeners[eventEnum].Add(listener);
    }
    public static void RemoveListener(EventEnum eventEnum, UnityAction listener) 
    {
        if (voidListeners.ContainsKey(eventEnum) && voidListeners[eventEnum].Contains(listener))
        {
            voidListeners[eventEnum].Remove(listener);
        }
    }
    public static void InvokeEvent(EventEnum eventEnum)
    {
        if (!voidListeners.ContainsKey(eventEnum)) return;
        foreach (UnityAction action in voidListeners[eventEnum])
        {
            action?.Invoke();
        }
    }

    public static void AddListener<T>(EventEnum eventEnum, Action<T> listener)
    {
        if (!oneParameterListeners.ContainsKey(eventEnum)) oneParameterListeners.Add(eventEnum, new List<Delegate>());
        oneParameterListeners[eventEnum].Add(listener);
    }
    public static void RemoveListener<T>(EventEnum eventEnum, Action<T> listener)
    {
        if (oneParameterListeners.ContainsKey(eventEnum) && oneParameterListeners[eventEnum].Contains(listener))
        {
            oneParameterListeners[eventEnum].Remove(listener);
        }
    }
    public static void InvokeEvent<T>(EventEnum eventEnum, T eventParameter)
    {
        foreach (Delegate action in oneParameterListeners[eventEnum])
        {
            (action as Action<T>)?.Invoke(eventParameter);
        }

        InvokeEvent(eventEnum);
    }
}
