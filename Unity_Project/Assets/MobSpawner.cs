using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobType
{
    public GameObject mobPrefab; // Prefab for the mob
    public float spawnChance;    // Chance of spawning this mob
}

public class MobSpawner : MonoBehaviour
{
    [Header("Mob Settings")]
    public List<MobType> mobTypes;   // List of mobs with spawn chances
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 2f; // Time between mob spawns

    [Header("Wave Settings")]
    public int initialWaveSize = 5;   // Number of mobs in the first wave
    public float waveInterval = 10f;  // Time between waves
    public float waveSizeMultiplier = 1.2f; // Increase in mob count per wave

    [Header("Castle Settings")]
    public Transform castleTransform; // Target for mobs
    private CastleHealth castleHealth; // Reference to the castle's health

    private bool isCastleDestroyed = false; // Flag to stop spawning if the castle is destroyed
    private int currentWave = 1;            // Current wave number
    private int mobsToSpawn;                // Number of mobs to spawn in the current wave
    private float totalChance;              // Sum of spawn chances for normalization

    void Start()
    {
        castleHealth = FindObjectOfType<CastleHealth>();
        if (castleHealth == null)
        {
            Debug.LogError("CastleHealth component not found in the scene!");
            return;
        }

        // Calculate the total spawn chance
        foreach (var mobType in mobTypes)
        {
            totalChance += mobType.spawnChance;
        }

        if (totalChance <= 0)
        {
            Debug.LogError("Total spawn chance must be greater than zero!");
            return;
        }

        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        Debug.Log($"Prepare for the first Wave");
        yield return new WaitForSeconds(30f);

        while (!isCastleDestroyed)
        {
            Debug.Log($"Starting Wave {currentWave}!");

            // Calculate the number of mobs to spawn in this wave
            mobsToSpawn = Mathf.RoundToInt(initialWaveSize * Mathf.Pow(waveSizeMultiplier, currentWave - 1));

            // Spawn the wave
            for (int i = 0; i < mobsToSpawn; i++)
            {
                if (castleHealth.HealthRemain() <= 0)
                {
                    Debug.Log("Castle destroyed! Stopping mob spawns.");
                    isCastleDestroyed = true;
                    yield break;
                }

                // Randomize spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Randomize mob type
                GameObject mobPrefab = GetRandomMob();
                if (mobPrefab != null)
                {
                    // Spawn the mob
                    GameObject mob = Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
                    MOB mobScript = mob.GetComponent<MOB>();
                    if (mobScript != null)
                    {
                        mobScript.castleTransform = castleTransform; // Assign castle target
                    }
                }

                yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next mob
            }

            // Wait for the next wave
            Debug.Log($"Wave {currentWave} completed. Waiting for the next wave...");
            yield return new WaitForSeconds(waveInterval);
            currentWave++; // Move to the next wave
        }
    }

    private GameObject GetRandomMob()
    {
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (var mobType in mobTypes)
        {
            cumulativeChance += mobType.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return mobType.mobPrefab;
            }
        }

        Debug.LogWarning("Failed to select a mob. Check spawn chances.");
        return null;
    }
}
