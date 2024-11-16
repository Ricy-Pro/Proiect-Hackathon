using System.Collections;
using UnityEngine;

public class MOB : MonoBehaviour
{
    public Transform castleTransform;
    private int CurrentHealth;
    public int maxHealth;
    public int damage;
    public float moveSpeed;

    public CastleHealth CastleHealth;
    CastleHealth detectedCastle;
    public Transform[] lanes; // Array of lanes
    public int laneIndex;     // Assigned lane
    public float spawnInterval = 2f;


    public Animator animator;
    Coroutine AttackOrder;
    
    protected virtual void Start()
    {
        Debug.Log("Base Mob");
        CurrentHealth=maxHealth;
        
    }

    void Attack()
    {
        
        
            detectedCastle.TakeDamage(damage);
            Debug.Log("Castle under attack! Damage dealt: " + damage);
            Object.Destroy(gameObject);
        
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log(CurrentHealth);
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
        else if (AttackOrder == null) // Start attacking only if not already attacking
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
