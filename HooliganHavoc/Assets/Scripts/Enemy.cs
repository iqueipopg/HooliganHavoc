using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxhealth = 100;
    [SerializeField] float speed = 3f;

    [Header("Charger")]
    [SerializeField] bool isCharger;               // Si el enemigo es del tipo "Charger"
    [SerializeField] float distanceToCharge = 4.5f; // Distancia para activar la carga
    [SerializeField] float chargeSpeed = 3.5f;     // Velocidad al cargar
    [SerializeField] float prepareTime = 4f;       // Tiempo antes de cargar

    [Header("Stun Settings")]
    [SerializeField] float stunDuration = 0.5f;    // Duración del "stun" cuando colisiona con el jugador

    private bool isCharging = false;              // Si el enemigo está cargando
    private bool isPreparingCharge = false;       // Si el enemigo está preparándose para cargar
    private bool isStunned = false;               // Si el enemigo está paralizado

    private int currentHealth;                    // Vida actual del enemigo
    private float originalSpeed;                  // Velocidad original para restaurar después de la carga o paralización

    Animator anim;
    Transform target;

    private void Start()
    {
        currentHealth = maxhealth;
        target = GameObject.Find("Player")?.transform; // Buscar al jugador
        anim = GetComponent<Animator>();
        originalSpeed = speed; // Guardar velocidad inicial
    }

    private void Update()
    {
        if (isStunned || isPreparingCharge) return; // No hacer nada si está paralizado o preparando carga

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
        if (!isStunned) // Solo cargar si no está paralizado
        {
            isPreparingCharge = false;
            isCharging = true;
            speed = chargeSpeed;

            // Detener la carga después de un tiempo
            Invoke("StopCharging", 1.5f);
        }
    }

    void StopCharging()
    {
        isCharging = false;
        speed = originalSpeed; // Restaurar velocidad original
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Detectar colisión con el jugador
        {
            StartCoroutine(Stun()); // Paralizar al enemigo
        }
    }

    private System.Collections.IEnumerator Stun()
    {
        isStunned = true;           // Marcar como paralizado
        float cachedSpeed = speed; // Guardar velocidad actual
        speed = 0;                 // Detener movimiento

        yield return new WaitForSeconds(stunDuration); // Esperar el tiempo de paralización

        isStunned = false;          // Restaurar movimiento
        speed = cachedSpeed;       // Restaurar velocidad
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim?.SetTrigger("hit");

        if (currentHealth <= 0)
        {
            EnemyManager.enemiesKilled++;
            Destroy(gameObject);
        }
    }
}
