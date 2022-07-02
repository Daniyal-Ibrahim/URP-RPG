using _Scripts.Events.CustomEvents;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events.Listeners
{
    public class BaseGameEventListener<T, TE, TUer> : MonoBehaviour,
        IGameEventListener<T> where TE : BaseGameEvent<T> where TUer : UnityEvent<T>
    {
        [SerializeField] private TE gameEvent = null;
        private TE GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private TUer unityEventResponse = null;

        private void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}
