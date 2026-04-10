using UnityEngine;


// AUTEUR: Stijn Grievink
// Small script to spawn extra enemies at a spawner for our test scene
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    // Timer variables
    [SerializeField] private float spawnDelay;

    // Invoke the spawn function to spawn an enemy after the delay
    void Start()
    {
        Invoke("SpawnEnemy", spawnDelay);
    }

    // Spawn an enemy and Invoke again to run spawn function after the delay
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        Invoke("SpawnEnemy", spawnDelay);
    }
}
