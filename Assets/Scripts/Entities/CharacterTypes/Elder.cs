using System;
using UnityEngine;

namespace M7459.Entities.CharacterTypes
{
    /// <summary>
    /// Class <c>Elder</c> is the class for the Elder character type.
    /// </summary>
    public class Elder : ICharacterType
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Elder</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Elder(Character character)
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
            _character.CurrentType.Wander();
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
            if (_character.CurrentState == _character.CharacterStates[CharacterProperties.States.Resting])
                return;
            _character.CurrentState = _character.CharacterStates[CharacterProperties.States.Resting];
            _character.CurrentState.StartState();
        }
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        public void Patrol()
        {
            // Elders don't patrol
        }

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        public void Wander()
        {
            if (_character.CurrentState == _character.CharacterStates[CharacterProperties.States.Wandering])
                return;
            _character.CurrentState = _character.CharacterStates[CharacterProperties.States.Wandering];
            _character.CurrentState.StartState();
        }
        
        /// <summary>
        /// Method <c>HandleCollisions</c> invokes the type HandleCollisions method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the collision.</param>
        public void HandleCollisions(CollisionProperties.Types type, Collision col, string tag)
        {
            // Elders don't need to handle collisions
        }
        
        /// <summary>
        /// Method <c>HandleTriggers</c> invokes the type HandleTriggers method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collider.</param>
        /// <param name="tag">The tag of the collider.</param>
        public void HandleTriggers(CollisionProperties.Types type, Collider col, string tag)
        {
            // Check if the collider is a rest area
            if (!col.transform.CompareTag("RestArea"))
                return;
            var restArea = col.gameObject.GetComponent<RestArea>();
            
            // Check if the rest area is occupied
            if (restArea.isOccupied && restArea.occupant != _character.gameObject)
                return;

            switch (type)
            {
                case CollisionProperties.Types.TriggerEnter:
                    // Set the rest area as occupied
                    restArea.isOccupied = true;
                    restArea.occupant = _character.gameObject;

                    // Set the rest area positions
                    _character.restAreaEnterPosition = restArea.enterPosition;
                    _character.restAreaExitPosition = restArea.exitPosition;

                    // Rest
                    _character.CurrentType.Rest();
                    break;
                case CollisionProperties.Types.TriggerExit:
                    // Set the rest area as not occupied
                    restArea.isOccupied = false;
                    restArea.occupant = null;

                    // Unset the rest area positions
                    _character.restAreaEnterPosition = null;
                    _character.restAreaExitPosition = null;

                    // Wander
                    _character.CurrentType.Wander();
                    break;
            }
        }
    }
}
