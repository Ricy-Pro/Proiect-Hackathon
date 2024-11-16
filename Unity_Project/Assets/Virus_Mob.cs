using System.Collections;
using UnityEngine;

public class Virus_Mob : MOB
{
    [Header("Virus Specific Settings")]
    public float timeToDouble = 2f;    // Time interval to duplicate
    public float repelForce = 5f;      // Force applied to separate mobs
    public GameObject mobPrefab;       // Prefab for spawning new mobs

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DoubleItself()); // Begin the duplication behavior
    }

    private IEnumerator DoubleItself()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToDouble);

            // Instantiate a duplicate mob
            GameObject newMob = Instantiate(mobPrefab, transform.position, transform.rotation);
            if (newMob.TryGetComponent(out MOB mobScript))
            {
                mobScript.castleTransform = castleTransform; // Assign castle target to the new mob
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            // Push mobs apart to prevent overlap
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>()?.AddForce(direction * repelForce, ForceMode2D.Impulse);
        }
    }
}
