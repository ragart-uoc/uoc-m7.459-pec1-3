using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>RotateTowards</c> is an action to rotate the transform so the forward vector of the game object points at target's current position.
    /// </summary>
    [Action("M7459/GameObject/RotateTowards")]
    [Help("Rotates the transform so the forward vector of the game object points at target's current position")]
    public class RotateTowards : GOAction
    {
        /// <value>Property <c>Target</c> represents the GameObject of the target.</value>
        [InParam("Target")]
        [Help("GameObject of the target")]
        public GameObject Target { get; set; }

        /// <value>Property <c>_targetTransform</c> represents the Transform of the target.</value>
        private Transform _targetTransform;

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Check if the target GameObject is null.</remarks>
        public override void OnStart()
        {
            if (Target == null)
            {
                Debug.LogError("The target is null", gameObject);
                return;
            }
            _targetTransform = Target.transform;
        }

        /// <summary>
        /// Method <c>OnUpdate</c> is called on every iteration of the task execution.
        /// </summary>
        /// <remarks>Rotate the Gameobject so that it points to the target GameObject position.</remarks>
        public override TaskStatus OnUpdate()
        {
            var direction = _targetTransform.position - gameObject.transform.position;
            var rotation = Quaternion.LookRotation(direction);
            var angle = Quaternion.Angle(gameObject.transform.rotation, rotation);
            if (angle < 10f)
                return TaskStatus.COMPLETED;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * 2f);
            return TaskStatus.RUNNING;
        }
        
        /// <summary>
        /// Method <c>OnAbort</c> is called when the task is aborted.
        /// </summary>
        /// <remarks>Complete the task.</remarks>
        public override void OnAbort()
        {
            gameObject.transform.rotation = Quaternion.identity;
            
        } 
    }
}
