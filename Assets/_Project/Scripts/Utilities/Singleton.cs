using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Muramasa.Utilities
{
    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        #region Fields

        [CanBeNull] private static T _instance;

        [NotNull]
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object Lock = new object();

        [SerializeField] private bool _persistent = true;

        #endregion

        #region Properties

        
        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }
 
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        // Search for existing instance.
                        _instance = (T)FindObjectOfType(typeof(T));
 
                        // Create new instance if one doesn't already exist.
                        if (_instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
 
                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
 
                    return _instance;
                }
            }
        }

        #endregion

        #region Methods

        private void Awake()
        {
            if (_persistent)
                DontDestroyOnLoad(gameObject);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        #endregion
    }

    public abstract class Singleton : MonoBehaviour
    {
        #region Properties

        protected static bool Quitting { get; private set; }

        #endregion

        #region Methods

        private void OnApplicationQuit()
        {
            Quitting = true;
        }
        
        private void OnDestroy()
        {
            Quitting = true;
        }

        #endregion
    }
}