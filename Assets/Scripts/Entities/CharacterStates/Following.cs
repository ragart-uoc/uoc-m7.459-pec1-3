using UnityEngine;

namespace M7459.Entities.CharacterStates
{
    public class Following : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Following</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Following(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
            if (_character.followTarget == null)
                _character.CurrentType.Idle();
            // Get the current offset from the target
            _character.followOffset = _character.transform.position - _character.followTarget.transform.position;
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            var characterTransform = _character.transform;
            var targetTransform = _character.followTarget.transform;

            // Position the character at the target's position plus the offset
            characterTransform.position = targetTransform.position + _character.followOffset;
            
            // Rotate the character to mirror the target's rotation
            var characterRotation = Quaternion.Inverse(targetTransform.rotation);
            characterTransform.rotation = characterRotation * Quaternion.Euler(0, 180, 0);
        }
        
        /// <summary>
        /// Method <c>HandleAnimations</c> invokes the state HandleAnimations method.
        /// <param name="animationEvent">The animation event.</param>
        /// </summary>
        public void HandleAnimations(AnimatorProperties.Events animationEvent)
        {
        }
    }
}