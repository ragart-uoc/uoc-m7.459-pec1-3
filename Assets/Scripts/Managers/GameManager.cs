using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using M7459.Entities;
using Random = UnityEngine.Random;

namespace M7459.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> represents the game manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the singleton instance of the class.</value>
        public static GameManager Instance;
        
        /// <value>Property <c>characterStructs</c> represents the character structs.</value>
        public CharacterStruct[] characterStructs;

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
            // Loop through the character structs
            for (var i = 0; i < characterStructs.Length; i++)
            {
                // Get the location list from the container
                characterStructs[i].locationList = new Transform[characterStructs[i].locationContainer.childCount];
                for (var j = 0; j < characterStructs[i].locationContainer.childCount; j++)
                {
                    characterStructs[i].locationList[j] = characterStructs[i].locationContainer.GetChild(j);
                }
                
                // Spawn the characters
                characterStructs[i].instances = Mathf.Clamp(characterStructs[i].instances, 0, characterStructs[i].maxInstances);
                for (var j = 0; j < characterStructs[i].instances; j++)
                {
                    AddCharacterFromStruct(i);
                }
            }
        }

        /// <summary>
        /// Method <c>RestartGame</c> restarts the game.
        /// </summary>
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Method <c>SpawnCharacter</c> spawns a character.
        /// <param name="prefab">The character prefab.</param>
        /// <param name="locationList">The spawn list.</param>
        /// </summary>
        private Character SpawnCharacter(GameObject prefab, Transform[] locationList)
        {
            var randomLocationKey = Random.Range(0, locationList.Length);
            var instance = Instantiate(prefab, 
                locationList[randomLocationKey].position,
                Quaternion.identity);
            var instanceCharacter = instance.GetComponent<Character>();
                instanceCharacter.startingLocation = randomLocationKey;
                instanceCharacter.nextLocation = randomLocationKey;
            return instanceCharacter;
        }
        
        /// <summary>
        /// Method <c>AddCharacterFromStruct</c> adds a character from a struct.
        /// </summary>
        /// <param name="typeIndex">The type index in the struct list.</param>
        private void AddCharacterFromStruct(int typeIndex)
        {
            // Check if the button should be disabled
            if (characterStructs[typeIndex].instances == characterStructs[typeIndex].maxInstances)
                characterStructs[typeIndex].addButton.interactable = false;

            // Spawn the character
            var instance = SpawnCharacter(characterStructs[typeIndex].prefab, characterStructs[typeIndex].locationList);
                instance.locationList = characterStructs[typeIndex].locationList;
                instance.wanderRadius = characterStructs[typeIndex].wanderRadius;
                instance.wanderOffset = characterStructs[typeIndex].wanderOffset;
                instance.wanderStoppingDistance = characterStructs[typeIndex].wanderStoppingDistance;
                instance.restingTime = characterStructs[typeIndex].restingTime;
                instance.maxSpeed = characterStructs[typeIndex].maxSpeed;
                instance.minSpeed = characterStructs[typeIndex].minSpeed;
                instance.canStart = true;
        }

        /// <summary>
        /// Method <c>AddCharacter</c> adds a character.
        /// </summary>
        /// <param name="type">The character type.</param>
        public void AddCharacter(string type)
        {
            // Get the type index
            var typeIndex = Array.FindIndex(characterStructs,
                x => x.characterType.ToString() == type);

            // Check if the type exists
            if (typeIndex < 0)
                return;
            
            // Check if the instances are at the maximum
            if (characterStructs[typeIndex].instances == characterStructs[typeIndex].maxInstances)
                return;
            
            // Increase the instances
            characterStructs[typeIndex].instances++;
            
            // Add the character
            AddCharacterFromStruct(typeIndex);
        }
    }
}
