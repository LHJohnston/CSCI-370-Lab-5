using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidbody2D;
    float horizontal;
    float vertical;
    private bool facingLeft = true;
    Animator animator;
    

    public float runSpeed = 5f;
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float moveX = horizontal = Input.GetAxisRaw("Horizontal");
        float moveY = vertical = Input.GetAxisRaw("Vertical");
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        rigidbody2D.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

}
