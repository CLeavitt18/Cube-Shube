using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Compenets\n------------------------------------------------------")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private EnemyUI ui;


    [Header("Enemy Stats\n------------------------------------------------------")]
    [SerializeField] private int life;
    [SerializeField] private int shield;
    [SerializeField] private int armor;
    [SerializeField] private float speed;
    [SerializeField] private int lookRange;
    [SerializeField] private int attackRange;
    [SerializeField] private int stopRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;

    private bool canShoot = true;

    private int maxLife;
    private int maxShield;


    // Start is called before the first frame update
    void Start()
    {
        agent.speed = speed;
        agent.stoppingDistance = stopRange;
        maxLife = life;
        maxShield = shield;

        ui.UpdatedBars();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        float distance = Vector3.Distance(Player.player.transform.position, transform.position);

        if (distance <= stopRange)
        {
            FacePlayer();
        }
        else if (distance <= lookRange)
        {
            agent.SetDestination(Player.player.transform.position);
        }
    }

    private void Attack()
    {
        if (!canShoot)
        {
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void FacePlayer()
    {
        Vector3 direction = (Player.player.transform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            shield -= damage;

            if (shield < 0)
            {
                life += shield;
                shield = 0;
            }
        }
        else
        {
            life -= damage;

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }

        ui.UpdatedBars();
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);  

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRange);  
    }

    public int GetLife()
    {
        return life;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }

    public int GetShield()
    {
        return shield;
    }

    public int GetMaxShield()
    {
        return maxShield;
    }
}
