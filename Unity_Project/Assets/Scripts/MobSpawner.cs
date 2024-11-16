using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mobPrefab;         // The mob prefab to spawn
    public Transform[] spawnPoints;     // Array of spawn points
    public float spawnInterval = 2f;    // Time between spawns
    public Transform castleTransform;  // Assign this in the Inspector

    void Start()
    {
            Debug.Log("MobSpawner started!");
            StartCoroutine(SpawnMobs());
    }

    IEnumerator SpawnMobs()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawn the mob and set its castle target
            GameObject mob = Instantiate(mobPrefab, spawnPoint.position, spawnPoint.rotation);
            MOB mobScript = mob.GetComponent<MOB>();
            mobScript.castleTransform = castleTransform;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
