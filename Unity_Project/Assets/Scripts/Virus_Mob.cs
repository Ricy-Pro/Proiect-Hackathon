using System.Collections;
using UnityEngine;

public class Virus_Mob : MOB
{
    public float TimeToDouble = 2f; // Time interval to double
    public float repelForce = 5f;   // Force applied to separate mobs on collision
    public GameObject mobPrefab;         // The mob prefab to spawn

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DoubleItself());
    }

    private IEnumerator DoubleItself()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToDouble);

            // Spawn the mob
            GameObject mob = Instantiate(mobPrefab, transform.position, transform.rotation);
            MOB mobScript = mob.GetComponent<MOB>();
            if (mobScript != null)
            {
                mobScript.castleTransform = castleTransform;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is another mob
        if (collision.gameObject.CompareTag("Mob"))
        {
            // Calculate direction away from the other mob
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Apply a force to separate the mobs
            GetComponent<Rigidbody2D>().AddForce(direction * repelForce, ForceMode2D.Impulse);
        }
    }
}
