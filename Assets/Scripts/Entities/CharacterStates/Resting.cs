using UnityEngine;

namespace M7459.Entities.CharacterStates
{
    public class Resting : ICharacterState
    {
        /// <value>Property <c>Character</c> represents the character.</value>
        private readonly Character _character;
        
        /// <value>Property <c>_restingTime</c> represents the resting time.</value>
        private float _restingTime;
        
        /// <value>Property <c>_restingState</c> represents the resting state.</value>
        private enum RestingState
        {
            Resting,
            EnteringRestArea,
            ExitingRestArea
        }
        
        /// <value>Property <c>_restingState</c> represents the resting state.</value>
        private RestingState _restingState;

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
            _restingTime = _character.restingTime;
            EnterArea();
        }

        /// <summary>
        /// Method <c>UpdateState</c> invokes the state Update method.
        /// </summary>
        public void UpdateState()
        {
            switch (_restingState)
            {
                case RestingState.EnteringRestArea:

                    // Check if the character has arrived
                    if (_character.agent.remainingDistance > _character.agent.stoppingDistance)
                        break;
                    
                    // Stop the agent
                    _character.agent.isStopped = true;
                    
                    // Rotate towards the exit position
                    var direction = (_character.restAreaExitPosition.position - _character.transform.position).normalized;
                    var rotation = Quaternion.LookRotation(direction);
                    var angle = Quaternion.Angle(_character.transform.rotation, rotation);
                    if (angle > 5f)
                    {
                        _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, rotation, Time.deltaTime * 2f);
                        break;
                    }
                    
                    // Sit
                    _character.animator.SetBool(_character.AnimatorSitting, true);
                    
                    break;

                case RestingState.Resting:

                    // Check if the character has rested enough
                    _restingTime -= Time.deltaTime;
                    if (_restingTime > 0f)
                        return;
                    
                    // Stand up
                    _character.animator.SetBool(_character.AnimatorSitting, false);

                    break;

                case RestingState.ExitingRestArea:
                    
                    
                    
                    break;
            }
        }

        /// <summary>
        /// Method <c>EnterArea</c> is called when the character enters the rest area.
        /// </summary>
        private void EnterArea()
        {
            // Move to the rest position
            _character.agent.SetDestination(_character.restAreaEnterPosition.position);
            
            // Change the state
            _restingState = RestingState.EnteringRestArea;
        }

        /// <summary>
        /// Method <c>Rest</c> is called when the character starts resting.
        /// </summary>
        private void Rest()
        {   
            // Change the state
            _restingState = RestingState.Resting;
        }
        
        /// <summary>
        /// Method <c>ExitArea</c> is called when the character exits the rest area.
        /// </summary>
        private void ExitArea()
        {
            // Start the agent
            _character.agent.isStopped = false;

            // Move to the exit position
            _character.agent.SetDestination(_character.restAreaExitPosition.position);

            // Change the state
            _restingState = RestingState.ExitingRestArea;
        }
        
        /// <summary>
        /// Method <c>HandleAnimations</c> invokes the state HandleAnimations method.
        /// <param name="animationEvent">The animation event.</param>
        /// </summary>
        public void HandleAnimations(AnimatorProperties.Events animationEvent)
        {
            switch (animationEvent)
            {
                case AnimatorProperties.Events.SittingFinished:
                    Rest();
                    break;
                case AnimatorProperties.Events.StandingUpFinished:
                    ExitArea();
                    break;
            }
        }
    }
}