using System.Collections.Generic;
using UnityEngine;

namespace PEC1.Entities.CharacterTypes
{
    /// <summary>
    /// Interface <c>ICharacterType</c> is the interface for the character types.
    /// </summary>
    public interface ICharacterType
    {
        /// <value>Property <c>TargetTags</c> represents the tags of the targets.</value>
        List<string> TargetTags { get; set; }

        /// <summary>
        /// Method <c>StartType</c> invokes the type Start method.
        /// </summary>
        void StartType();
        
        /// <summary>
        /// Method <c>UpdateType</c> invokes the type Update method.
        /// </summary>
        void UpdateType();
        
        /// <summary>
        /// Method <c>Rest</c> invokes the type Rest method.
        /// </summary>
        void Rest();
        
        /// <summary>
        /// Method <c>StopResting</c> invokes the type StopResting method.
        /// </summary>
        void StopResting();
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        void Patrol();

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        void Wander();
        
        /// <summary>
        /// Method <c>HandleCollisionEnter</c> invokes the type OnCollisionEnter method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleCollisionEnter(Collision col, string tag);
        
        /// <summary>
        /// Method <c>HandleCollisionStay</c> invokes the type OnCollisionStay method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleCollisionStay(Collision col, string tag);
        
        /// <summary>
        /// Method <c>HandleCollisionExit</c> invokes the type OnCollisionExit method.
        /// </summary>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleCollisionExit(Collision col, string tag);
        
        /// <summary>
        /// Method <c>HandleTriggerEnter</c> invokes the type OnTriggerEnter method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleTriggerEnter(Collider col, string tag);
        
        /// <summary>
        /// Method <c>HandleTriggerStay</c> invokes the type OnTriggerStay method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleTriggerStay(Collider col, string tag);
        
        /// <summary>
        /// Method <c>HandleTriggerExit</c> invokes the type OnTriggerExit method.
        /// </summary>
        /// <param name="col">The other collider.</param>
        /// <param name="tag">The tag of the game object containing the collider.</param>
        void HandleTriggerExit(Collider col, string tag);
    }
}
