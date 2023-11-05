using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PEC1.Entities.CharacterTypes;
using PEC1.Entities.CharacterStates;

namespace PEC1.Entities
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

            /// <value>Property <c>wanderRadius</c> represents the wander radius.</value>
            [Header("Settings")]
            public float wanderRadius = 10f;
            
            /// <value>Property <c>restingTime</c> represents the resting time.</value>
            public float restingTime = 5f;
            
            /// <value>Property <c>patrolDirection</c> represents the patrol direction.</value>
            public bool patrolDirection;
        
        #endregion
        
        #region Navigation
        
            /// <value>Property <c>_nextWayPoint</c> represents the character next way point.</value>
            public int nextWayPoint;
            
        #endregion
            
        #region Animator References
        
            /// <value>Property <c>AnimatorSpeed</c> represents the character speed animation.</value>
            public readonly int AnimatorSpeed = Animator.StringToHash("Speed");
        
        #endregion

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Get the character types
            _characterTypes.Add(CharacterProperties.Types.Elder, new Elder(this));
            _characterTypes.Add(CharacterProperties.Types.Runner, new Runner(this));
            
            // Get the character states
            CharacterStates.Add(CharacterProperties.States.Idle, new Idle(this));
            CharacterStates.Add(CharacterProperties.States.Resting, new Resting(this));
            CharacterStates.Add(CharacterProperties.States.Patrolling, new Patrolling(this));
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
            if (animator == null)
                animator = GetComponent<Animator>();
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();
            
            // Invoke the current type Start method
            CurrentType.StartType();
        }

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            // Invoke the current type Update method
            CurrentType.UpdateType();
        }
        
        #region Collisions

            /// <summary>
            /// Method <c>OnCollisionEnter</c> is called when the character enters a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionEnter(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisionEnter(col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnCollisionStay</c> is called when the character stays in a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionStay(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisionStay(col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnCollisionExit</c> is called when the character exits a collision.
            /// </summary>
            /// <param name="col">The collision.</param>
            private void OnCollisionExit(Collision col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleCollisionExit(col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerEnter</c> is called when the character enters a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerEnter(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggerEnter(col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerStay</c> is called when the character stays in a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerStay(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggerStay(col, transform.tag);
            }
            
            /// <summary>
            /// Method <c>OnTriggerExit</c> is called when the character exits a trigger.
            /// </summary>
            /// <param name="col">The other collider.</param>
            private void OnTriggerExit(Collider col)
            {
                if (col.transform.parent != transform)
                    CurrentType.HandleTriggerExit(col, transform.tag);
            }

        #endregion
        
        #region Navigating
        
            /// <summary>
            /// Method <c>RandomNavSphere</c> returns a random position on the navmesh.
            /// </summary>
            /// <param name="origin">The origin position.</param>
            /// <param name="distance">The distance from the origin position.</param>
            /// <param name="layermask">The layermask the navmesh is on.</param>
            /// <returns></returns>
            public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
            {
                var randomDirection = Random.insideUnitSphere * distance;
                randomDirection += origin;
                NavMesh.SamplePosition(randomDirection, out var navHit, distance, layermask);
                return navHit.position;
            }
        
        #endregion
        
    }
}
