using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using M7459.Entities;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>SetAnimatorTrigger</c> is an action to trigger an animator variable.
    /// </summary>
    [Action("M7459/Animation/SetAnimatorTrigger")]
    [Help("Triggers an animator variable")]
    public class SetAnimatorTrigger : GOAction
    {
        /// <value>Property <c>AnimatorVariable</c> represents the name of the animator variable to set.</value>
        [InParam("AnimatorVariable")]
        [Help("Name of the animator variable to set")]
        public string AnimatorVariable { get; set; }

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Set the animator variable to the float value.</remarks>
        public override void OnStart()
        {
            var animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetTrigger(Animator.StringToHash(AnimatorVariable));
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