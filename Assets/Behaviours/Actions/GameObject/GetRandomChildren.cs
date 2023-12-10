using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to obtain a random child of a GameObject.
    /// </summary>
    [Action("M7459/GameObject/GetRandomChildren")]
    [Help("Gets a random child of a GameObject")]
    public class GetRandomChildren : GOAction
    {
        /// <value>Input GameObject containing the children Parameter</value>
        [InParam("ChildrenContainer")]
        [Help("GameObject containing the children")]
        public GameObject ChildrenContainer { get; set; }
        
        /// <value>Input Index of the current child Parameter</value>
        [InParam("ChildIndex")]
        [Help("Index of the current child")]
        public int CurrentChildIndex { get; set; }

        /// <value>Output Index of the next child Parameter</value>
        [OutParam("ChildIndex")]
        [Help("Index of the next child")]
        public int ChildIndex { get; set; }
        
        /// <value>Output GameObject of the next child Parameter</value>
        [OutParam("Child")]
        [Help("GameObject of the next child")]
        public GameObject Child { get; set; }

        /// <summary>Start Method of GetNextChildren</summary>
        /// <remarks>Check if the GameObject is null or has no children.</remarks>
        public override void OnStart()
        {
            if (ChildrenContainer == null)
            {
                Debug.LogError("The GameObject is null", ChildrenContainer);
                return;
            }
            if (ChildrenContainer.transform.childCount == 0)
                Debug.LogError("The GameObject has no children", ChildrenContainer);
        }

        /// <summary>Update Method of GetNextChildren</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            ChildIndex = Random.Range(0, ChildrenContainer.transform.childCount);
            if (ChildIndex == CurrentChildIndex)
                return TaskStatus.RUNNING;
            Child = ChildrenContainer.transform.GetChild(ChildIndex).gameObject;
            return TaskStatus.COMPLETED;
        }
    }
}
