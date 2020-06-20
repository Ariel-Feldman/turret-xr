using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PoolingSystem 
{   
    public class PoolSystem : MonoBehaviour
    {
        public GameObject PoolPrefab;
        public Transform PoolTransform;
        public int PoolSize;

        private Queue<GameObject> _poolQueue = new Queue<GameObject>();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            ConstructPool();
        }

        private void ConstructPool()
        {
            for (int i = 0; i < PoolSize; i ++)
            {
                GameObject instant = Instantiate(PoolPrefab, PoolTransform);
                instant.SetActive(false);
                _poolQueue.Enqueue(instant);
            }
        }

        public GameObject GetFromPool()
        {

            if (_poolQueue.Count == 0)
            {
                DebugLogger.Log("Pool Is Empty", LogSeverity.Debug);
                return null;
            }

            GameObject instant = _poolQueue.Dequeue();

            instant.SetActive(true);
            return instant;
        }
        public void DeActivePool()
        {
            foreach (GameObject instant in _poolQueue)
            {
                instant.gameObject.SetActive(false);
            }
        }
    
    } 
}
