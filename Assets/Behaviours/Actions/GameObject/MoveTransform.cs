using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>MoveTransform</c> is an action to move the transform in a given direction and distance.
    /// </summary>
    [Action("M7459/GameObject/MoveTransform")]
    [Help("Move the transform in a given direction and distance")]
    public class MoveTransform : GOAction
    {
        /// <value>Property <c>Direction</c> represents the direction.</value>
        [InParam("Direction")]
        [Help("Direction")]
        public Vector3 Direction { get; set; }
        
        /// <value>Property <c>Distance</c> represents the distance.</value>
        [InParam("Distance")]
        [Help("Distance")]
        public float Distance { get; set; }
        
        /// <value>Property <c>_targetPosition</c> represents the target position.</value>
        private Vector3 _targetPosition;
        
        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Sets the target position.</remarks>
        public override void OnStart()
        {
            _targetPosition = gameObject.transform.position + Direction * Distance;
        }
        
        /// <summary>
        /// Method <c>OnUpdate</c> is called on every iteration of the task execution.
        /// </summary>
        /// <remarks>Move the GameObject towards the target position.</remarks>
        public override TaskStatus OnUpdate()
        {
            var currentPosition = gameObject.transform.position;
            var currentDistance = Vector3.Distance(currentPosition, _targetPosition);
            if (currentDistance < 0.1f)
                return TaskStatus.COMPLETED;
            gameObject.transform.position = Vector3.MoveTowards(currentPosition, _targetPosition, Time.deltaTime * 2f);
            return TaskStatus.RUNNING;
        }
    }
}