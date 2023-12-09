using UnityEngine;
using UnityEngine.AI;

namespace M7459.Entities.CharacterStates
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
            if (!_character.agent.hasPath)
                Wander();
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            if (_character.agent.remainingDistance <= _character.wanderStoppingDistance)
                Wander();
        }

        /// <summary>
        /// Method <c>Wander</c> makes the character wander.
        /// </summary>
        private void Wander()
        {
            // Get a random position within the wander radius
            var localTarget = Random.insideUnitSphere * _character.wanderRadius;
            localTarget += new Vector3(0, 0, _character.wanderOffset);
            
            // Get the world position of the target
            var worldTarget = _character.transform.TransformPoint(localTarget);
            worldTarget.y = 0;

            // Get the opposite world position of the target
            var antiWorldTarget = _character.transform.TransformPoint(-localTarget);
            antiWorldTarget.y = 0;

            // Set the destination
            if (NavMesh.SamplePosition(worldTarget, out var navHit, _character.wanderRadius, -1))
                _character.agent.SetDestination(navHit.position);
            else if (NavMesh.SamplePosition(antiWorldTarget, out navHit, _character.wanderRadius, -1))
                _character.agent.SetDestination(navHit.position);
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