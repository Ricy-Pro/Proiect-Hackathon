using System.Collections;
using UnityEngine;

public class Virus_Mob : MOB
{
    [Header("Virus Specific Settings")]
    public float timeToDouble = 2f;    // Time interval to duplicate
    //public float repelForce = 5f;      // Force applied to separate mobs
    public GameObject mobPrefab;       // Prefab for spawning new mobs
    public static int virusCount = 0;  // Global counter for Virus_Mobs
    public int maxVirusCount = 10;     // Maximum allowed Virus_Mobs

    protected override void Start()
    {
        base.Start();

        // Increment the global virus count when this mob is spawned
        virusCount++;
        StartCoroutine(DoubleItself());
    }

    private IEnumerator DoubleItself()
    {
        while (virusCount < maxVirusCount) // Check if the max limit is reached
        {
            yield return new WaitForSeconds(timeToDouble);

            // Instantiate a duplicate mob
            GameObject newMob = Instantiate(mobPrefab, transform.position, transform.rotation);
            if (newMob.TryGetComponent(out Virus_Mob newVirus))
            {
                newVirus.castleTransform = castleTransform; // Assign castle target
                virusCount++; // Increment global virus count
                Debug.Log($"Virus count: {virusCount}");
            }
        }

        // Stop duplicating if the limit is reached
        Debug.Log("Maximum Virus_Mob count reached. Stopping duplication.");
    }


    private void OnDestroy()
    {
        // Decrement the global virus count when this mob is destroyed
        virusCount--;
        Debug.Log($"Virus destroyed. Remaining count: {virusCount}");
    }
}
