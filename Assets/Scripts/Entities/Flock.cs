using UnityEngine;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>Flock</c> is the base class for a flock of units.
    /// </summary>
    public class Flock : MonoBehaviour
    {
        /// <value>Property <c>unitPrefab</c> represents the prefab of the unit that will be spawned.</value>
        [Header("Spawn Settings")]
        [SerializeField]
        private GameObject unitPrefab;

        /// <value>Property <c>size</c> represents the number of units in the flock.</value>
        [SerializeField]
        private int size;

        /// <value>Property <c>spawnBounds</c> represents the bounds in which the flock will be spawned.</value>
        [SerializeField]
        private Vector3 spawnBounds;
        
        /// <value>Property <c>minSpeed</c> represents the minimum speed of the units in the flock.</value>
        [Header("Speed Settings")]
        [Range(0, 10)]
        [SerializeField]
        private float minSpeed;
        public float MinSpeed => minSpeed;
        
        /// <value>Property <c>maxSpeed</c> represents the maximum speed of the units in the flock.</value>
        [Range(0, 10)]
        [SerializeField]
        private float maxSpeed;
        public float MaxSpeed => maxSpeed;
        
        /// <value>Property <c>cohesionDistance</c> represents the distance at which units will be considered neighbours.</value>
        [Header("Detection Distance Settings")]
        [Range(0, 10)]
        [SerializeField]
        private float cohesionDistance;
        public float CohesionDistance => cohesionDistance;
        
        /// <value>Property <c>avoidanceDistance</c> represents the distance at which units will avoid each other.</value>
        [Range(0, 10)]
        [SerializeField]
        private float avoidanceDistance;
        public float AvoidanceDistance => avoidanceDistance;
        
        /// <value>Property <c>alignmentDistance</c> represents the distance at which units will align with each other.</value>
        [Range(0, 10)]
        [SerializeField]
        private float alignmentDistance;
        public float AlignmentDistance => alignmentDistance;
        
        /// <value>Property <c>boundsDistance</c> represents the distance at which units will avoid the bounds.</value>
        [Range(0, 10)]
        [SerializeField]
        private float boundsDistance;
        public float BoundsDistance => boundsDistance;
        
        /// <value>Property <c>obstacleDistance</c> represents the distance at which units will avoid obstacles.</value>
        [Range(0, 10)]
        [SerializeField]
        private float obstacleDistance;
        public float ObstacleDistance => obstacleDistance;
        
        /// <value>Property <c>cohesionWeight</c> represents the weight of cohesion.</value>
        [Header("Behaviour Weight Settings")]
        [Range(0, 10)]
        [SerializeField]
        private float cohesionWeight;
        public float CohesionWeight => cohesionWeight;
        
        /// <value>Property <c>avoidanceWeight</c> represents the weight of avoidance.</value>
        [Range(0, 10)]
        [SerializeField]
        private float avoidanceWeight;
        public float AvoidanceWeight => avoidanceWeight;
        
        /// <value>Property <c>alignmentWeight</c> represents the weight of alignment.</value>
        [Range(0, 10)]
        [SerializeField]
        private float alignmentWeight;
        public float AlignmentWeight => alignmentWeight;
        
        /// <value>Property <c>boundsWeight</c> represents the weight of bounds.</value>
        [Range(0, 10)]
        [SerializeField]
        private float boundsWeight;
        public float BoundsWeight => boundsWeight;
        
        /// <value>Property <c>obstacleWeight</c> represents the weight of obstacles.</value>
        [Range(0, 100)]
        [SerializeField]
        private float obstacleWeight;
        public float ObstacleWeight => obstacleWeight;

        /// <value>Property <c>Units</c> represents the units in the flock.</value>
        public FlockUnit[] Units { get; set; }

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start() {
            SpawnUnits();
        }

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            foreach (var unit in Units)
            {
                unit.MoveUnit();
            }
        }

        /// <summary>
        /// Method <c>SpawnUnits</c> spawns the units in the flock.
        /// </summary>
        private void SpawnUnits() {
            Units = new FlockUnit[size];

            for (var i = 0; i < size; i++) {
                var randomVector = Random.insideUnitSphere;
                randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
                var spawnPosition = transform.position + randomVector;
                var spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Units[i] = Instantiate(unitPrefab, spawnPosition, spawnRotation).GetComponent<FlockUnit>();
                Units[i].AssignFlock(this);
                Units[i].InitializeSpeed(Random.Range(minSpeed, maxSpeed));
            }
        }

    }
}
