using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxhealth = 100;
    [SerializeField] float speed = 100;

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

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToRight = target.position.x > transform.position.x;   
            transform.localScale = new Vector2(playerToRight ? -1 : 1, 1);
        }

    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
