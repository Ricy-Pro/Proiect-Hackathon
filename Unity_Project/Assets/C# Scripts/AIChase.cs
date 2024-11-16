using UnityEngine;

public class AIChase : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    public int damage;

    public GameObject Base;
    public float speed;

    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {

        distance =
            Vector2
            .Distance(transform.position, Base.transform.position);
        Vector2 direction = Base.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, Base.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);


    }

}