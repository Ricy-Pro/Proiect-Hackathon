using System.Collections;
using UnityEngine;

public class MOB : MonoBehaviour
{
    public Transform castleTransform;   // Target for the mob
    public int maxHealth;               // Maximum health of the mob
    public int damage;                  // Damage dealt to the castle
    public float moveSpeed;             // Speed of the mob's movement

    private int currentHealth;          // Current health of the mob
    private CastleHealth detectedCastle; // Reference to the castle's health script
    private Coroutine attackCoroutine;   // Coroutine for attack logic

    protected virtual void Start()
    {
        currentHealth = maxHealth;      // Initialize mob's health
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;       // Reduce health by the damage amount
        StartCoroutine(BlinkRed());    // Visual feedback for taking damage

        if (currentHealth <= 0)        // Destroy mob if health reaches zero
            Destroy(gameObject);
    }

    IEnumerator BlinkRed()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.red;     // Change sprite color to red
            yield return new WaitForSeconds(0.2f); // Wait briefly
            spriteRenderer.color = Color.white;   // Revert to original color
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CastleHealth") && detectedCastle == null)
        {
            detectedCastle = collision.GetComponent<CastleHealth>();
        }
    }

    private void Update()
    {
        if (detectedCastle == null)    // If no castle detected, move toward it
        {
            MoveTowardsCastle();
        }
        else if (attackCoroutine == null) // If castle detected and not already attacking
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void MoveTowardsCastle()
    {
        if (castleTransform != null)
        {
            // Calculate direction to the castle and move
            Vector2 direction = (castleTransform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (detectedCastle != null)
            {
                detectedCastle.TakeDamage(damage);  // Damage the castle
                Debug.Log("Castle under attack! Damage dealt: " + damage);
            }

            Destroy(gameObject);                   // Destroy mob after attack
            yield break;                           // End coroutine
        }
    }
}