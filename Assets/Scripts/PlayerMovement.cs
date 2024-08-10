using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    private Rigidbody2D body;
    private Animator anim;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Help grab references for RigidBody2D and Animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        if(Input.GetKey(KeyCode.Space) )
            body.velocity = new Vector2(body.velocity.x, speed);
    }
}
