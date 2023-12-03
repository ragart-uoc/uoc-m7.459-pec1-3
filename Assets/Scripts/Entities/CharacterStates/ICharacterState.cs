namespace M7459.Entities.CharacterStates
{
    /// <summary>
    /// Interface <c>ICharacterState</c> is the interface for the character states.
    /// </summary>
    public interface ICharacterState
    {
        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        void StartState();
        
        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        void UpdateState();
        
        /// <summary>
        /// Method <c>HandleAnimations</c> invokes the state HandleAnimations method.
        /// <param name="animationEvent">The animation event.</param>
        /// </summary>
        void HandleAnimations(AnimatorProperties.Events animationEvent);
    }
}
