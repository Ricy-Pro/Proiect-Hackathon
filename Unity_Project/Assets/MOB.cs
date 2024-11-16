using UnityEngine;

public class MOB : MonoBehaviour
{
    private int CurrentHealth;
    public int maxHealth;
    public int damage;

    public CastleHealth CastleHealth;
    float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = maxHealth;
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
        //Change the sprinterender color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //wait for small amout of time
        yield return new WaitForSeconds(0.2f);
        //Revert to default color
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CastleHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
