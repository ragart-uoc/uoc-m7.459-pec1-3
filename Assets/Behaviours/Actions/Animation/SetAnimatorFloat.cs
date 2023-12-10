using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>SetAnimatorFloat</c> is an action to set a float parameter on an animator variable.
    /// </summary>
    [Action("M7459/Animation/SetAnimatorFloat")]
    [Help("Sets a float parameter on an animator variable")]
    public class SetAnimatorFloat : GOAction
    {
        /// <value>Property <c>AnimatorVariable</c> represents the name of the animator variable to set.</value>
        [InParam("AnimatorVariable")]
        [Help("Name of the animator variable to set")]
        public string AnimatorVariable { get; set; }
        
        /// <value>Property <c>AnimatorFloatValue</c> represents the value to set the animator variable to.</value>
        [InParam("AnimatorFloatValue")]
        [Help("The value to set the animator variable to")]
        public float AnimatorFloatValue { get; set; }

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Set the animator variable to the float value.</remarks>
        public override void OnStart()
        {
            if (AnimatorVariable == null)
            {
                Debug.LogError("The animator variable is null");
                return;
            }
            var animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetFloat(Animator.StringToHash(AnimatorVariable), AnimatorFloatValue);
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
