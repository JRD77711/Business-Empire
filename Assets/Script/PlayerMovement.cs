using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Transform spriteTransform;

    public int facingDirection = 1; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteTransform = transform.GetChild(0);
    }

    void Update()
    {
        
        if (UIManager.instance != null && UIManager.instance.terminalUI.activeSelf)
        {
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);

        
        if (movement.x > 0)
        {
            spriteTransform.localScale = new Vector3(1, 1, 1);
            facingDirection = 1;
        }
        else if (movement.x < 0)
        {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
            facingDirection = -1;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}