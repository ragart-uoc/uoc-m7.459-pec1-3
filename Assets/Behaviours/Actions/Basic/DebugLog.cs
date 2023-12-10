using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>DebugLog</c> is an action to log a message to the debug console.
    /// </summary>
    [Action("M7459/Event/DebugLog")]
    [Help("Logs a message to the debug console")]
    public class DebugLog : GOAction
    {
        /// <value>Property <c>Message</c> represents the message to be logged.</value>
        [InParam("Message")]
        [Help("Message to be logged")]
        public string Message { get; set; }
        
        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Log the message.</remarks>
        public override void OnStart()
        {
            if (Message == null)
            {
                Debug.LogError("The message is null");
                return;
            }
            Debug.Log(Message);
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