namespace M7459.Entities
{
    /// <summary>
    /// Class <c>CollisionProperties</c> represents the properties of the collisions.
    /// </summary>
    public class CollisionProperties
    {
        /// <value>Property <c>Types</c> represents the types of collisions.</value>
        public enum Types
        {
            CollisionEnter,
            CollisionStay,
            CollisionExit,
            TriggerEnter,
            TriggerStay,
            TriggerExit
        }
    }
}
