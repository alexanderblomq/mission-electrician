using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float movementSmoothing = 1f;
    [SerializeField] float gcOffset = 0.5f;

    
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsPlatform;
    private float groundedRadius = 0.2f;
    private Rigidbody2D rb;
    private Vector2 mVel = Vector2.zero;
    [SerializeField] Transform groundCheck;
    private bool flipped = false;
    private bool queueJump = false;
    private bool isGrounded = false;
    private bool dead = false;

    private AudioSource audio;

    private Vector2 velocityModifier;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private MovingPlatform currentPlatform;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        queueJump = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
    }

    void FixedUpdate()
    {
        isGrounded = false;

        currentPlatform = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if ((whatIsGround & (1 << colliders[i].gameObject.layer)) != 0)
                {
                    isGrounded = true;
                }
                else if(!currentPlatform && (whatIsPlatform & (1 << colliders[i].gameObject.layer)) != 0)
                {
                    isGrounded = true;
                    currentPlatform = colliders[i].gameObject.GetComponent<MovingPlatform>();
                }
            }
        }
        Move(Input.GetAxisRaw("Horizontal"), queueJump);
    }

    public void Electrocute()
    {
        dead = true;
        animator.SetTrigger("Electrocuted");
        rb.bodyType = RigidbodyType2D.Static;
        LevelManager.Instance.LevelLost();
        //TODO: Get electrocuted
    }

    public void Move(float move, bool jump)
    {
        if (dead) return;

        if (Mathf.Abs(move) > 0)
        {
            if (!audio.isPlaying) audio.Play();
            animator.SetBool("Running", true);
        }
        else
        {
            audio.Stop();
            animator.SetBool("Running", false);
        }
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
        rb.linearVelocity -= velocityModifier;
        Vector2 moveVel = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref mVel, movementSmoothing);
        if (currentPlatform) velocityModifier = currentPlatform.GetVelModifier();
        moveVel += velocityModifier;
        rb.linearVelocity = moveVel;

        if (jump && isGrounded)
        {
            isGrounded = false;
            queueJump = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Flip(bool faceLeft)
    {
        GetComponentInChildren<CableManager>().flipOffset(faceLeft);
        spriteRenderer.flipX = faceLeft;
        flipped = faceLeft;
    }
}