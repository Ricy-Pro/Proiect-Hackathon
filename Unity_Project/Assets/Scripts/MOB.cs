using System.Collections;
using UnityEngine;

public class MOB : MonoBehaviour
{
    public Transform castleTransform;
    private int CurrentHealth;
    public int maxHealth;
    public int damage;
    public float moveSpeed = 10;

    public CastleHealth CastleHealth;
    CastleHealth detectedCastle;
    public Transform[] lanes; // Array of lanes
    public int laneIndex;     // Assigned lane
    public GameObject mobPrefab;
    public float spawnInterval = 2f;
    public float rotationSpeed = 5f; // Smooth rotation speed (adjust as needed)


    public Animator animator;
    Coroutine attackOrder;
    
    void Start()
    {
        CurrentHealth = maxHealth;

        // Place the mob at the assigned lane
        //if (lanes != null && lanes.Length > 0)
        //{
        //    transform.position = lanes[laneIndex].position;
        //}

        //StartCoroutine(SpawnMobs());
    }

    void Attack()
    {
        
        
            detectedCastle.TakeDamage(damage);
            Debug.Log("Castle under attack! Damage dealt: " + damage);
            Object.Destroy(gameObject);
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(BlinkRed());

        if (CurrentHealth <= 0)
            Destroy(gameObject);
    }

    IEnumerator BlinkRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //IEnumerator SpawnMobs()
    //{
    //    while (true)
    //    {
    //        // Spawn a mob and assign it to a random lane
    //        GameObject mob = Instantiate(mobPrefab);
    //        int laneIndex = Random.Range(0, lanes.Length);

    //        MOB mobScript = mob.GetComponent<MOB>();
    //        mobScript.lanes = lanes;
    //        mobScript.laneIndex = laneIndex;

    //        yield return new WaitForSeconds(spawnInterval);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectedCastle != null)
            return;

        if (collision.CompareTag("CastleHealth"))
        {
            detectedCastle = collision.GetComponent<CastleHealth>();
            Debug.Log("Castle detected!");
        }
    }

    //void Move()
    //{
    //    transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    //}

    void Update()
    {
        if (!detectedCastle)
        {

            MoveTowardsCastle();
        }
        else if (attackOrder == null) // Start attacking only if not already attacking
        {
            Attack();
        }
    }

    void MoveTowardsCastle()
    {
        if (castleTransform != null)
        {
            // Calculate the direction to the castle
            Vector2 direction = (castleTransform.position - transform.position).normalized;

            // Move the mob toward the castle without rotating the sprite
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            //Move();
        }
    }
}
