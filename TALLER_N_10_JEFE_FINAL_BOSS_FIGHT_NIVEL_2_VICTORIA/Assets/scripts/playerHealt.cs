using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Empujon (Knockback)")]
    public float knockbackForceX = 8f;
    public float knockbackForceY = 6f;
    public float invulnerabilityTime = 1f;

    [Header("Referencias")]
    private Animator animator;
    private Rigidbody2D rd;
    private SpriteRenderer spriteRenderer;

    private bool isInvulnerable = false;

    void Start()
    {
        currentLives = maxLives;
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && !isInvulnerable)
        {
            TakeDamage(collision);
        }
    }

    public void TakeDamage(Collision2D collision)

    {
        currentLives--;
        Debug.Log("Vidas restantes: " + currentLives);

    if(CameraShake.Instance != null){
    CameraShake.Instance.Sacudir(0.2f, 0.2f);
    }

      //  if(InvulnerabilityRoutine) return;

        // Activar animacion de daño
        if (animator != null)
            animator.SetTrigger("hurt");

        // Si se queda sin vidas, reiniciar nivel
        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // Calcular dirección del empujón (alejándose del enemigo)
        Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
        knockbackDir.y = 1f; // siempre un poco hacia arriba

        rd.linearVelocity = Vector2.zero; // resetear velocidad para que el empujón se sienta consistente
        rd.AddForce(new Vector2(knockbackDir.x * knockbackForceX, knockbackDir.y * knockbackForceY), ForceMode2D.Impulse);

        StartCoroutine(InvulnerabilityRoutine());


    }

    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        // Parpadeo visual mientras es invulnerable
        float elapsed = 0f;
        float blinkInterval = 0.1f;
        while (elapsed < invulnerabilityTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvulnerable = false;
    }

    // Para que el PlayerController sepa si está siendo empujado
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public int GetCurrentLives()
{
    return currentLives;
}
//********************
public void TakeDamageFromTrigger(Transform damageSource)
{
    if (isInvulnerable) return;

    currentLives--;
    Debug.Log("Vidas restantes: " + currentLives);

    if (animator != null)
        animator.SetTrigger("hurt");

    if (currentLives <= 0)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return;
    }

    Vector2 knockbackDir = (transform.position - damageSource.position).normalized;
    knockbackDir.y = 1f;

    rd.linearVelocity = Vector2.zero;
    rd.AddForce(new Vector2(knockbackDir.x * knockbackForceX, knockbackDir.y * knockbackForceY), ForceMode2D.Impulse);

    StartCoroutine(InvulnerabilityRoutine());
}

}