using UnityEngine;

namespace M7459.Entities.CharacterTypes
{
    /// <summary>
    /// Class <c>ZenPerson</c> is the class for the ZenPerson character type.
    /// </summary>
    public class ZenPerson : ICharacterType
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>ZenPerson</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public ZenPerson(Character character)
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
            _character.CurrentType.StartType();
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
        /// Method <c>Rest</c> invokes the type Rest method.
        /// </summary>
        public void Rest()
        {
            // ZenPersons don't rest
        }
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        public void Patrol()
        {
            // ZenPersons don't patrol
        }

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        public void Wander()
        {
            // ZenPerson don't wander
        }
        
        /// <summary>
        /// Method <c>HandleCollisions</c> invokes the type HandleCollisions method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the collision.</param>
        public void HandleCollisions(CollisionProperties.Types type, Collision col, string tag)
        {
            // ZenPersons don't need to handle collisions
        }
        
        /// <summary>
        /// Method <c>HandleTriggers</c> invokes the type HandleTriggers method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collider.</param>
        /// <param name="tag">The tag of the collider.</param>
        public void HandleTriggers(CollisionProperties.Types type, Collider col, string tag)
        {
            // ZenPersons don't need to handle triggers
        }
    }
}
