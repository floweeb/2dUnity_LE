using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    
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
        float horizontal_input = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);
        
        // Flip player when moving left/right
        if(horizontal_input > 0.01f)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else if (horizontal_input < -0.01f)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();

        // Set animator parameters
        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private bool isGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }
}
