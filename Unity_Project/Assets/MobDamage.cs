using UnityEngine;

public class MobDamage : MonoBehaviour
{
    public int damage;

    public CastleHealth CastleHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CastleHealth.TakeDamage(damage);
        }
    }

}

