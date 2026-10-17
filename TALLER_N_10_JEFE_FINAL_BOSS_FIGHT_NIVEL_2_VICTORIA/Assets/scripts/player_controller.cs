using UnityEngine;

public class player_controller : MonoBehaviour
{
    private Rigidbody2D rd;
    public float speed = 7f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Animator animator;

    private bool facingRight = true;

    [Header("Knockback")]
    private playerHealth playerHealth;
    private bool isKnockback = false;
    private float knockbackTimer = 0f;
    public float knockbackDuration = 0.3f; // cuánto dura el empujón (ajústalo en el Inspector)

    [Header("Efectos visuales")]
public GameObject polvoSaltoPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // --- Manejo del estado de knockback ---
        if (playerHealth != null && playerHealth.IsInvulnerable() && !isKnockback)
        {
            isKnockback = true;
            knockbackTimer = knockbackDuration;
        }

        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
                isKnockback = false;
        }

        // --- Movimiento horizontal ---
        float move = Input.GetAxis("Horizontal");

        // Si estamos en knockback, ignoramos el input de movimiento
        if (!isKnockback)
        {
            rd.linearVelocity = new Vector2(move * speed, rd.velocity.y);
        }

        // Animación de velocidad (0 durante el knockback)
        float speedAnimation = isKnockback ? 0f : Mathf.Abs(move);
        animator.SetFloat("speed", speedAnimation);

        // --- Salto ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isKnockback)
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJump", true);
            if(polvoSaltoPrefabs != null){
    Instantiate(polvoSaltoPrefabs, transform.position, Quaternion.identity);
    if(AudioManager.Instance != null ){
    AudioManager.Instance.ReproducirSfx(AudioManager.Instance.sonidoSalto);
}
}
        }

        // --- Voltear sprite según dirección ---
        if (!isKnockback)
        {
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            animator.SetBool("isJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}