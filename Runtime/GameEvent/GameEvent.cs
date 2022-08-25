using System.Collections.Generic;
using UnityEngine;

/*
using System.Collections;
using GS.CommonGameEvents;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace GS.Tests.PlayMode
{
    public class GameEventTests
    {
        private GameEventListener _gameEventListener;
        private GameEvent _gameEvent;
        private UnityEvent _unityEvent;
        
        [SetUp]
        public void SetUpTests()
        {
            _gameEvent = ScriptableObject.CreateInstance<GameEvent>();
            _unityEvent = new UnityEvent();
            
            var go = new GameObject();
            _gameEventListener = go.AddComponent<GameEventListener>();
            _gameEventListener.gameEvent = _gameEvent;
            _gameEventListener.unityEvent = _unityEvent;
            _gameEventListener.debuggingEnabled = false;
            _gameEventListener.RegisterGameEvent(_gameEventListener);
        }
        
        [UnityTest]
        public IEnumerator GameEventListenerDebuggingDisabled()
        {
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(false, _gameEventListener.debuggingEnabled);
        }
        
        [UnityTest]
        public IEnumerator GameEventHasOneListener()
        {
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(1, _gameEvent.ListenerCount);
        }
        
        [UnityTest]
        public IEnumerator GameEventHasMultipleListeners()
        {
            var go2 = new GameObject();
            var additionalListener = go2.AddComponent<GameEventListener>();
            additionalListener.gameEvent = _gameEvent;
            additionalListener.unityEvent = _unityEvent;
            additionalListener.debuggingEnabled = false;
            additionalListener.RegisterGameEvent(additionalListener);
            
            yield return new WaitForFixedUpdate();
            Assert.Greater(_gameEvent.ListenerCount, 1);
        }
        
        [UnityTest]
        public IEnumerator GameEventWasTriggered()
        {
            var unityEventWasTriggered = false;
            _unityEvent.AddListener((() => unityEventWasTriggered = true));
            _gameEvent.Invoke();
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(true, unityEventWasTriggered);
        }
    }
}

*/
namespace Ioni.GameEvent
{
    /// <summary>
    /// GameEvent
    /// 
    /// A scriptable object for global game events.
    /// </summary>
    [CreateAssetMenu(menuName = "Game Event", fileName = "New Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();
        
        /// <summary>
        /// Invoke
        ///
        /// Raises event for all the listeners registered to this GameEvent
        /// </summary>
        public void Invoke()
        {
            foreach (var globalEventListener in _listeners)
            {
                globalEventListener.RaiseEvent();
            }
        }

        
        /// <summary>
        /// ListenerCount - Returns the amount of listeners attached to this GameEvent
        /// </summary>
        public int ListenerCount => _listeners.Count;

        
        /// <summary>
        /// Register
        ///
        /// Apply a game event listener on this game event
        /// </summary>
        /// <param name="geListener">Game event listener to add</param>
        public void Register(GameEventListener geListener) => _listeners.Add(geListener);
        
        /// <summary>
        /// Deregister
        ///
        /// Remove a game event listener from this game event
        /// </summary>
        /// <param name="geListener">Game event listener to remove</param>
        public void Deregister(GameEventListener geListener) => _listeners.Remove(geListener);
    }
}