using System.Collections.Generic;
using UnityEngine;

namespace PEC1.Entities.CharacterTypes
{
    /// <summary>
    /// Class <c>Runner</c> is the class for the Runner character type.
    /// </summary>
    public class Runner : ICharacterType
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;
        
        /// <value>Property <c>TargetTags</c> represents the tags of the targets.</value>
        public List<string> TargetTags { get; set; }

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
            // Reset the animator
            _character.animator.Rebind();
            _character.animator.Update(0f);
            
            // Start the state
            _character.CurrentState = _character.CharacterStates[CharacterProperties.States.Patrolling];
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
        /// Method <c>Rest</c> invokes the type Rest method.
        /// </summary>
        public void Rest()
        {
            // Runners don't rest
        }

        /// <summary>
        /// Method <c>StopResting</c> invokes the type StopResting method.
        /// </summary>
        public void StopResting()
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
        /// Method <c>HandleCollisionEnter</c> invokes the type OnCollisionEnter method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleCollisionEnter(Collision col, string tag)
        {
        }
        
        /// <summary>
        /// Method <c>HandleCollisionStay</c> invokes the type OnCollisionStay method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleCollisionStay(Collision col, string tag)
        {
        }
        
        /// <summary>
        /// Method <c>HandleCollisionExit</c> invokes the type OnCollisionExit method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleCollisionExit(Collision col, string tag)
        {
        }
        
        /// <summary>
        /// Method <c>HandleTriggerEnter</c> invokes the type OnTriggerEnter method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleTriggerEnter(Collider col, string tag)
        {
        }
        
        /// <summary>
        /// Method <c>HandleTriggerStay</c> invokes the type OnTriggerStay method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleTriggerStay(Collider col, string tag)
        {
        }
        
        /// <summary>
        /// Method <c>HandleTriggerExit</c> invokes the type OnTriggerExit method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        public void HandleTriggerExit(Collider col, string tag)
        {
        }

    }
}
