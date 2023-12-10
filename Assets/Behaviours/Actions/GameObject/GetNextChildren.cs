using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// Class <c>GetNextChildren</c> is an action to get the next child of a GameObject.
    /// </summary>
    [Action("M7459/GameObject/GetNextChildren")]
    [Help("Gets the next child of a GameObject")]
    public class GetNextChildren : GOAction
    {
        /// <value>Property <c>ChildrenContainer</c> represents the GameObject containing the children.</value>
        [InParam("ChildrenContainer")]
        [Help("GameObject containing the children")]
        public GameObject ChildrenContainer { get; set; }
        
        /// <value>Property <c>CurrentChildIndex</c> represents the index of the current child.</value>
        [InParam("ChildIndex")]
        [Help("Index of the current child")]
        public int CurrentChildIndex { get; set; }

        /// <value>Property <c>ChildIndex</c> represents the index of the next child.</value>
        [OutParam("ChildIndex")]
        [Help("Index of the next child")]
        public int ChildIndex { get; set; }
        
        /// <value>Property <c>Child</c> represents the GameObject of the next child.</value>
        [OutParam("Child")]
        [Help("GameObject of the next child")]
        public GameObject Child { get; set; }

        /// <summary>
        /// Method <c>OnStart</c> is called at the beginning of the task execution.
        /// </summary>
        /// <remarks>Get the next child of the GameObject.</remarks>
        public override void OnStart()
        {
            if (ChildrenContainer == null)
            {
                Debug.LogError("The children container is null", ChildrenContainer);
                return;
            }
            if (ChildrenContainer.transform.childCount == 0)
            {
                Debug.LogError("The container has no children", ChildrenContainer);
            }
            ChildIndex = (CurrentChildIndex + 1) % ChildrenContainer.transform.childCount;
            Child = ChildrenContainer.transform.GetChild(ChildIndex).gameObject;
        }

        /// <summary>
        /// Method <c>OnUpdate</c> is called on every iteration of the task execution.
        /// </summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
