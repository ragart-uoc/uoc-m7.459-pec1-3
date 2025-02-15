using UnityEngine;

namespace M7459.Entities.CharacterTypes
{
    /// <summary>
    /// Class <c>Runner</c> is the class for the Runner character type.
    /// </summary>
    public class Runner : ICharacterType
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Runner</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Runner(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartType</c> invokes the type Start method.
        /// </summary>
        public void StartType()
        {
            // Randomize the agent speed
            _character.agent.speed = Random.Range(_character.minSpeed, _character.maxSpeed);
            _character.agent.acceleration = _character.agent.speed * 2f;
            
            // Randomize the agent direction
            _character.patrolDirection = Random.value > 0.5f;
            
            // Reset the animator
            _character.animator.Rebind();
            _character.animator.Update(0f);
            
            // Start the state
            _character.CurrentType.Patrol();
        }

        /// <summary>
        /// Method <c>UpdateType</c> invokes the type Update method.
        /// </summary>
        public void UpdateType()
        {
            // Pass the velocity to the animator
            _character.animator.SetFloat(_character.AnimatorSpeed, _character.agent.velocity.magnitude);
            
            // Update the state
            _character.CurrentState.UpdateState();
        }
        
        /// <summary>
        /// Method <c>Follow</c> invokes the type Follow method.
        /// </summary>
        public void Follow()
        {
            // Runners don't follow
        }

        /// <summary>
        /// Method <c>Idle</c> invokes the type Idle method.
        /// </summary>
        public void Idle()
        {
            if (_character.CurrentState == _character.CharacterStates[CharacterProperties.States.Idle])
                return;
            _character.CurrentState = _character.CharacterStates[CharacterProperties.States.Idle];
            _character.CurrentState.StartState();
        }

        /// <summary>
        /// Method <c>Rest</c> invokes the type Rest method.
        /// </summary>
        public void Rest()
        {
            // Runners don't rest
        }
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        public void Patrol()
        {
            if (_character.CurrentState == _character.CharacterStates[CharacterProperties.States.Patrolling])
                return;
            _character.CurrentState = _character.CharacterStates[CharacterProperties.States.Patrolling];
            _character.CurrentState.StartState();
        }

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        public void Wander()
        {
            // Runners don't wander
        }
        
        /// <summary>
        /// Method <c>HandleCollisions</c> invokes the type HandleCollisions method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the collision.</param>
        public void HandleCollisions(CollisionProperties.Types type, Collision col, string tag)
        {
            // Runners don't need to handle collisions
        }
        
        /// <summary>
        /// Method <c>HandleTriggers</c> invokes the type HandleTriggers method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collider.</param>
        /// <param name="tag">The tag of the collider.</param>
        public void HandleTriggers(CollisionProperties.Types type, Collider col, string tag)
        {
            // Runners don't need to handle triggers
        }
    }
}
