using UnityEngine;

namespace M7459.Entities.CharacterTypes
{
    /// <summary>
    /// Class <c>ZenMaster</c> is the class for the ZenMaster character type.
    /// </summary>
    public class ZenMaster : ICharacterType
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>ZenMaster</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public ZenMaster(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartType</c> invokes the type Start method.
        /// </summary>
        public void StartType()
        {
            // Reset the animator
            _character.animator.Rebind();
            _character.animator.Update(0f);
            
            // Start the state
            _character.CurrentState.StartState();
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
            // ZenMasters don't follow
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
            // ZenMasters don't rest
        }
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        public void Patrol()
        {
            // ZenMasters don't patrol
        }

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        public void Wander()
        {
            // ZenMaster don't wander
        }
        
        /// <summary>
        /// Method <c>HandleCollisions</c> invokes the type HandleCollisions method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the collision.</param>
        public void HandleCollisions(CollisionProperties.Types type, Collision col, string tag)
        {
            // ZenMasters don't need to handle collisions
        }
        
        /// <summary>
        /// Method <c>HandleTriggers</c> invokes the type HandleTriggers method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collider.</param>
        /// <param name="tag">The tag of the collider.</param>
        public void HandleTriggers(CollisionProperties.Types type, Collider col, string tag)
        {
            // ZenMasters don't need to handle triggers
        }
    }
}
