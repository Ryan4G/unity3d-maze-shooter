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

public class Messenger<T> : Messenger
{

}
