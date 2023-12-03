namespace M7459.Entities.CharacterStates
{
    public class Idle : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Idle</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Idle(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
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
