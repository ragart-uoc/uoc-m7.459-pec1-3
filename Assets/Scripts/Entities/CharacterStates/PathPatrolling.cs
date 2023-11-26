using UnityEngine;

namespace M7459.Entities.CharacterStates
{
    public class PathPatrolling : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;
        
        /// <value>Property <c>_distanceTravelled</c> represents the distance travelled.</value>
        private float _distanceTravelled;

        /// <summary>
        /// Class constructor <c>PathPatrolling</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public PathPatrolling(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
            if (_character.pathCreator != null)
            {
                _distanceTravelled = _character.pathCreator.path.GetClosestDistanceAlongPath(_character.transform.position);
                _character.agent.destination = _character.pathCreator.path.GetPointAtDistance(_distanceTravelled, _character.endOfPathInstruction);
            };
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            if (_character.agent.remainingDistance > _character.agent.stoppingDistance)
            {
                _distanceTravelled = _character.pathCreator.path.GetClosestDistanceAlongPath(_character.transform.position); 
                _character.agent.destination = _character.pathCreator.path.GetPointAtDistance(_distanceTravelled, _character.endOfPathInstruction);
            }
            else
            {
                _character.agent.isStopped = true;
                if (_character.pathCreator != null)
                {
                    _character.animator.SetFloat(_character.AnimatorSpeed, _character.agent.speed);
                    _distanceTravelled += _character.agent.speed * Time.deltaTime;
                    _character.transform.position = _character.pathCreator.path.GetPointAtDistance(_distanceTravelled, _character.endOfPathInstruction);
                    _character.transform.rotation = _character.pathCreator.path.GetRotationAtDistance(_distanceTravelled, _character.endOfPathInstruction);
                }        
            }
        }
    }
}