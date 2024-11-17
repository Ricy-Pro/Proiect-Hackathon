using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Properties")]
    public int damage = 10;      // Base damage dealt to mobs
    public float range = 50f;        // Range of the tower
    public float fireRate = 1f;     // Shots per second
    public int level = 1;           // Tower level (affects damage scaling)

    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint;        // Point where projectiles are spawned

    private float fireCooldown = 0f;  // Time left until the tower can fire again

    void Update()
    {
        // Reduce cooldown over time
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Find and attack enemies within range
        MOB target = FindClosestEnemy();
        if (target != null && fireCooldown <= 0)
        {
            Attack(target);
            fireCooldown = 1f / fireRate; // Reset cooldown
        }
    }

    // Finds the closest enemy within range
    MOB FindClosestEnemy()
    {
        MOB closestMob = null;
        float closestDistance = range;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Mob"))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= range)
            {
                MOB mob = enemy.GetComponent<MOB>();
                if (mob != null && (closestMob == null || distance < closestDistance))
                {
                    closestMob = mob;
                    closestDistance = distance;
                }
            }
        }

        return closestMob;
    }

    // Attack a specific target
    void Attack(MOB target)
    {
        // Instantiate projectile if prefab exists
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
            {
                projScript.Initialize(target, damage);
            }
        }
        else
        {
            // Directly apply damage if no projectile system
            target.TakeDamage(damage);
        }
    }

    // Level up the tower to increase damage and possibly other stats
    public void LevelUp()
    {
        level++;
        damage += 5; // Example: Increase damage with level
        range += 0.5f; // Example: Slightly increase range
        Debug.Log($"Tower leveled up! Level: {level}, Damage: {damage}, Range: {range}");
    }
}