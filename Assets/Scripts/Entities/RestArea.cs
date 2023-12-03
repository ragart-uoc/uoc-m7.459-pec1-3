using UnityEngine;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>RestArea</c> represents the resting areas.
    /// </summary>
    public class RestArea : MonoBehaviour
    {
        /// <value>Property <c>enterPosition</c> represents the enter position.</value>
        public Transform enterPosition;
        
        /// <value>Property <c>exitPosition</c> represents the exit position.</value>
        public Transform exitPosition;

        /// <value>Property <c>isOccupied</c> represents if the resting area is occupied.</value>
        public bool isOccupied;
        
        /// <value>Property <c>occupant</c> represents the occupant of the resting area.</value>
        public GameObject occupant;
        
        /// <summary>
        /// Method <c>Start</c> invokes the start method.
        /// </summary>
        private void Start()
        {
            isOccupied = false;
            occupant = null;
        }
    }
}
