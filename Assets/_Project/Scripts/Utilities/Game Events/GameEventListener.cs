using UnityEngine;

namespace Systems
{
    [System.Serializable]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private EventListener[] listeners = new EventListener[1];
        
        private void OnEnable()
        {
            foreach (var eventListener in listeners)
            {
                eventListener.GameEvent.RegisterListener(eventListener);    
            }
        }

        private void OnDisable()
        {
            foreach (var eventListener in listeners)
            {
                eventListener.GameEvent.UnregisterListener(eventListener);
            }
        }
    }
}
