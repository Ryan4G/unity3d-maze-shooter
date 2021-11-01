using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messenger
{
    protected static Dictionary<string, List<Action>> eventsDic = new Dictionary<string, List<Action>>();

    public static void AddListener(string gameEvent, Action action)
    {
        if (!eventsDic.ContainsKey(gameEvent))
        {
            List<Action> actions = new List<Action>();
            actions.Add(action);
            eventsDic.Add(gameEvent, actions);
        }
        else
        {
            List<Action> actions = eventsDic[gameEvent];

            if (!actions.Contains(action))
            {
                actions.Add(action);
            }
        }
    }

    public static void RemoveListener(string gameEvent, Action action)
    {
        if (eventsDic.ContainsKey(gameEvent))
        {
            List<Action> actions = eventsDic[gameEvent];

            if (actions.Contains(action))
            {
                actions.Remove(action);
            }

            if (actions.Count == 0)
            {
                eventsDic.Remove(gameEvent);
            }
        }
    }

    public static void Boardcast(string gameEvent)
    {
        if (eventsDic.ContainsKey(gameEvent))
        {
            List<Action> actions = eventsDic[gameEvent];

            foreach(Action a in actions)
            {
                a.Invoke();
            }
        }
    }
}

public class Messenger<T>
{
    protected static Dictionary<string, List<Action<T>>> eventsDic = new Dictionary<string, List<Action<T>>>();

    public static void AddListener(string gameEvent, Action<T> action)
    {
        if (!eventsDic.ContainsKey(gameEvent))
        {
            List<Action<T>> actions = new List<Action<T>>();
            actions.Add(action);
            eventsDic.Add(gameEvent, actions);
        }
        else
        {
            List<Action<T>> actions = eventsDic[gameEvent];

            if (!actions.Contains(action))
            {
                actions.Add(action);
            }
        }
    }

    public static void RemoveListener(string gameEvent, Action<T> action)
    {
        if (eventsDic.ContainsKey(gameEvent))
        {
            List<Action<T>> actions = eventsDic[gameEvent];

            if (actions.Contains(action))
            {
                actions.Remove(action);
            }

            if (actions.Count == 0)
            {
                eventsDic.Remove(gameEvent);
            }
        }
    }

    public static void Boardcast(string gameEvent, T value)
    {
        if (eventsDic.ContainsKey(gameEvent))
        {
            List<Action<T>> actions = eventsDic[gameEvent];

            foreach (Action<T> a in actions)
            {
                a.Invoke(value);
            }
        }
    }
}
