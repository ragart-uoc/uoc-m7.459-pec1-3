using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using M7459.Entities;
using M7459.Managers;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>RaiseCustomEvent</c> is an action to raise a custom event.
    /// </summary>
    [Action("M7459/Event/RaiseCustomEvent")]
    [Help("Raises a custom event")]
    public class RaiseCustomEvent : GOAction
    {
        /// <value>Property <c>EventManager</c> represents the GameObject containing the custom event management logic.</value>
        [InParam("EventManager")]
        [Help("GameObject containing the custom event management logic")]
        public EventManager EventManager { get; set; }
        
        /// <value>Property <c>CustomEventType</c> represents the custom event type to raise.</value>
        [InParam("EventType")]
        [Help("Custom event type to raise")]
        public CustomEventProperties.Types CustomEventType { get; set; }
        
        /// <value>Property <c>Target</c> represents the target of the event.</value>
        [InParam("Target")]
        [Help("The target of the event")]
        public GameObject Target { get; set; }
        
        /// <value>Property <c>Value</c> represents the value of the event.</value>
        [InParam("Value")]
        [Help("The value of the event")]
        public float Value { get; set; }
        
        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Raise the event.</remarks>
        public override void OnStart()
        {
            EventManager = EventManager ? EventManager : EventManager.Instance;
            if (EventManager == null)
            {
                Debug.LogError("The event manager is null");
                return;
            }
            var args = new CustomEventArgs
            {
                Type = CustomEventType,
                Raiser = gameObject,
                Target = Target,
                Value = Value
            };
            EventManager.SendMessage("TriggerEvent", args);
        }
        
        /// <summary>
        /// Method <c>OnUpdate</c> is called on every iteration of the task execution.
        /// </summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}