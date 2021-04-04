using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    [System.Serializable, CreateAssetMenu(fileName = "New Game Event", menuName = "GameEvents/New Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<EventListener> eventListeners = 
            new List<EventListener>();
        
        
        // TODO: Add debug global flag to set this bool
        [SerializeField] private bool dispatchMessage = false;

        public void Raise(object item = default)
        {
            for (var i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(item);
                
                if (dispatchMessage)
                {
                    Debug.LogFormat("Event named : {0} was raised", this.name);
                }
            }
        }
        
        public void Raise()
        {
            for (var i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised();
                
                if (dispatchMessage)
                {
                    Debug.LogFormat("Event named : {0} was raised", this.name);
                }
            }
        }
        
        

        public void RegisterListener(EventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(EventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}