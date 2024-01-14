using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace M7459.ML_Agents.Samples.RollerBall.Scripts
{
    /// <summary>
    /// Class <c>RollerBall</c> is a class that represents the RollerBall agent.
    /// </summary>
    public class RollerBall : Agent
    {
        /// <summary>Property <c>_rigidBody</c> represents the agent rigid body.</summary>
        private Rigidbody _rigidBody;
        
        /// <summary>Property <c>Target</c> represents the target transform.</summary>
        public Transform target;

        /// <summary>Property <c>forceMultiplier</c> represents the force multiplier.</summary>
        public float forceMultiplier = 10;
        
        /// <summary>Property <c>horizontalInput</c> represents the horizontal input.</summary>
        private float _horizontalInput;

        /// <summary>Property <c>verticalInput</c> represents the vertical input.</summary>
        private float _verticalInput;
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Method <c>OnEpisodeBegin</c> is called at the beginning of an episode.
        /// </summary>
        public override void OnEpisodeBegin()
        {
            if (transform.localPosition.y < 0)
            {
                _rigidBody.angularVelocity = Vector3.zero;
                _rigidBody.velocity = Vector3.zero;
                transform.localPosition = new Vector3(0, 0.5f, 0);
            }
            target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
        }
        
        /// <summary>
        /// Method <c>CollectObservations</c> is used to collect the vector observations of the agent for the step.
        /// </summary>
        /// <param name="sensor">The vector observations for the agent.</param>
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(target.localPosition);
            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(_rigidBody.velocity.x);
            sensor.AddObservation(_rigidBody.velocity.z);
        }
        
        /// <summary>
        /// Method <c>OnActionReceived</c> is used to specify agent behavior at every step, based on the provided action.
        /// </summary>
        /// <param name="actions">An array containing the action vector</param>
        public override void OnActionReceived(ActionBuffers actions)
        {
            var controlSignal = Vector3.zero;
            controlSignal.x = actions.ContinuousActions[0];
            controlSignal.z = actions.ContinuousActions[1];
            _rigidBody.AddForce(controlSignal * forceMultiplier);
            
            var distanceToTarget = Vector3.Distance(transform.localPosition, target.localPosition);
            if (distanceToTarget < 1.42f)
            {
                SetReward(1.0f);
                EndEpisode();
            }
            
            if (transform.localPosition.y < 0)
            {
                EndEpisode();
            }
        }
        
        /// <summary>
        /// Method <c>Heuristic</c> is used to choose an action for the agent using a custom heuristic.
        /// </summary>
        /// <param name="actionsOut">Array for the output actions.</param>
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var continuousActionsOut = actionsOut.ContinuousActions;
            continuousActionsOut[0] = _horizontalInput;
            continuousActionsOut[1] = _verticalInput;
        }
        
        /// <summary>
        /// Method <c>GetMovementInput</c> is used to get the movement input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void GetMovementInput(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var movement = context.ReadValue<Vector2>();
            _horizontalInput = movement.x;
            _verticalInput = movement.y;
        }
    }
}
