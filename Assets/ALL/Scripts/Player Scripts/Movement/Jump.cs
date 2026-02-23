using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // التحقق من الأرض
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // القفز
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
                animator.SetTrigger("Jump");
        }

        // تطبيق الجاذبية
        velocity.y += gravity * Time.deltaTime;

        // إعادة تعيين السرعة عند ملامسة الأرض
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // تطبيق الحركة الرأسية
        controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);

        // تحديث الأنيميشن
        if (animator != null)
        {
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", velocity.y);
        }
    }
}