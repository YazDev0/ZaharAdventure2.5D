using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 2f; // ارتفاع القفز
    public float gravity = -20f; // الجاذبية
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Timing")]
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.2f;

    [Header("References")]
    private CharacterController controller;
    private Animator animator;

    // Private variables
    private Vector3 velocity;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // إنشاء ground check إذا لم يكن موجوداً
        if (groundCheck == null)
        {
            GameObject checkPoint = new GameObject("GroundCheck");
            checkPoint.transform.parent = transform;
            checkPoint.transform.localPosition = new Vector3(0, -controller.height / 2 - 0.1f, 0);
            groundCheck = checkPoint.transform;
        }
    }

    void Update()
    {
        // التحقق من ملامسة الأرض
        CheckGrounded();

        // تحديث مؤقتات القفز
        UpdateJumpTimers();

        // قراءة مدخلات القفز
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }

        // محاولة القفز
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f && !isJumping)
        {
            PerformJump();
        }

        // تطبيق الجاذبية
        ApplyGravity();

        // تطبيق الحركة الرأسية
        ApplyVerticalMovement();

        // تحديث الأنيميشن
        UpdateAnimation();
    }

    void CheckGrounded()
    {
        // التحقق من ملامسة الأرض
        isGrounded = Physics.CheckSphere(groundCheck.position,
                     groundCheckRadius, groundLayer);

        // إعادة تعيين السرعة الرأسية إذا كان على الأرض
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // قوة خفيفة لأسفل للتأكد من الثبات على الأرض
            isJumping = false;
        }
    }

    void UpdateJumpTimers()
    {
        // تحديث مؤقت coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // تحديث مؤقت buffer
        jumpBufferCounter -= Time.deltaTime;
    }

    void PerformJump()
    {
        // حساب قوة القفز باستخدام الفيزياء: v = √(2 * g * h)
        float jumpForce = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // تطبيق قوة القفز
        velocity.y = jumpForce;

        // إعادة تعيين المؤقتات
        coyoteTimeCounter = 0f;
        jumpBufferCounter = 0f;
        isJumping = true;

        // تفعيل أنيميشن القفز
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }

    void ApplyGravity()
    {
        // تطبيق الجاذبية
        velocity.y += gravity * Time.deltaTime;
    }

    void ApplyVerticalMovement()
    {
        if (controller == null) return;

        // تطبيق الحركة الرأسية فقط
        Vector3 verticalMove = new Vector3(0, velocity.y, 0) * Time.deltaTime;
        controller.Move(verticalMove);
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", velocity.y);
            animator.SetBool("IsJumping", isJumping && !isGrounded);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // للوصول من scripts أخرى
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public float GetVerticalSpeed()
    {
        return velocity.y;
    }
}