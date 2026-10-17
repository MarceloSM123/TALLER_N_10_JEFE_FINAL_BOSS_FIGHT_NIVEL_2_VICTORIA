using UnityEngine;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{

    [Header("Ajustes de Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;
    public float attackDelay = 0.2f;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)){
        StartCoroutine(AttackRoutine());
        if(AudioManager.Instance != null ){
    AudioManager.Instance.ReproducirSfx(AudioManager.Instance.sonidoGolpe);
}
        }
    }

   /* void Attack(){
    if(animator != null){
        animator.SetTrigger("Attack");
    }

    

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    foreach(Collider2D enemy in hitEnemies){
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if(enemyHealth != null){
            enemyHealth.TakeDamage(attackDamage);
        }
    }
    }  */

    IEnumerator AttackRoutine()
{
    // 1. Activar animacion de ataque de inmediato
    if (animator != null)
    {
        animator.SetTrigger("Attack");
    }

    // 2. Esperar una fraccion de segundo a que la espada se extienda hacia adelante
    yield return new WaitForSeconds(attackDelay);

    // 3. Detectar enemigos e infligir daño en el momento justo del impacto
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    foreach (Collider2D enemy in hitEnemies)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
             enemyHealth.TakeDamage(attackDamage);
        }

        BossController boss = enemy.GetComponent<BossController>();
    if (boss != null)
    {
        boss.TakeDamage(attackDamage);
    }
    }
} 

    private void OnDrawGizmosSelected(){
    if(attackPoint == null) return;
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
