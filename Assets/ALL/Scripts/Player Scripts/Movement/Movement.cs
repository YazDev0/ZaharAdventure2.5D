using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;

    private CharacterController controller;
    private Animator animator;
    private bool isFacingRight = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // تأكد أن اللاعب يبدأ متجه لليمين (زاوية 90)
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        // قراءة المدخلات
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // === تغيير اتجاه اللاعب بالدوران ===
        if (horizontalInput > 0) // يتحرك لليمين
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                // دوران لليمين (90 درجة)
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (horizontalInput < 0) // يتحرك لليسار
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                // دوران لليسار (-90 درجة)
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }

        // === الحركة ===
        if (horizontalInput != 0)
        {
            // تحريك اللاعب في الاتجاه الذي يواجهه
            float moveAmount = moveSpeed * Time.deltaTime;
            controller.Move(transform.forward * moveAmount);

            // Animation حركة
            if (animator != null)
            {
                animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            // Animation وقوف
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }

    // دالة إضافية إذا احتجت تعرف اتجاه اللاعب من خارج الكود
    public bool IsFacingRight()
    {
        return isFacingRight;
    }

    public bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0;
    }
}