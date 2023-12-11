namespace M7459.Entities
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
            Runner,
            PathRunner,
            ZenDummy,
            ZenMaster,
            ZenPerson
        }
        
        /// <value>Property <c>States</c> represents the states of characters.</value>
        public enum States
        {
            Idle,
            Following,
            Resting,
            Patrolling,
            PathPatrolling,
            Wandering
        }
    }
}