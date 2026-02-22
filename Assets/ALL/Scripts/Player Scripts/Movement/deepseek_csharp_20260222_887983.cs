using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation Settings")]
    [SerializeField] private Animator animator;

    // Components
    private Rigidbody rb;

    // State variables
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;

    // Animation names
    private const string IDLE_ANIM = "Idle";
    private const string RUN_ANIM = "Run";
    private const string JUMP_ANIM = "Jump";

    // Track current animation state
    private string currentAnimation = "";

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        HandleInput();
        CheckGround();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void HandleMovement()
    {
        Vector3 movement = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0);
        rb.linearVelocity = movement;

        // Handle flipping باستخدام Rotate
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
        // لا نشغل الأنيميشن هنا مباشرة، سيتم التعامل معه في HandleAnimation
    }

    void CheckGround()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position;

        isGrounded = Physics.Raycast(rayStart, Vector3.down, out hit, groundCheckDistance, groundLayer);
        Debug.DrawRay(rayStart, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    void HandleAnimation()
    {
        string newAnimation = "";

        // تحديد الأنيميشن المناسب
        if (!isGrounded)
        {
            newAnimation = JUMP_ANIM;
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            newAnimation = RUN_ANIM;
        }
        else
        {
            newAnimation = IDLE_ANIM;
        }

        // تشغيل الأنيميشن إذا كان مختلفاً عن الحالي
        if (newAnimation != currentAnimation)
        {
            PlayAnimation(newAnimation);
            currentAnimation = newAnimation;
        }
    }

    void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
            Debug.Log($"Playing animation: {animationName}"); // للتأكد من عمل الأنيميشن
        }
    }

    // للتحقق من الأنيميشن الحالي
    public string GetCurrentAnimation()
    {
        return currentAnimation;
    }
}