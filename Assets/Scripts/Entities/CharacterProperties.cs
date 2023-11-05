namespace PEC1.Entities
{
    /// <summary>
    /// Class <c>CharacterProperties</c> represents the properties of the character.
    /// </summary>
    public static class CharacterProperties
    {
        /// <value>Property <c>Types</c> represents the types of characters.</value>
        public enum Types
        {
            Elder,
            Runner
        }
        
        /// <value>Property <c>States</c> represents the states of characters.</value>
        public enum States
        {
            Idle,
            Resting,
            Patrolling,
            Wandering
        }
    }
}