using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float movementSmoothing = 1f;
    [SerializeField] float gcOffset = 0.5f;
    private Vector3 groundCheck;
    [SerializeField] LayerMask whatIsGround;
    private float groundedRadius = 0.2f;
    private Rigidbody2D rb;
    private Collider2D coll;
    Vector2 mVel = Vector2.zero;

    private bool queueJump = false;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = new Vector3(transform.position.x, transform.position.y - gcOffset, 0);
    }

    private void Update()
    {
        queueJump = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
    }

    void FixedUpdate()
    {
        groundCheck.x = transform.position.x;
        groundCheck.y = transform.position.y - gcOffset;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck, groundedRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        Move(Input.GetAxisRaw("Horizontal"), queueJump);
    }

    public void Move(float move, bool jump)
    {
        Vector2 targetVelocity = new Vector2(move * 10f, rb.linearVelocityY);
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref mVel, movementSmoothing);

        if(jump && isGrounded)
        {
            isGrounded = false;
            queueJump = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }
}