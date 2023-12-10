using System;
using UnityEngine;

namespace M7459.Entities
{
    /// <summary>
    /// Method <c>CustomEvent</c> represents a custom event
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        /// <value>Property <c>Type</c> represents the type of the custom event.</value>
        public CustomEventProperties.Types Type { get; set; }
        
        /// <value>Property <c>Raiser</c> represents the raiser of the custom event.</value>
        public GameObject Raiser { get; set; }
    }
}
