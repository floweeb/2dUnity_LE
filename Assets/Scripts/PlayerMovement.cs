using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5, jumpPower = 8;
    [SerializeField] private LayerMask groundLayer, wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontal_input;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Help grab references for RigidBody2D and Animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        horizontal_input = Input.GetAxis("Horizontal");

        // Flip player when moving left/right
        if(horizontal_input > 0.01f)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else if (horizontal_input < -0.01f)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        // Set animator parameters
        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", isGrounded());

        // wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 1.5f;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");

        }else if (onWall() && !isGrounded())
        {
            if (horizontal_input == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, 0);
                float dir = 1.5f;
                if (Mathf.Sign(transform.localScale.x) < 0)
                    dir = -1.5f;
                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCooldown = 0;

        }
    }
    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return rayCastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontal_input == 0 && isGrounded() && !onWall();
    }
}
