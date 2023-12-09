using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace M7459.Entities
{
    /// <summary>
    /// Class <c>FlockUnit</c> represents a single unit in a flock.
    /// </summary>
    public class FlockUnit : MonoBehaviour
    {
        /// <value>Property <c>FOVangle</c> represents the field of view angle of the unit.</value>
        [SerializeField]
        private float fovAngle;
        
        /// <value>Property <c>smoothDamp</c> represents the smooth damp of the unit.</value>
        [SerializeField]
        private float smoothDamp;
        
        /// <value>Property <c>obstacleMask</c> represents the mask of the unit's obstacles.</value>
        [SerializeField]
        private LayerMask obstacleMask;
        
        /// <value>Property <c>checkedDirections</c> represents the directions that the unit will check for obstacles.</value>
        [SerializeField]
        private Vector3[] checkedDirections;

        /// <value>Property <c>myTransform</c> represents the transform of the unit.</value>
        private Transform MyTransform { get; set; }

        /// <value>Property <c>_cohesionNeighbours</c> represents the cohesion neighbours of the unit.</value>
        private readonly List<FlockUnit> _cohesionNeighbours = new();
        
        /// <value>Property <c>_avoidanceNeighbours</c> represents the avoidance neighbours of the unit.</value>
        private readonly List<FlockUnit> _avoidanceNeighbours = new();
        
        /// <value>Property <c>_alignmentNeighbours</c> represents the alignment neighbours of the unit.</value>
        private readonly List<FlockUnit> _alignmentNeighbours = new();
        
        /// <value>Property <c>_assignedFlock</c> represents the flock that the unit is assigned to.</value>
        private Flock _flock;
        
        /// <value>Property <c>_currentVelocity</c> represents the current velocity of the unit.</value>
        private Vector3 _currentVelocity;
        
        /// <value>Property <c>_currentObstacleAvoidanceVector</c> represents the current obstacle avoidance vector of the unit.</value>
        private Vector3 _currentObstacleAvoidanceVector;

        /// <value>Property <c>_speed</c> represents the speed of the unit.</value>
        private float _speed;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            MyTransform = transform;
        }

        /// <summary>
        /// Method <c>AssignFlock</c> assigns the unit to a flock.
        /// </summary>
        /// <param name="flock">The flock to assign the unit to.</param>
        public void AssignFlock(Flock flock)
        {
            _flock = flock;
        }
        
        /// <summary>
        /// Method <c>InitializeSpeed</c> initializes the speed of the unit.
        /// </summary>
        public void InitializeSpeed(float speed)
        {
            _speed = speed;
        }
        
        /// <summary>
        /// Method <c>MoveUnit</c> moves the unit.
        /// </summary>
        public void MoveUnit()
        {
            FindNeighbours();
            CalculateSpeed();

            var cohesionVector = CalculateCohesionVector() * _flock.CohesionWeight;
            var avoidanceVector = CalculateAvoidanceVector() * _flock.AvoidanceWeight;
            var alignmentVector = CalculateAlignmentVector() * _flock.AlignmentWeight;
            var boundsVector = CalculateBoundsVector() * _flock.BoundsWeight;
            var obstacleVector = CalculateObstacleVector() * _flock.ObstacleWeight;

            var moveVector = cohesionVector + avoidanceVector + alignmentVector + boundsVector + obstacleVector;
            moveVector = Vector3.SmoothDamp(MyTransform.forward, moveVector, ref _currentVelocity, smoothDamp);
            moveVector = moveVector.normalized * _speed;
            if (moveVector == Vector3.zero)
                moveVector = transform.forward;

            MyTransform.forward = moveVector;
            MyTransform.position += moveVector * Time.deltaTime;
        }

        /// <summary>
        /// Method <c>FindNeighbours</c> finds the neighbours of the unit.
        /// </summary>
        private void FindNeighbours()
        {
            _cohesionNeighbours.Clear();
            _avoidanceNeighbours.Clear();
            _alignmentNeighbours.Clear();
            var allUnits = _flock.Units;
            foreach (var unit in allUnits)
            {
                if (unit == this)
                    continue;
                var distance = Vector3.SqrMagnitude(unit.MyTransform.position - MyTransform.position);
                if (distance <= _flock.CohesionDistance * _flock.CohesionDistance)
                {
                    _cohesionNeighbours.Add(unit);
                }
                if (distance <= _flock.AvoidanceDistance * _flock.AvoidanceDistance)
                {
                    _avoidanceNeighbours.Add(unit);
                }
                if (distance <= _flock.AlignmentDistance * _flock.AlignmentDistance)
                {
                    _alignmentNeighbours.Add(unit);
                }
            }
        }

        /// <summary>
        /// Method <c>CalculateSpeed</c> calculates the speed of the unit.
        /// </summary>
        private void CalculateSpeed()
        {
            if (_cohesionNeighbours.Count == 0)
                return;
            _speed = 0;
            foreach (var unit in _cohesionNeighbours)
            {
                _speed += unit._speed;
            }
            _speed /= _cohesionNeighbours.Count;
            _speed = Mathf.Clamp(_speed, _flock.MinSpeed, _flock.MaxSpeed);
        }
        
        /// <summary>
        /// Method <c>CalculateCohesionVector</c> calculates the cohesion vector for the unit.
        /// </summary>
        /// <returns>The cohesion vector for the unit.</returns>
        private Vector3 CalculateCohesionVector()
        {
            var cohesionVector = Vector3.zero;
            if (_cohesionNeighbours.Count == 0)
                return cohesionVector;
            var neighboursInFOV = 0;
            foreach (var unit in _cohesionNeighbours.Where(unit => IsInFOV(unit.transform.position)))
            {
                neighboursInFOV++;
                cohesionVector += unit.transform.position;
            }
            cohesionVector /= neighboursInFOV;
            cohesionVector -= MyTransform.position;
            cohesionVector = cohesionVector.normalized;
            return cohesionVector;
        }

        /// <summary>
        /// Method <c>CalculateAvoidanceVector</c> calculates the avoidance vector for the unit.
        /// </summary>
        /// <returns>The avoidance vector for the unit.</returns>
        private Vector3 CalculateAvoidanceVector()
        {
            var avoidanceVector = Vector3.zero;
            if (_avoidanceNeighbours.Count == 0)
                return avoidanceVector;
            var neighboursInFOV = 0;
            foreach (var unit in _avoidanceNeighbours.Where(unit => IsInFOV(unit.MyTransform.position)))
            {
                neighboursInFOV++;
                avoidanceVector += (MyTransform.position - unit.MyTransform.position);
            }
            avoidanceVector /= neighboursInFOV;
            avoidanceVector = avoidanceVector.normalized;
            return avoidanceVector;
        }
        
        /// <summary>
        /// Method <c>CalculateAlignmentVector</c> calculates the alignment vector for the unit.
        /// </summary>
        /// <returns>The alignment vector for the unit.</returns>
        private Vector3 CalculateAlignmentVector()
        {
            var alignmentVector = MyTransform.forward;
            if (_alignmentNeighbours.Count == 0)
                return alignmentVector;
            var neighboursInFOV = 0;
            foreach (var unit in _alignmentNeighbours.Where(unit => IsInFOV(unit.MyTransform.position)))
            {
                neighboursInFOV++;
                alignmentVector += unit.MyTransform.forward;
            }
            alignmentVector /= neighboursInFOV;
            alignmentVector = alignmentVector.normalized;
            return alignmentVector;
        }

        /// <summary>
        /// Method <c>CalculateBoundsVector</c> calculates the bounds vector for the unit.
        /// </summary>
        /// <returns>The bounds vector for the unit.</returns>
        private Vector3 CalculateBoundsVector()
        {
            var centerOffset = _flock.transform.position - MyTransform.position;
            var nearCenter = (centerOffset.magnitude >= _flock.BoundsDistance * 0.9f);
            return nearCenter ? centerOffset.normalized : Vector3.zero;
        }
        
        /// <summary>
        /// Method <c>CalculateObstacleVector</c> calculates the obstacle vector for the unit.
        /// </summary>
        /// <returns>The obstacle vector for the unit.</returns>
        private Vector3 CalculateObstacleVector()
        {
            if (Physics.Raycast(MyTransform.position, MyTransform.forward, out _, _flock.ObstacleDistance, obstacleMask))
                return CheckObstacleDirection();
            _currentObstacleAvoidanceVector = Vector3.zero;
            return _currentObstacleAvoidanceVector;
        }

        /// <summary>
        /// Method <c>CheckObstacleDirection</c> finds the best direction to avoid an obstacle.
        /// </summary>
        /// <returns>The best direction to avoid the obstacle.</returns>
        private Vector3 CheckObstacleDirection()
        {
            if (_currentObstacleAvoidanceVector != Vector3.zero && !Physics.Raycast(MyTransform.position,
                    MyTransform.forward, out _, _flock.ObstacleDistance, obstacleMask))
                return _currentObstacleAvoidanceVector;
            float maxDistance = int.MinValue;
            var selectedDirection = Vector3.zero;
            foreach (var checkedDirection in checkedDirections)
            {
                var direction = MyTransform.TransformDirection(checkedDirection.normalized);
                if (Physics.Raycast(MyTransform.position, direction, out var hit, _flock.ObstacleDistance, obstacleMask))
                {
                    var distance = (hit.point - MyTransform.position).sqrMagnitude;
                    if (!(distance > maxDistance))
                        continue;
                    maxDistance = distance;
                    selectedDirection = direction;
                }
                else
                {
                    _currentObstacleAvoidanceVector = direction.normalized;
                    return _currentObstacleAvoidanceVector;
                }
            }
            return selectedDirection.normalized;
        }


        /// <summary>
        /// Method <c>IsInFOV</c> checks if a position is in the unit's field of view.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is in the unit's field of view, false otherwise.</returns>
        private bool IsInFOV(Vector3 position)
        {
            return Vector3.Angle(MyTransform.forward, position - MyTransform.position) <= fovAngle;
        }
    }
}
