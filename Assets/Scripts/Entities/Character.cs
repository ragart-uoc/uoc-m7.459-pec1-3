using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation;
using M7459.Entities.CharacterStates;
using M7459.Entities.CharacterTypes;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>Character</c> is the base class for all characters.
    /// </summary>
    public class Character : MonoBehaviour
    {
        #region Character Types
            
            /// <value>Property <c>characterType</c> represents the character type.</value>
            public CharacterProperties.Types characterType;
            
            /// <value>Property <c>_characterTypes</c> represents the list of character types.</value>
            private readonly Dictionary<CharacterProperties.Types, ICharacterType> _characterTypes = new Dictionary<CharacterProperties.Types, ICharacterType>();
                
            /// <value>Property <c>CurrentType</c> represents the current character type.</value>
            public ICharacterType CurrentType;
            
        #endregion

        #region Character States
            
            /// <value>Property <c>characterState</c> represents the character state.</value>
            public CharacterProperties.States characterState;
            
            /// <value>Property <c>_characterStates</c> represents the list of character states.</value>
            public readonly Dictionary<CharacterProperties.States, ICharacterState> CharacterStates = new Dictionary<CharacterProperties.States, ICharacterState>();
                
            /// <value>Property <c>CurrentState</c> represents the current character state.</value>
            public ICharacterState CurrentState;
            
        #endregion
        
        #region Component references
        
            /// <value>Property <c>animator</c> represents the character animator.</value>
            [Header("Components")]
            public Animator animator;
                
            /// <value>Property <c>agent</c> represents the character nav mesh agent.</value>
            public NavMeshAgent agent;
        
        #endregion
        
        #region Character Settings

            /// <value>Property <c>autoStart</c> represents if the character should auto start.</value>
            private bool _started;
            
            /// <value>Property <c>canStart</c> represents if the character can start.</value>
            [HideInInspector]
            public bool canStart;

            /// <value>Property <c>wanderRadius</c> represents the wander radius.</value>
            [HideInInspector]
            public float wanderRadius;
            
            /// <value>Property <c>wanderOffset</c> represents the wander offset.</value>
            [HideInInspector]
            public float wanderOffset;
            
            /// <value>Property <c>wanderStoppingDistance</c> represents the wander stopping distance.</value>
            [HideInInspector]
            public float wanderStoppingDistance;
            
            /// <value>Property <c>restingTime</c> represents the resting time.</value>
            [HideInInspector]
            public float restingTime;

            /// <value>Property <c>minSpeed</c> represents the minimum speed.</value>
            [HideInInspector]
            public float minSpeed;

            /// <value>Property <c>maxSpeed</c> represents the maximum speed.</value>
            [HideInInspector]
            public float maxSpeed;
            
            /// <value>Property <c>patrolDirection</c> represents the patrol direction.</value>
            [HideInInspector]
            public bool patrolDirection;
        
        #endregion
        
        #region Navigation

            /// <value>Property <c>locationList</c> represents the character location list.</value>
            [HideInInspector]
            public Transform[] locationList;

            /// <value>Property <c>startingLocation</c> represents the character starting location.</value>
            [HideInInspector]
            public int startingLocation;
        
            /// <value>Property <c>nextLocation</c> represents the character next location.</value>
            [HideInInspector]
            public int nextLocation;
            
            /// <value>Property <c>restAreaEnterPosition</c> represents the character rest area enter position.</value>
            public Transform restAreaEnterPosition;
            
            /// <value>Property <c>restAreaExitPosition</c> represents the character rest area exit position.</value>
            public Transform restAreaExitPosition;

            /// <value>Property <c>pathCreator</c> represents the character path creator.</value>
            [HideInInspector]
            public PathCreator pathCreator;
            
            /// <value>Property <c>endOfPathInstruction</c> represents the character end of path instruction.</value>
            [HideInInspector]
            public EndOfPathInstruction endOfPathInstruction;
            
        #endregion
            
        #region Animator References
        
            /// <value>Property <c>AnimatorSpeed</c> represents the character speed animation.</value>
            public readonly int AnimatorSpeed = Animator.StringToHash("Speed");
            
            /// <value>Property <c>AnimatorSitting</c> represents the character sitting animation.</value>
            public readonly int AnimatorSitting = Animator.StringToHash("Sitting");
        
        #endregion

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Get the character types
            _characterTypes.Add(CharacterProperties.Types.Elder, new Elder(this));
            _characterTypes.Add(CharacterProperties.Types.Runner, new Runner(this));
            _characterTypes.Add(CharacterProperties.Types.PathRunner, new PathRunner(this));
            _characterTypes.Add(CharacterProperties.Types.ZenMaster, new ZenMaster(this));
            _characterTypes.Add(CharacterProperties.Types.ZenPerson, new ZenPerson(this));
            _characterTypes.Add(CharacterProperties.Types.ZenDummy, new ZenDummy(this));
            
            // Get the character states
            CharacterStates.Add(CharacterProperties.States.Idle, new Idle(this));
            CharacterStates.Add(CharacterProperties.States.Resting, new Resting(this));
            CharacterStates.Add(CharacterProperties.States.Patrolling, new Patrolling(this));
            CharacterStates.Add(CharacterProperties.States.PathPatrolling, new PathPatrolling(this));
            CharacterStates.Add(CharacterProperties.States.Wandering, new Wandering(this));
            
            // Set the current type and state
            CurrentType = _characterTypes[characterType];
            CurrentState = CharacterStates[characterState];
        }

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            // Get the components
            animator ??= GetComponent<Animator>();
            agent ??= GetComponent<NavMeshAgent>();

            // Find the path creator
            pathCreator ??= FindObjectOfType<PathCreator>();
        }

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            switch (_started)
            {
                // Check for delayed start
                case false when !canStart:
                    return;
                case false when canStart:
                    CurrentType.StartType();
                    _started = true;
                    break;
                // Invoke the current type Update method
                default:
                    CurrentType.UpdateType();
                    break;
            }
        }

        /// <summary>
        /// Method <c>GetNextItemInList</c> returns the next item in a list.
        /// </summary>
        /// <param name="index">The current index.</param>
        /// <param name="listLength">The length of the list.</param>
        /// <param name="direction">The direction of the list.</param>
        /// <returns>The next item in the list.</returns>
        private int GetNextItemInList(int index, int listLength, bool direction)
        {
            return direction
                ? (index + 1) % listLength
                : (index - 1 + listLength) % listLength;
        }
        
        /// <summary>
        /// Method <c>SetNextLocation</c> sets the next location.
        /// </summary>
        public void SetNextLocation()
        {
            nextLocation = GetNextItemInList(
                nextLocation,
                locationList.Length,
                patrolDirection);
        }
        
        #region Collisions

            /// <summary>
            /// Method <c>OnCollisionEnter</c> is called when the character enters a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionEnter(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisions(CollisionProperties.Types.CollisionEnter, col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnCollisionStay</c> is called when the character stays in a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionStay(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisions(CollisionProperties.Types.CollisionStay, col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnCollisionExit</c> is called when the character exits a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionExit(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisions(CollisionProperties.Types.CollisionExit, col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerEnter</c> is called when the character enters a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerEnter(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggers(CollisionProperties.Types.TriggerEnter, col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerStay</c> is called when the character stays in a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerStay(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggers(CollisionProperties.Types.TriggerStay, col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerExit</c> is called when the character exits a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerExit(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggers(CollisionProperties.Types.TriggerExit, col, transform.tag);
            }

        #endregion
        
        #region Animator Events

            /// <summary>
            /// Method <c>SittingFinished</c> is called when the character finishes sitting.
            /// </summary>
            public void SittingFinished()
            {
                CurrentState.HandleAnimations(AnimatorProperties.Events.SittingFinished);
            }

            /// <summary>
            /// Method <c>StandingUpFinished</c> is called when the character finishes standing up.
            /// </summary>
            public void StandingUpFinished()
            {
                CurrentState.HandleAnimations(AnimatorProperties.Events.StandingUpFinished);
            }
            
        #endregion
        
    }
}
