using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    public float speed = 3f;
    private bool movinRight=false;
    [Header("Detector")]
    public Transform groundCheck;
    public float distanceToGround = 1.5f;
    public float distanceToWall=1.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb=GetComponent<Rigidbody2D>(); // instanciamos    
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(movinRight?speed:-speed, rb.velocity.y);
        Vector2 direction = movinRight ? Vector2.right : Vector2.left;
        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, groundLayer);
        RaycastHit2D isWallAhead = Physics2D.Raycast(transform.position, direction, distanceToWall, groundLayer);
        if(isGroundAhead.collider==null || isWallAhead.collider!=null){
            Flip();
        }
    }

    void Flip(){
        movinRight=!movinRight;
        Vector3 scale = transform.localScale;
        scale.x *=-1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
{
    if (groundCheck != null)
    {
        Gizmos.DrawRay(groundCheck.position, Vector2.down * distanceToGround);
    }

    Vector2 direction = movinRight ? Vector2.right : Vector2.left;

    Gizmos.DrawRay(transform.position, direction * distanceToWall);
}  
}
