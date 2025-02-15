using System;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using M7459.Entities;
using M7459.Managers;
using UnityEngine;

namespace BBCore.Conditions
{
    /// <summary>
    /// Class <c>CustomEventRaised</c> is a condition to check whether a custom event of a given type has been raised.
    /// </summary>
    [Condition("M7459/Event/CustomEventRaised")]
    [Help("Checks whether a custom event of a given type has been raised")]
    public class CustomEventRaised : ConditionBase
    {
        /// <value>Property <c>EventManager</c> represents the GameObject containing the custom event management logic.</value>
        [InParam("EventManager")]
        [Help("GameObject containing the custom event management logic")]
        public EventManager EventManager { get; set; }

        /// <value>Property <c>CustomEventType</c> represents the custom event type to be checked.</value>
        [InParam("Event type")]
        [Help("Custom event type to be checked")]
        public CustomEventProperties.Types CustomEventType { get; set; }
        
        /// <value>Property <c>Raiser</c> represents the raiser of the event.</value>
        [OutParam("Raiser")]
        [Help("The raiser of the event")]
        public GameObject Raiser { get; set; }
        
        /// <value>Property <c>Target</c> represents the target of the event.</value>
        [OutParam("Target")]
        [Help("The target of the event")]
        public GameObject Target { get; set; }
        
        /// <value>Property <c>Value</c> represents the value of the event.</value>
        [OutParam("Value")]
        [Help("The value of the event")]
        public float Value { get; set; }
        
        /// <summary>
        /// Method <c>Check</c> checks whether the condition is fulfilled.
        /// </summary>
        public override bool Check()
        {
            return true;
        }
        
        /// <summary>
        /// Method <c>MonitorCompleteWhenTrue</c> is called when the condition is true and the task is to be completed.
        /// </summary>
        public override TaskStatus MonitorCompleteWhenTrue()
        {
            EventManager.OnEventRaised += OnEventRaised;
            return TaskStatus.SUSPENDED;
        }
        
        /// <summary>
        /// Method <c>MonitorFailWhenFalse</c> is called when the condition is false and the task is to be failed.
        /// </summary>
        public override TaskStatus MonitorFailWhenFalse()
        {
            EventManager.OnEventRaised += OnEventRaised;
            return TaskStatus.SUSPENDED;
        }
        
        /// <summary>
        /// Method <c>OnEventRaised</c> is called when a custom event is raised.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnEventRaised(object sender, EventArgs e)
        {
            var args = e as CustomEventArgs;
            if (args!.Type != CustomEventType)
                return;
            Raiser = args.Raiser;
            Target = args.Target;
            Value = args.Value;
            EventManager.OnEventRaised -= OnEventRaised;
            EndMonitorWithSuccess();
        }
    }
}