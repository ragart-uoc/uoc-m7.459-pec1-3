using UnityEngine;
using M7459.Entities;

namespace M7459.Managers
{
    /// <summary>
    /// Class <c>FlockManager</c> manages flocks of units.
    /// </summary>
    public class FlockManager : MonoBehaviour
    {
        /// <value>Property <c>flockPrefab</c> represents the prefab of the flock that will be spawned.</value>
        [Header ("Flock Settings")]
        [SerializeField]
        private GameObject flockPrefab;

        /// <value>Property <c>size</c> represents the number of flocks.</value>
        [SerializeField]
        private int size;
        
        /// <value>Property <c>spawnBounds</c> represents the bounds in which the flocks will be spawned.</value>
        [SerializeField]
        private Vector3 spawnBounds;
        
        /// <value>Property <c>instancesContainer</c> represents the instances container.</value>
        public GameObject instancesContainer;

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            SpawnFlocks();
        }

        /// <summary>
        /// Method <c>SpawnFlocks</c> spawns the flocks.
        /// </summary>
        private void SpawnFlocks()
        {
            for (var i = 0; i < size; i++)
            {
                var randomVector = Random.insideUnitSphere;
                    randomVector = new Vector3(
                        randomVector.x * spawnBounds.x,
                        randomVector.y * spawnBounds.y,
                        randomVector.z * spawnBounds.z);
                var spawnPosition = transform.position + randomVector;
                    spawnPosition.y = 2; // Patch to ensure the flocks interact with the characters.
                var flock = Instantiate(flockPrefab, spawnPosition, Quaternion.identity);
                // Set the parent of the flock to the instances container.
                flock.transform.SetParent(instancesContainer.transform);
                // Assign the hive to the flock.
                flock.GetComponent<Flock>().AssignHive(this);
            }
        }
    }
}
