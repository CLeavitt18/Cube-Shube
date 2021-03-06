using UnityEngine;


public class Bullet : MonoBehaviour
{
    //0 = player 1 = enemy
    [SerializeField] private int ownerId;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int lifeTime;
    [SerializeField] private int damage;
    [SerializeField] private int speed;


    void Start()
    {
        Destroy(gameObject, lifeTime);
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other) 
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            if (ownerId == 0)
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    damagable.TakeDamage(damage);
                }
            }
            else
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    damagable.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
