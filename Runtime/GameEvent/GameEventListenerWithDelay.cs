using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ioni.GameEvent
{
    public class GameEventListenerWithDelay : GameEventListener
    {
        [SerializeField] private float delay = 1f; 
        [SerializeField] private UnityEvent delayedUnityEvent;

        private void Awake() => listenEvent.Register(this);
        private void OnDestroy() => listenEvent.Deregister(this);
        
        public override void RaiseEvent()
        {
            actEvent.Invoke();
            if(debuggingEnabled) Debug.Log("Unity event was triggered");
            StartCoroutine(RunDelayedEvent());
        }

        private IEnumerator RunDelayedEvent()
        {
            yield return new WaitForSeconds(delay);
            if(debuggingEnabled) Debug.Log("Delayed unity event was triggered");
            delayedUnityEvent.Invoke();
        }
    }
}
