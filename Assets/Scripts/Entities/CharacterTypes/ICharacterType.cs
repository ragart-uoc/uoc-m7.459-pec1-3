using UnityEngine;

namespace M7459.Entities.CharacterTypes
{
    /// <summary>
    /// Interface <c>ICharacterType</c> is the interface for the character types.
    /// </summary>
    public interface ICharacterType
    {
        /// <summary>
        /// Method <c>StartType</c> invokes the type Start method.
        /// </summary>
        void StartType();
        
        /// <summary>
        /// Method <c>UpdateType</c> invokes the type Update method.
        /// </summary>
        void UpdateType();

        /// <summary>
        /// Method <c>Follow</c> invokes the type Follow method.
        /// </summary>
        void Follow();
        
        /// <summary>
        /// Method <c>Idle</c> invokes the type Idle method.
        /// </summary>
        void Idle();
        
        /// <summary>
        /// Method <c>Rest</c> invokes the type Rest method.
        /// </summary>
        void Rest();
        
        /// <summary>
        /// Method <c>Patrol</c> invokes the type Patrol method.
        /// </summary>
        void Patrol();

        /// <summary>
        /// Method <c>Wander</c> invokes the type Wander method.
        /// </summary>
        void Wander();
        
        /// <summary>
        /// Method <c>HandleCollisions</c> invokes the type HandleCollisions method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collision.</param>
        /// <param name="tag">The tag of the collision.</param>
        void HandleCollisions(CollisionProperties.Types type, Collision col, string tag);
        
        /// <summary>
        /// Method <c>HandleTriggers</c> invokes the type HandleTriggers method.
        /// </summary>
        /// <param name="type">The type of collision.</param>
        /// <param name="col">The collider.</param>
        /// <param name="tag">The tag of the collider.</param>
        void HandleTriggers(CollisionProperties.Types type, Collider col, string tag);
    }
}
