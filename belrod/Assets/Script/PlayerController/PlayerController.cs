using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 rawInput = new Vector2(moveX, moveY);

        bool isMoving = rawInput.sqrMagnitude > 0.01f;

        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            // Направление движения
            moveInput = rawInput.normalized;

            // Передаем направление в Walk Blend Tree
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);

            // Запоминаем последнее направление
            lastMoveDirection = moveInput;

            // Передаем последнее направление в Idle Blend Tree
            animator.SetFloat("LastMoveX", lastMoveDirection.x);
            animator.SetFloat("LastMoveY", lastMoveDirection.y);
        }
        else
        {
            moveInput = Vector2.zero;

            animator.SetFloat("MoveX", 0f);
            animator.SetFloat("MoveY", 0f);

            // LastMoveX и LastMoveY НЕ меняем
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + moveInput * moveSpeed * Time.fixedDeltaTime
        );
    }
}