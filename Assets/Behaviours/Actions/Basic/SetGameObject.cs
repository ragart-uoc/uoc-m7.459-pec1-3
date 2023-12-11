using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Method <c>SetGameObject</c> is a basic action to associate a GameObject to a variable.
    /// </summary>
    [Action("Basic/SetGameObject")]
    [Help("Sets a value to a GameObject variable")]
    public class SetGameObject : BasePrimitiveAction
    {
        /// <value>Input Input variable.</value>
        [InParam("Value")]
        [Help("Input variable")]
        public GameObject Value { get; set; }
        
        /// <value>OutPut Variable.</value>
        [OutParam("Variable")]
        [Help("Output variable")]
        public GameObject Variable { get; set; }

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Initializes the Boolean value.</remarks>
        public override void OnStart()
        {
            Variable = Value;
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