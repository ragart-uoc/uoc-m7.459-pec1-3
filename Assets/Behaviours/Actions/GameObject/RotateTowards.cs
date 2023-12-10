using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to rotate the Gameobject so that it points to the target GameObject position.
    /// </summary>
    [Action("M7459/GameObject/RotateTowards")]
    [Help("Rotates the transform so the forward vector of the game object points at target's current position")]
    public class RotateTowards : GOAction
    {
        /// <value>Input GameObject of the target Parameter</value>
        [InParam("Target")]
        [Help("GameObject of the target")]
        public GameObject Target { get; set; }

        /// <value>Transform of the target.</value>
        private Transform _targetTransform;

        /// <summary>Start Method of RotateTowards.</summary>
        /// <remarks>Check if the target GameObject is null.</remarks>
        public override void OnStart()
        {
            if (Target == null)
            {
                Debug.LogError("The GameObject is null", gameObject);
                return;
            }
            _targetTransform = Target.transform;
        }

        /// <summary>Update Method of RotateTowards.</summary>
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
        
        /// <summary>Abort Method of RotateTowards.</summary>
        /// <remarks>Complete the task.</remarks>
        public override void OnAbort()
        {
            gameObject.transform.rotation = Quaternion.identity;
            
        } 
    }
}
