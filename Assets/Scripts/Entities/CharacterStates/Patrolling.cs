using PEC1.Managers;

namespace PEC1.Entities.CharacterStates
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
            _character.SetNextWayPoint();
            _character.agent.destination = GameManager.Instance.runnerWaypoints[_character.nextWayPoint].position;
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            if (_character.agent.pathPending || _character.agent.remainingDistance > _character.agent.stoppingDistance)
                return;
            // Define the next way point depending on the direction
            _character.SetNextWayPoint();
            _character.agent.destination = GameManager.Instance.runnerWaypoints[_character.nextWayPoint].position;
        }
    }
}