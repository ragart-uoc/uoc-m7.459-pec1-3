using UnityEngine;
using UnityEngine.AI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>Fox</c> is a class that represents the Fox agent.
    /// </summary>
    public class Fox : Agent
    {
        #region Agent

            /// <summary>Property <c>_rigidBody</c> represents the agent rigid body.</summary>
            private Rigidbody _rigidBody;
            
            /// <summary>Property <c>_startPosition</c> represents the agent start position.</summary>
            private Vector3 _startPosition;

            /// <summary>Property <c>forceMultiplier</c> represents the force multiplier.</summary>
            public float forceMultiplier;
            
            /// <summary>Property <c>useVectorObs</c> represents whether to use vector observations.</summary>
            public bool useVectorObs;
        
        #endregion
        
        #region Target
            
            /// <summary>Property <c>Target</c> represents the target transform.</summary>
            public Transform targetTransform;
            
        #endregion
        
        #region Episode
        
            /// <summary>Property <c>_lastEpisodeWin</c> represents whether the last episode was a win.</summary>
            private bool _lastEpisodeWin;
            
            /// <summary>Property <c>_obstacleCollisionStepCount</c> represents the number of steps that the agent has collided with an obstacle.</summary>
            private int _obstacleCollisionStepCount;
            
        #endregion
        
        #region Input
        
            /// <summary>Property <c>horizontalInput</c> represents the horizontal input.</summary>
            private float _horizontalInput;

            /// <summary>Property <c>verticalInput</c> represents the vertical input.</summary>
            private float _verticalInput;
        
        #endregion
        
        #region Floor
        
            /// <summary>Property <c>floorWidth</c> represents the floor width.</summary>
            public float floorWidth;
            
            /// <summary>Property <c>floorLength</c> represents the floor length.</summary>
            public float floorLength;
            
            /// <summary>Property <c>floorOffset</c> represents the floor offset.</summary>
            public Vector3 floorOffset;
            
        #endregion
        
        #region Animator 
        
            /// <summary>Property <c>_animator</c> represents the agent animator.</summary>
            private Animator _animator;
            
            /// <summary>Property <c>AnimatorSpeed</c> represents the pointer to the animator speed variable.</summary>
            private static readonly int AnimatorSpeed = Animator.StringToHash("Speed");
            
            /// <summary>Property <c>_targetAnimator</c> represents the target animator.</summary>
            private Animator _targetAnimator;

            /// <summary>Property <c>TargetAnimatorRestart</c> represents the pointer to the animator restart variable.</summary>
            private static readonly int TargetAnimatorRestart = Animator.StringToHash("Restart");

        #endregion

        /// <summary>
        /// Method <c>Initialize</c> is called once when the agent is first enabled.
        /// </summary>
        public override void Initialize()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _startPosition = transform.localPosition;
            _animator = transform.GetChild(0).GetComponent<Animator>();
            _targetAnimator = targetTransform.GetChild(0).GetComponent<Animator>();
        }

        /// <summary>
        /// Method <c>FixedUpdate</c> is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            // Pass speed to animator
            _animator.SetFloat(AnimatorSpeed, _rigidBody.velocity.magnitude);
        }

        /// <summary>
        /// Method <c>OnEpisodeBegin</c> is called at the beginning of an episode.
        /// </summary>
        public override void OnEpisodeBegin()
        {
            // Agent
            if (_lastEpisodeWin == false)
            {
                _rigidBody.angularVelocity = Vector3.zero;
                _rigidBody.velocity = Vector3.zero;
                transform.localPosition = _startPosition;
                transform.localRotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
            }
            // Target
            MoveToRandomPosition(targetTransform);
            targetTransform.LookAt(transform);
            _targetAnimator.SetTrigger(TargetAnimatorRestart);
            // Reset other properties
            _lastEpisodeWin = false;
            _obstacleCollisionStepCount = 0;
        }
        
        /// <summary>
        /// Method <c>CollectObservations</c> is used to collect the vector observations of the agent for the step.
        /// </summary>
        /// <param name="sensor">The vector observations for the agent.</param>
        public override void CollectObservations(VectorSensor sensor)
        {
            if (!useVectorObs)
                return;
            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(transform.InverseTransformDirection(_rigidBody.velocity));
        }
        
        /// <summary>
        /// Method <c>OnActionReceived</c> is used to specify agent behavior at every step, based on the provided action.
        /// </summary>
        /// <param name="actions">An array containing the action vector</param>
        public override void OnActionReceived(ActionBuffers actions)
        {
            if (transform.localPosition.y < -10f)
            {
                SetReward(-0.5f);
                EndEpisode();
                return;
            }
            AddReward(-0.5f / MaxStep);
            MoveAgent(actions.DiscreteActions);
        }
        
        /// <summary>
        /// Method <c>Heuristic</c> is used to choose an action for the agent using a custom heuristic.
        /// </summary>
        /// <param name="actionsOut">Array for the output actions.</param>
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discreteActionsOut = actionsOut.DiscreteActions;
            if (_verticalInput > 0)
            {
                discreteActionsOut[0] = 1;
            }
            else if (_horizontalInput > 0)
            {
                discreteActionsOut[0] = 2;
            }
            else if (_horizontalInput < 0)
            {
                discreteActionsOut[0] = 3;
            }
        }
        
        /// <summary>
        /// Method <c>OnTriggerEnter</c> is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="col">The other Collider involved in the collision.</param>
        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("MLTarget"))
            {
                _lastEpisodeWin = true;
                SetReward(+1.0f);
                EndEpisode();
            }

            if (col.CompareTag("MLDeathZone"))
            {
                SetReward(-0.5f);
                EndEpisode();
            }
        }
        
        /// <summary>
        /// Method <c>OnCollisionStay</c> is called once per frame for every collider/rigidbody that is touching rigidbody/collider.
        /// </summary>
        /// <param name="col">The Collision data associated with this collision.</param>
        private void OnCollisionStay(Collision col)
        {
            if (col.transform.CompareTag("Prop") || col.transform.CompareTag("Character"))
            {
                AddReward(-0.1f);
                _obstacleCollisionStepCount++;
                if (_obstacleCollisionStepCount >= 10)
                    EndEpisode();
            }
        }
        
        /// <summary>
        /// Method <c>MoveAgent</c> is used to move the agent.
        /// </summary>
        /// <param name="actions">The action vector.</param>
        private void MoveAgent(ActionSegment<int> actions)
        {
            var direction = Vector3.zero;
            var rotationDirection = Vector3.zero;

            var action = actions[0];
            switch (action)
            {
                case 1:
                    direction = transform.forward * 1f;
                    break;
                case 2:
                    rotationDirection = transform.up * 1f;
                    break;
                case 3:
                    rotationDirection = transform.up * -1f;
                    break;
            }
            transform.Rotate(rotationDirection, Time.fixedDeltaTime * 200f);
            _rigidBody.AddForce(direction * forceMultiplier, ForceMode.VelocityChange);
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

        /// <summary>
        /// Method <c>MoveToRandomPosition</c> is used to move a transform to a random position.
        /// </summary>
        /// <param name="moveTransform">The transform to move.</param>
        private void MoveToRandomPosition(Transform moveTransform)
        {
            while (true)
            {
                // Get a random position
                var newPosition =
                    new Vector3(Random.Range(-floorWidth / 2f, floorWidth / 2f),
                        0f,
                        Random.Range(-floorLength / 2f, floorLength / 2f)
                        ) + floorOffset;
                // Check if the new position is too close to the agent
                if (Vector3.Distance(newPosition, transform.localPosition) < 1.5f)
                {
                    continue;
                }
                // Check if the new position is a NavMesh position
                if (!NavMesh.SamplePosition(newPosition, out var navMeshHit, 1.0f, NavMesh.AllAreas))
                {
                    continue;
                }
                // Set the new position
                moveTransform.localPosition = navMeshHit.position;
                break;
            }
        }
    }
}
