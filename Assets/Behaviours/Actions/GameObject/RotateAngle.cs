using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>RotateAngle</c> is an action to decrease the rotation of the transform in a given angle.
    /// </summary>
    [Action("M7459/GameObject/RotateAngle")]
    [Help("Decrease the rotation of the transform in a given angle")]
    public class RotateAngle : GOAction
    {
        /// <value>Property <c>Angle</c> represents the angle.</value>
        [InParam("Angle")]
        [Help("Angle")]
        public float Angle { get; set; }

        /// <value>Property <c>Axis</c> represents the axis.</value>
        [InParam("Axis")]
        [Help("Axis")]
        public Vector3 Axis { get; set; }
        
        /// <value>Property <c>_targetRotation</c> represents the target rotation.</value>
        private Quaternion _targetRotation;
        
        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Sets the target rotation.</remarks>
        public override void OnStart()
        {
            var difference = Quaternion.AngleAxis(Angle, Axis);
            _targetRotation = gameObject.transform.rotation * difference;
        }

        /// <summary>
        /// Method <c>OnUpdate</c> is called on every iteration of the task execution.
        /// </summary>
        /// <remarks>Rotates the GamOobject towards the target rotation.</remarks>
        public override TaskStatus OnUpdate()
        {
            var currentAngle = Quaternion.Angle(gameObject.transform.rotation, _targetRotation);
            if (currentAngle < 5f)
                return TaskStatus.COMPLETED;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, _targetRotation, Time.deltaTime * 2f);
            return TaskStatus.RUNNING;
        }
    }
}