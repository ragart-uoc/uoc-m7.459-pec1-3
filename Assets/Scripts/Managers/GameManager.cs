using UnityEngine;

namespace PEC1.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> represents the game manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the singleton instance of the class.</value>
        public static GameManager Instance;

        /// <value>Property <c>maxRunners</c> represents the maximum number of runners.</value>
        public int maxRunners = 25;
        
        /// <value>Property <c>runners</c> represents the number of runners.</value>
        public int runners = 8;
        
        /// <value>Property <c>runnerMinSpeed</c> represents the minimum speed of the runners.</value>
        public float runnerMinSpeed = 2.0f;
        
        /// <value>Property <c>runnerMaxSpeed</c> represents the maximum speed of the runners.</value>
        public float runnerMaxSpeed = 5.0f;
        
        /// <value>Property <c>maxElders</c> represents the maximum number of elders.</value>
        public int maxElders = 25;
        
        /// <value>Property <c>elders</c> represents the number of elders.</value>
        public int elders = 8;
        
        /// <value>Property <c>elderMinSpeed</c> represents the minimum speed of the elders.</value>
        public float elderMinSpeed = 0.5f;
        
        /// <value>Property <c>elderMaxSpeed</c> represents the maximum speed of the elders.</value>
        public float elderMaxSpeed = 1.5f;

        /// <value>Property <c>runnerWaypointContainer</c> represents the runner waypoints.</value>
        public GameObject runnerWaypointContainer;
        
        /// <value>Property <c>runnerWaypoints</c> represents the runner waypoints.</value>
        public Transform[] runnerWaypoints;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            runnerWaypoints = new Transform[runnerWaypointContainer.transform.childCount];
            for (var i = 0; i < runnerWaypointContainer.transform.childCount; i++)
            {
                runnerWaypoints[i] = runnerWaypointContainer.transform.GetChild(i);
            }
        }
    }
}
