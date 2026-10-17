using UnityEngine;
using System.Collections;

public class TimeSpike : MonoBehaviour
{
    [Header("Ajustes del Tiempo")]
    public float waitTime = 2f;
    public float activeTime = 1f;

    [Header("Daño de la trampa")]
    public int damageAmount = 1;

    [Header("Estado Trampa")]
        public bool isDangerous = false;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    animator = GetComponent<Animator>();
    StartCoroutine(TrapRoutine());
    }

    IEnumerator TrapRoutine()
{
    while (true)
    {
        isDangerous = false;
        yield return new WaitForSeconds(waitTime);

        if (animator != null)
        {
            animator.SetTrigger("Active");
        }

        isDangerous = true;
        yield return new WaitForSeconds(activeTime);

        isDangerous = false; 
    }
}

/*IEnumerator AttackRoutine()
{
    // 1. Activar animación de ataque de inmediato
    if (animator != null)
    {
        animator.SetTrigger("Attack");
    }

    // 2. Esperar una fracción de segundo a que la espada se extienda hacia adelante
    yield return new WaitForSeconds(attackDelay);

    // 3. Detectar enemigos e infligir daño en el momento justo del impacto
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    foreach (Collider2D enemy in hitEnemies)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage, transform.position);
        }
    }
}*/

/* private void OnTriggerStay2D(Collider2D other)
{
  //  if (isDangerous)
  //  {
  //      PlayerHealth playerhealth = other.GetComponent<PlayerHealth>();
  //  }

    if(isDangerous){
    playerHealth Playerhealth = other.GetComponent<playerHealth>();

    if(Playerhealth != null){
        Playerhealth.TakeDamage(transform);
    }
}
} */ 

private void OnTriggerStay2D(Collider2D other)
{
    if (!isDangerous) return;

    playerHealth playerhealth = other.GetComponent<playerHealth>();

    if (playerhealth != null && !playerhealth.IsInvulnerable())
    {
        playerhealth.TakeDamageFromTrigger(transform);
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
