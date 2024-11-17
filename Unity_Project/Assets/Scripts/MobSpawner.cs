using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobPrefabs;      // Array of mob prefabs to spawn
    public Transform[] spawnPoints;     // Array of spawn points
    public float spawnInterval = 2f;    // Time between spawns
    public Transform castleTransform;   // Target for mobs, assign in the Inspector
    private CastleHealth castleHealth; // Reference to the castle's health

    private bool isCastleDestroyed = false; // Flag to stop spawning if the castle is destroyed

    void Start()
    {
        Debug.Log("MobSpawner started!");
        castleHealth = FindObjectOfType<CastleHealth>();
        if (castleHealth == null)
        {
            Debug.LogError("CastleHealth component not found in the scene!");
            return;
        }

        // Start spawning mobs
        StartCoroutine(SpawnMobs());
    }

    IEnumerator SpawnMobs()
    {
        while (!isCastleDestroyed)
        {

            if (castleHealth.HealthRemain() <= 0)
            {
                Debug.Log("Castle destroyed! Stopping mob spawns.");
                isCastleDestroyed = true; // Stop spawning when the castle is destroyed
                yield break;
            }

            // Randomize spawn point
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            // Randomize mob type
            int randomMobIndex = Random.Range(0, mobPrefabs.Length);
            GameObject mobPrefab = mobPrefabs[randomMobIndex];

            // Spawn the mob
            GameObject mob = Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
            MOB mobScript = mob.GetComponent<MOB>();
            if (mobScript != null)
            {
                mobScript.castleTransform = castleTransform; // Assign castle target to the mob
            }

            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next mob

          
        }
    }
}