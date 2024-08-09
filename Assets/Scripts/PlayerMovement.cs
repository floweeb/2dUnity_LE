using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal"), body.velocity.y);
    }
}
