using System;
using UnityEngine;
using M7459.Entities;

namespace M7459.Managers
{
    /// <summary>
    /// Class <c>EventManager</c> manages the events of the game.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the singleton instance of the class.</value>
        public static EventManager Instance;
        
        public event EventHandler<CustomEventArgs> OnEventRaised;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// Method <c>TriggerEvent</c> triggers an event.
        /// </summary>
        /// <param name="e">The arguments of the event.</param>
        public void TriggerEvent(CustomEventArgs e)
        {
            OnEventRaised?.Invoke(this, e);
        }
    }
}
