using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float movementSmoothing = 1f;
    [SerializeField] float gcOffset = 0.5f;
   
    [SerializeField] LayerMask whatIsGround;
    private float groundedRadius = 0.2f;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector2 mVel = Vector2.zero;
    private Vector3 groundCheck;
    private bool flipped = false;
    private bool queueJump = false;
    private bool isGrounded = false;
    private bool dead = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

    public void Electrocute()
    {
        dead = true;
        LevelManager.Instance.LevelLost();
        //TODO: Get electrocuted
    }

    public void Move(float move, bool jump)
    {
        if(dead) return;
        if (!flipped && move < 0)
        {
            Debug.Log("Flipping to the left");
            Flip(true);
        }
        else if (flipped && move > 0)
        {
            Debug.Log("Flipping to the right");
            Flip(false);
        }

            Vector2 targetVelocity = new Vector2(move * 10f, rb.linearVelocityY);
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref mVel, movementSmoothing);

        if(jump && isGrounded)
        {
            isGrounded = false;
            queueJump = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Flip(bool faceLeft)
    {
        spriteRenderer.flipX = faceLeft;
        flipped = faceLeft;
    }
}