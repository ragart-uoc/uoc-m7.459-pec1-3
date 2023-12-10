using UnityEngine;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>Animal</c> is the base class for all animals.
    /// </summary>
    public class Animal : MonoBehaviour
    {
        #region Animator References
        
            /// <summary>Property <c>_animator</c> represents the animal animator.</summary>
            private Animator _animator;
        
            /// <value>Property <c>_animatorSpeed</c> represents the animal speed animation.</value>
            private int _animatorSpeed = Animator.StringToHash("Speed");
        
        # endregion
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        /// </summary>
        public void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Method <c>SetAnimatorSpeed</c> sets the animal speed.
        /// </summary>
        /// <param name="speed">The speed to set.</param>
    }
}
