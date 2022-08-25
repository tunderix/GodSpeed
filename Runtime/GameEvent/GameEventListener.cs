using UnityEngine;
using UnityEngine.Events;

namespace Ioni.GameEvent
{
    /// <summary>
    /// GameEventListener
    /// Has two events. One GameEvent, which is being listened to. Any time the listened event is invoked,
    /// the ActEvent will be also invoked.
    /// Register listened event on Awake and Deregister when the GameObject is destroyed.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] public GameEvent listenEvent;
        [SerializeField] public UnityEvent actEvent;
        [SerializeField] public bool debuggingEnabled;

        private void Awake() => RegisterGameEvent(this);
        private void OnDestroy() => DeregisterGameEvent(this);
        
        /// <summary>
        /// Makes sure the listen event is assigned, then log it up and
        /// finally registers the event to raise events through this listener
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterGameEvent(GameEventListener listener)
        {
            if (listenEvent == null)
            {
                if(debuggingEnabled) D.Warn("ListenEvent was not assigned");
                return;
            }
            
            if(debuggingEnabled) D.Info("GameEventListener was registered");
            listenEvent.Register(listener);
        }
        
        /// <summary>
        /// Makes sure the listen event is assigned, then log it up and
        /// finally Deregister the listener
        /// </summary>
        /// <param name="listener"></param>
        public void DeregisterGameEvent(GameEventListener listener)
        {
            if (listenEvent == null)
            {
                if(debuggingEnabled) D.Warn("ListenEvent was not assigned");
                return;
            }
            
            if(debuggingEnabled) D.Info("GameEventListener was Deregistered");
            listenEvent.Deregister(listener);
        }
        
        /// <summary>
        /// This is invoked by the listened event.
        /// Launches the assigned ActEvent.
        /// </summary>
        public virtual void RaiseEvent()
        {
            if (actEvent == null)
            {
                if(debuggingEnabled) D.Warn("ActEvent was not assigned");
                return;
            }
            
            if(debuggingEnabled) D.Info("ActEvent was triggered");
            actEvent.Invoke();
        }
    }
}
