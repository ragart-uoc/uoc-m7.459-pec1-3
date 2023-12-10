using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action for calling a method with arguments in a particular game object.
    /// </summary>
    [Action("M7459/GameObject/SendMessageWithArgs")]
    [Help("Calls a method with arguments in a particular game object")]
    public class SendMessageWithArgs : GOAction
    {
        ///<value>Input Name of the method that must be called Parameter.</value>
        [InParam("MethodName")]
        [Help("Name of the method that must be called")]
        public string MethodName;

        ///<value>Input Game object to send the message Parameter.</value>
        [InParam("GameObject")]
        [Help("GameObject receiving the message")]
        public GameObject GameObject;
        
        ///<value>Input Arguments of the method Parameter.</value>
        [InParam("Arguments")]
        [Help("Arguments of the method")]
        public object[] Arguments;

        /// <summary>Start Method of SendMessageWithArgs.</summary>
        /// <remarks>If targetGameObject not null calls the method named methodName on every MonoBehaviour in that game object.</remarks>
        public override void OnStart()
        {
            GameObject ??= gameObject;
            GameObject.SendMessage(MethodName, Arguments);
        }

        /// <summary>Method of Update of SendMessage.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}