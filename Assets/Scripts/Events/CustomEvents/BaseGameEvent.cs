using System.Collections.Generic;
using _Scripts.Events.Listeners;
using UnityEngine;

namespace _Scripts.Events.CustomEvents
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _eventListeners = new List<IGameEventListener<T>>();

        protected void RaiseEvent(T item)
        {
            for(var i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(item);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if(!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if(_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}
