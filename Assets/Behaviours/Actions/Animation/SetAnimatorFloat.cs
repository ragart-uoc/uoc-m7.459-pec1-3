using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to set a float parameter on an animator variable.
    /// </summary>
    [Action("M7459/Animation/SetAnimatorFloat")]
    [Help("Sets a float parameter on an animator variable")]
    public class SetAnimatorFloat : GOAction
    {
        /// <value>Input Name of the animator variable to set Parameter</value>
        [InParam("AnimatorVariable")]
        [Help("Name of the animator variable to set")]
        public string AnimatorVariable { get; set; }
        
        /// <value>Input Value to set the animator variable to Parameter</value>
        [InParam("AnimatorFloatValue")]
        [Help("The value to set the animator variable to")]
        public float AnimatorFloatValue { get; set; }

        /// <summary>Start Method of SetAnimatorFloat</summary>
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

        /// <summary>Update Method of SetAnimatorFloat</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
