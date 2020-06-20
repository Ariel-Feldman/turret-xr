using UnityEngine;
namespace PoolingSystem 
{
    [RequireComponent(typeof(PoolSystem))]
    public class EnemySpawner : MonoBehaviour
    {

        public PoolSystem pool;
        public float timeBetweenSpawn;
        public Vector3[] spawnZone;

        private float _nextTimToSpawns;
        private bool KeepSpawning = true; 

        private void Update()
        {
            // Fast out - do not spawn when no playing
            if ((GameStateManager.Instance.GameState != GameState.Playing) || !KeepSpawning )
                return;
            
            // Check if its spawn time
            if (Time.time > _nextTimToSpawns)
            {
                _nextTimToSpawns = Time.time + timeBetweenSpawn;
                SpawnFromPool(pool, Vector3.forward, Quaternion.identity);
            }
        }

        private void SpawnFromPool(PoolSystem pool, Vector3 position, Quaternion rotation)
        {
            GameObject enemy = pool.GetFromPool();
            if (enemy == null)
            {
                DebugLogger.Log("Pool not availble", LogSeverity.Debug);
                KeepSpawning = false;
                return;
            }
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;   
        }
    } 
}
