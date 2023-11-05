namespace PEC1.Entities.CharacterStates
{
    public class Wandering : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;

        /// <summary>
        /// Class constructor <c>Wandering</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Wandering(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
            _character.agent.isStopped = false;
            Wander();
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            if (_character.agent.remainingDistance <= _character.agent.stoppingDistance)
                Wander();
        }

        /// <summary>
        /// Method <c>Wander</c> makes the character wander.
        /// </summary>
        private void Wander()
        {
            // Get a random position
            var randomPosition =
                _character.RandomNavSphere(_character.transform.position, _character.wanderRadius, -1);
                    
            // Set the destination
            _character.agent.SetDestination(randomPosition);
        }
    }
}