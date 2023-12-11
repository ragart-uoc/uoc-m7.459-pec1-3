using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>MoveAwayFromGameObject</c> is an action to move away from a given target using a NavMeshAgent.
    /// </summary>
    [Action("Navigation/MoveAwayFromGameObject")]
    [Help("Moves the GameObject away from a given target by using a NavMeshAgent")]
    public class MoveAwayFromGameObject : GOAction
    {
        /// <value>Property <c>target</c> represents the GameObject moving away from.</value>
        [InParam("Target")]
        [Help("GameObject moving away from")]
        public GameObject Target { get; set; }

        /// <value>Property <c>_navAgent</c> represents the NavMeshAgent of the GameObject.</value>
        private NavMeshAgent _navAgent;
        
        /// <value>Property <c>_transform</c> represents the Transform of the GameObject.</value>
        private Transform _transform;

        /// <value>Property <c>_targetTransform</c> represents the Transform of the target GameObject.</value>
        private Transform _targetTransform;

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Check if GameObject and NavMeshAgent exist.</remarks>
        public override void OnStart()
        {
            _transform = gameObject.transform;
            var transformPosition = _transform.position;

            if (Target == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }
            _targetTransform = Target.transform;

            _navAgent = gameObject.GetComponent<NavMeshAgent>();
            if (_navAgent == null)
            {
                Debug.LogError("The NavMeshAgent is null", gameObject);
                return;
            }
            
            var direction = (transformPosition - _targetTransform.position).normalized;
                direction = Quaternion.AngleAxis(Random.Range(0, 179), Vector3.up) * direction;
            _navAgent.SetDestination(transformPosition - (direction * 10));
            
            #if UNITY_5_6_OR_NEWER
                _navAgent.isStopped = false;
            #else
                navAgent.Resume();
            #endif
        }

        /// <summary>Method of Update of MoveAwayFromGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
                return TaskStatus.COMPLETED;
            return TaskStatus.RUNNING;
        }

        /// <summary>Abort method of MoveAwayFromGameObject.</summary>
        /// <remarks>Check if there is a NavMeshAgent, if it exists, the movement is stopped.</remarks>
        public override void OnAbort()
        {
            if (_navAgent != null)
            {
                #if UNITY_5_6_OR_NEWER
                    _navAgent.isStopped = true;
                #else
                    navAgent.Stop();
                #endif
            }
        }
    }
}