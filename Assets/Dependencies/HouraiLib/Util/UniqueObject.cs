using System;
using UnityEngine;
using System.Collections.Generic;

namespace HouraiTeahouse {
    /// <summary>
    /// Component that marks a unique object.
    /// Objects instantiated with this attached only allows one to exist.
    /// Trying to create/instantiate more copies will have the object destroyed instantly.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UniqueObject : MonoBehaviour {
        /// <summary>
        /// A collection of all of the UniqueObjects currently in the game.
        /// </summary>
        private static Dictionary<string, UniqueObject> _allIds;

        [SerializeField, ReadOnly, Tooltip("The unique id for this object")]
        private string _id;

        /// <summary>
        /// The unique ID of the object.
        /// </summary>
        public string ID {
            get { return _id; }
        }

        /// <summary>
        /// Unity Callback. Called on object instantiation.
        /// </summary>
        void Awake() {
            if (_allIds == null)
                _allIds = new Dictionary<string, UniqueObject>();
            if (_allIds.ContainsKey(ID)) {
                // Destroy only destroys the object after a frame is finished, which still allows
                // other code in other attached scripts to execute.
                // DestroyImmediate ensures that said code is not executed and immediately removes the
                // GameObject from the scene.
                DestroyImmediate(gameObject);
                return;
            }
            _allIds[ID] = this;
        }

        /// <summary>
        /// Unity callback. Called on object destruction.
        /// </summary>
        void OnDestroy() {
            if (_allIds == null || _allIds[ID] != this)
                return;
            _allIds.Remove(ID);
            if (_allIds.Count <= 0)
                _allIds = null;
        }

        /// <summary>
        /// Unity callback. Called on editor reset.
        /// </summary>
        void Reset() {
            _id = Guid.NewGuid().ToString();
        }
    }
}
