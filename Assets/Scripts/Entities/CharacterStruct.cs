using UnityEngine;
using UnityEngine.UI;

namespace M7459.Entities
{
    /// <summary>
    /// Struct <c>CharacterStruct</c> represents the character struct.
    /// </summary>
    [System.Serializable]
    public struct CharacterStruct
    {
        /// <value>Property <c>characterType</c> represents the character type.</value>
        public CharacterProperties.Types characterType;
        
        /// <value>Property <c>prefab</c> represents the prefab.</value>
        public GameObject prefab;
        
        /// <value>Property <c>instances</c> represents the number of instances.</value>
        public int instances;

        /// <value>Property <c>maxInstances</c> represents the maximum number of instances.</value>
        public int maxInstances;

        /// <value>Property <c>wanderRadius</c> represents the wander radius.</value>
        public float wanderRadius;
            
        /// <value>Property <c>restingTime</c> represents the resting time.</value>
        public float restingTime;
        
        /// <value>Property <c>minSpeed</c> represents the minimum speed.</value>
        public float minSpeed;
        
        /// <value>Property <c>maxSpeed</c> represents the maximum speed.</value>
        public float maxSpeed;
        
        /// <value>Property <c>addButton</c> represents the add button.</value>
        public Button addButton;
        
        /// <value>Property <c>locationContainer</c> represents the location container.</value>
        public Transform locationContainer;
        
        /// <value>Property <c>locationList</c> represents the location list.</value>
        public Transform[] locationList;
    }
}
