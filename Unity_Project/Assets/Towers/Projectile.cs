using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;   // Speed of the projectile
    public MOB target;         // Target the projectile will move towards
    private int damage;       // Damage dealt by the projectile

    public void Initialize(MOB targetMob, int towerDamage)
    {
        target = targetMob;
        damage = towerDamage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy projectile if target is gone
            return;
        }

        // Move towards the target
        Vector2 direction = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Check if we've reached the target
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject); // Destroy the projectile after hitting the target
    }
}