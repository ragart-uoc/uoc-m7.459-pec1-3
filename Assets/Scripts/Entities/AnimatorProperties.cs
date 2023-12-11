namespace M7459.Entities
{
    /// <summary>
    /// Class <c>AnimatorProperties</c> represents the properties of the animator.
    /// </summary>
    public static class AnimatorProperties
    {
        /// <value>Property <c>Events</c> represents the events of the animator.</value>
        public enum Events
        {
            SittingFinished,
            StandingUpFinished
        }
        
        /// <value>Property <c>Animations</c> represents the animations of the animator.</value>
        public enum Animations
        {
            AirSquad = 1,
            CropJump = 2,
            JumpingJack = 3,
            Pistol = 4
        }
    }
}