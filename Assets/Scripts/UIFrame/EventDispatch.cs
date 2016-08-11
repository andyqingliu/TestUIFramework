using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/***
 * EventDispatch.cs
 *
 * @author LiuQing
 */
namespace Xsjm
{
    public class EventDispatch
    {
        private Dictionary<string, Delegate> events;

        protected Dictionary<string, Delegate> Events
        {
            get
            {
                if (null == events)
                {
                    events = new Dictionary<string, Delegate>();
                }
                return events;
            }
        }

        public void AddEvent(string evtName, Action evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action action = del as Action;
                if (null != action)
                {
                    action += evt;
                    events[evtName] = action;
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
            else
            {
                events.Add(evtName,evt);
            }
        }

        public void RemoveEvent(string evtName, Action evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action action = del as Action;
                if (null != action)
                {
                    action -= evt;
                    if (null == action)
                    {
                        events.Remove(evtName);
                    }
                    else
                    {
                        events[evtName] = action;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
        }

        public void AddEvent<T>(string evtName, Action<T> evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action<T> action = del as Action<T>;
                if (null != action)
                {
                    action += evt;
                    events[evtName] = action;
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
        }

        public void RemoveEvent<T>(string evtName, Action<T> evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action<T> action = del as Action<T>;
                if (null != action)
                {
                    action -= evt;
                    if (null == action)
                    {
                        events.Remove(evtName);
                    }
                    else
                    {
                        events[evtName] = action;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
        }

        public void AddEvent<T, V>(string evtName, Action<T, V> evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action<T, V> action = del as Action<T, V>;
                if (null != action)
                {
                    action += evt;
                    events[evtName] = action;
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
            else
            {
                events.Add(evtName, evt);
            }
        }

        public void RemoveEvent<T, V>(string evtName, Action<T, V> evt)
        {
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Action<T, V> action = del as Action<T, V>;
                if (null != action)
                {
                    action -= evt;
                    if (null == action)
                    {
                        events.Remove(evtName);
                    }
                    else
                    {
                        events[evtName] = action;
                    }
                }
                else
                {
                    throw new Exception("EventDispatch event type is null");
                }
            }
        }

        public void TriggerEvent(string evtName)
        {
            if (null == events)
            {
                return;
            }
            Delegate del;
            if (Events.TryGetValue(evtName,out del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action action = dels[i] as Action;
                    if (null != action)
                    {
                        action();
                    }
                }
            }
        }

        public void TriggerEvent<T>(string evtName, T t)
        {
            if (null == events)
            {
                return;
            }
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action<T> action = dels[i] as Action<T>;
                    if (null != action)
                    {
                        action(t);
                    }
                }
            }
        }

        public void TriggerEvent<T, V>(string evtName, T t, V v)
        {
            if (null == events)
            {
                return;
            }
            Delegate del;
            if (Events.TryGetValue(evtName, out del))
            {
                Delegate[] dels = del.GetInvocationList();
                for (int i = 0; i < dels.Length; i++)
                {
                    Action<T, V> action = dels[i] as Action<T, V>;
                    if (null != action)
                    {
                        action(t, v);
                    }
                }
            }
        }
    }
}
