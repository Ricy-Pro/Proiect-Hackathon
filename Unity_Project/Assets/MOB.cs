using System.Collections;
using UnityEngine;

public class MOB : MonoBehaviour
{
    private int CurrentHealth;
    public int maxHealth;
    public int damage;
    public float moveSpeed;

    public CastleHealth CastleHealth;
    CastleHealth detectedCastle;
    public Transform[] lanes; // Array of lanes
    public int laneIndex;     // Assigned lane
    public GameObject mobPrefab;
    public float spawnInterval = 2f;

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

    void TakeDamage(int damage)
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

    void Move()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    void Update()
    {
        if (!detectedCastle)
        {
            Move();
        }
        else if (attackOrder == null) // Start attacking only if not already attacking
        {
            Attack();
        }
    }
}
