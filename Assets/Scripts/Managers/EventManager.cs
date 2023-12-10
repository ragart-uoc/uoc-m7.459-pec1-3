using System;
using UnityEngine;

namespace M7459.Managers
{
    /// <summary>
    /// Class <c>EventManager</c> manages the events of the game.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the singleton instance of the class.</value>
        public static EventManager Instance;

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

        public void TriggerEvent(object args)
        {
            Debug.Log("Method: " + args.GetType().GetMethod("MethodName"));
            Debug.Log("Caller: " + args.GetType().GetMethod("Caller"));
            
            
            /*
            var e = EventArgs.Empty;
            e.GetType().GetProperty("Args")?.SetValue(e, args);
            TriggerEvent(handler, e);
            */
        }

        public void TriggerEvent(string handler, EventArgs args)
        {
            var eventDelegate =
                (MulticastDelegate)GetType().GetField(handler,
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                    ?.GetValue(this);

            var delegates = eventDelegate?.GetInvocationList();

            Debug.Assert(delegates != null, nameof(delegates) + " != null");
            foreach (var dlg in delegates)
            {
                dlg.Method.Invoke(dlg.Target, new object[] { this, args });
            }
        }
    }
}
