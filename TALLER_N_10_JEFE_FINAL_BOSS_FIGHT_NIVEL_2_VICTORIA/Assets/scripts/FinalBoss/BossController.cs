using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Estadísticas del Jefe")]
    public int maxHealth = 10;
    private int currentHealth;
    
    [Header("UI")]
    public Slider barraVidaUI;
    
    // --- NUEVA VARIABLE AÑADIDA ---
    public GameObject panelVictoriaUI; 

    [Header("Movimiento")]
    public float speed = 2f;
    public Transform playerTarget;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Detección de Entorno")]
    public Transform groundCheck;
    public float distanceToGround = 1.5f;
    public float distanceToWall = 0.8f;
    public LayerMask groundLayer;

    [Header("Fase de Furia")]
    private bool isEnraged = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (barraVidaUI != null)
        {
            barraVidaUI.maxValue = maxHealth;
            barraVidaUI.value = currentHealth;
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTarget = player.transform;
        }
    }

    void Update()
    {
        if (playerTarget == null) return;

        float directionToPlayer = Mathf.Sign(playerTarget.position.x - transform.position.x);

        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, groundLayer);
        RaycastHit2D isWallAhead = Physics2D.Raycast(transform.position, Vector2.right * directionToPlayer, distanceToWall, groundLayer);

        if (isGroundAhead.collider != null && isWallAhead.collider == null)
        {
            float newXPosition = Mathf.MoveTowards(transform.position.x, playerTarget.position.x, speed * Time.deltaTime);
            Vector2 targetPosition = new Vector2(newXPosition, rb.position.y);
            rb.MovePosition(targetPosition);
        }

        if (directionToPlayer > 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
        else if (directionToPlayer < 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (barraVidaUI != null) barraVidaUI.value = currentHealth;

        if (currentHealth <= maxHealth / 2 && !isEnraged) EnterEnragedState();
        
        // --- MODIFICADO: Ahora llama al método Die() ---
        if (currentHealth <= 0) Die(); 
    }

    // --- NUEVO MÉTODO DIE() ---
    private void Die()
    {
        // 1. Activar el panel de victoria
        if (panelVictoriaUI != null)
        {
            panelVictoriaUI.SetActive(true);
        }

        // 2. Frenar el tiempo del juego
        Time.timeScale = 0f;

        // 3. Destruir al Boss
        Destroy(gameObject);
    }

    private void EnterEnragedState()
    {
        isEnraged = true;
        if (spriteRenderer != null) spriteRenderer.color = Color.red;
        speed *= 1.8f;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(groundCheck.position, Vector2.down * distanceToGround);
        }

        if (playerTarget != null)
        {
            float directionToPlayer = Mathf.Sign(playerTarget.position.x - transform.position.x);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector2.right * directionToPlayer * distanceToWall);
        }
    }
}