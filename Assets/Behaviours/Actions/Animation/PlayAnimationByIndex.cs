using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;
using M7459.Entities;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>PlayAnimationByIndex</c> is an action to trigger an animator variable by its index.
    /// </summary>
    [Action("M7459/Animation/PlayAnimationByIndex")]
    [Help("Triggers an animator variable by its index")]
    public class PlayAnimationByIndex : GOAction
    {
        /// <value>Property <c>AnimatorIndex</c> represents the index of the animator variable to set.</value>
        [InParam("AnimationIndex")]
        [Help("Index of the animator variable to set")]
        public float AnimationIndex { get; set; }

        private Animator _animator;

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Set the animator variable to the float value.</remarks>
        public override void OnStart()
        {
            
            var animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetTrigger(Animator.StringToHash(((AnimatorProperties.Animations)AnimationIndex).ToString()));
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