using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("stats")]

    [SerializeField] int maxhealth = 100;
    [SerializeField] float speed = 3f;

    [Header("Charger")]

    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge  = 4.5f;
    [SerializeField] float chargeSpeed = 4f;
    [SerializeField] float prepareTime = 4f;

    bool isCharging = false;
    bool isPreparingCharge = false;


    private int currentHealth;

    Animator anim;
    Transform target;

    private void Start()
    {
        currentHealth = maxhealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!WaveManager.Instance.WaveRunning()) return;
        if (isPreparingCharge) return;

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToRight = target.position.x > transform.position.x;   
            transform.localScale = new Vector2(playerToRight ? -1 : 1, 1);

            if (isCharger && !isCharging && Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime);
            }
        }



    }

    void StartCharging()
    {
        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;
    }


    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            EnemyManager.enemiesKilled++;
            Destroy(gameObject);
        }
    }
}
