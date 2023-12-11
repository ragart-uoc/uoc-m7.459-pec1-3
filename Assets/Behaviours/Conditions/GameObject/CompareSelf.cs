using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// Class <c>CompareSelf</c> is a condition to check whether the GameObject is the same as the target.
    /// </summary>
    [Condition("Basic/CompareSelf")]
    [Help("Checks whether the GameObject is the same as the target")]
    public class CompareSelf : GOCondition
    {
        
        /// <value>Property <c>Target</c> represents the GameObject to be compared.</value>
        [InParam("Target")]
        [Help("GameObject to be compared")]
        public GameObject Target { get; set; }

        /// <summary>
        /// Checks whether two booleans have the same value.
        /// </summary>
        /// <returns>the value of compare first boolean with the second boolean.</returns>
        public override bool Check()
        {
            return gameObject == Target;
        }
    }
}