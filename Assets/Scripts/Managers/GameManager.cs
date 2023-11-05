using UnityEngine;
using PEC1.Entities;

namespace PEC1.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> represents the game manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the singleton instance of the class.</value>
        public static GameManager Instance;
        
        /// <value>Property <c>runnerPrefab</c> represents the runner prefab.</value>
        public GameObject runnerPrefab;

        /// <value>Property <c>maxRunners</c> represents the maximum number of runners.</value>
        public int maxRunners = 25;
        
        /// <value>Property <c>runners</c> represents the number of runners.</value>
        public int runners = 8;
        
        /// <value>Property <c>runnerMinSpeed</c> represents the minimum speed of the runners.</value>
        public float runnerMinSpeed = 2.0f;
        
        /// <value>Property <c>runnerMaxSpeed</c> represents the maximum speed of the runners.</value>
        public float runnerMaxSpeed = 5.0f;
        
        /// <value>Property <c>elderPrefab</c> represents the elder prefab.</value>
        public GameObject elderPrefab;

        /// <value>Property <c>elderSpawnAreasContainer</c> represents the elder spawn areas container.</value>
        public Transform elderSpawnAreasContainer;
        
        /// <value>Property <c>elderSpawnAreas</c> represents the elder spawn areas.</value>
        public Transform[] elderSpawnAreas;
        
        /// <value>Property <c>maxElders</c> represents the maximum number of elders.</value>
        public int maxElders = 25;
        
        /// <value>Property <c>elders</c> represents the number of elders.</value>
        public int elders = 8;
        
        /// <value>Property <c>elderMinSpeed</c> represents the minimum speed of the elders.</value>
        public float elderMinSpeed = 1f;
        
        /// <value>Property <c>elderMaxSpeed</c> represents the maximum speed of the elders.</value>
        public float elderMaxSpeed = 2f;

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
            // Get the elder spawn areas
            elderSpawnAreas = new Transform[elderSpawnAreasContainer.transform.childCount];
            for (var i = 0; i < elderSpawnAreasContainer.transform.childCount; i++)
            {
                elderSpawnAreas[i] = elderSpawnAreasContainer.transform.GetChild(i);
            }
            
            // Spawn the elders
            elders = Mathf.Clamp(elders, 0, maxElders);
            for (var i = 0; i < elders; i++)
            {
                SpawnElder();
            }

            // Get the runner waypoints
            runnerWaypoints = new Transform[runnerWaypointContainer.transform.childCount];
            for (var i = 0; i < runnerWaypointContainer.transform.childCount; i++)
            {
                runnerWaypoints[i] = runnerWaypointContainer.transform.GetChild(i);
            }

            // Spawn the runners
            runners = Mathf.Clamp(runners, 0, maxRunners);
            for (var i = 0; i < runners; i++)
            {
                SpawnRunner();
            }
        }

        /// <summary>
        /// Method <c>SpawnCharacterRandomList</c> spawns a character in a random position of a list.
        /// </summary>
        /// <param name="prefab">The character prefab.</param>
        /// <param name="list">The spawn list.</param>
        /// <returns>The character instance.</returns>
        public GameObject SpawnCharacterRandomList(GameObject prefab, Transform[] list)
        {
            // Spawn the character in the position of a random Transform in the list
            var instance = Instantiate(prefab, 
                list[Random.Range(0, list.Length)].position,
                Quaternion.identity);
            return instance;
        }
        
        /// <summary>
        /// Method <c>SpawnCharacterKeyList</c> spawns a character in a position of a list.
        /// </summary>
        /// <param name="prefab">The character prefab.</param>
        /// <param name="list">The spawn list.</param>
        /// <param name="key">The key of the list.</param>
        /// <returns>The character instance.</returns>
        public GameObject SpawnCharacterKeyList(GameObject prefab, Transform[] list, int key)
        {
            // Spawn the character in the position of a random Transform in the list
            var instance = Instantiate(prefab, 
                list[key].position,
                Quaternion.identity);
            return instance;
        }

        /// <summary>
        /// Method <c>SpawnElder</c> spawns an elder.
        /// </summary>
        public void SpawnElder()
        {
            SpawnCharacterRandomList(elderPrefab, elderSpawnAreas);
        }

        /// <summary>
        /// Method <c>SpawnRunner</c> spawns a runner.
        /// </summary>
        public void SpawnRunner()
        {
            var randomWaypointKey = Random.Range(0, runnerWaypoints.Length);
            var instance = SpawnCharacterKeyList(runnerPrefab, runnerWaypoints, randomWaypointKey);
            var instanceCharacter = instance.GetComponent<Character>();
            
            // Set the starting waypoint
            instanceCharacter.startingWayPoint = randomWaypointKey;
            instanceCharacter.nextWayPoint = randomWaypointKey;

            // Randomize the agent speed
            instanceCharacter.agent.speed = Random.Range(runnerMinSpeed, runnerMaxSpeed);
            instanceCharacter.agent.acceleration = instanceCharacter.agent.speed * 2f;
            
            // Randomize the agent direction
            instanceCharacter.patrolDirection = Random.value > 0.5f;
        }
        
        /// <summary>
        /// Method <c>GetNextItemInList</c> returns the next item in a list.
        /// </summary>
        /// <param name="index">The current index.</param>
        /// <param name="listLength">The length of the list.</param>
        /// <param name="direction">The direction of the list.</param>
        /// <returns>The next item in the list.</returns>
        public int GetNextItemInList(int index, int listLength, bool direction)
        {
            return direction
                ? (index + 1) % listLength
                : (index - 1 + listLength) % listLength;
        }
    }
}
