namespace M7459.Entities.CharacterStates
{
    public class Patrolling : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Patrolling</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Patrolling(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
            _character.agent.isStopped = false;
            _character.SetNextLocation();
            _character.agent.destination = _character.locationList[_character.nextLocation].position;
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            if (_character.agent.pathPending || _character.agent.remainingDistance > _character.agent.stoppingDistance)
                return;
            // Define the next location depending on the direction
            _character.SetNextLocation();
            _character.agent.destination = _character.locationList[_character.nextLocation].position;
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