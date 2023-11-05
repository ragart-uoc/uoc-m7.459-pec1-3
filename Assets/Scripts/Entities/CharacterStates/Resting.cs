using UnityEngine;

namespace PEC1.Entities.CharacterStates
{
    public class Resting : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;
        
        private float _restingTime;

        /// <summary>
        /// Class constructor <c>Resting</c> initializes the class.
        /// </summary>
        /// <param name="character">The character.</param>
        public Resting(Character character)
        {
            _character = character;
        }

        /// <summary>
        /// Method <c>StartState</c> invokes the state Start method.
        /// </summary>
        public void StartState()
        {
            _character.agent.isStopped = true;
            _restingTime = _character.restingTime;
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            _restingTime -= Time.deltaTime;
            if (_restingTime <= 0f)
                _character.CurrentType.Wander();
        }
    }
}