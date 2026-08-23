using UnityEngine;
using UnityEngine.InputSystem; // Подключаем новую систему ввода

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down; // По умолчанию смотрим вниз

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Этот метод вызывается автоматически новой системой ввода (экшен должен называться "Move")
    private void OnMove(InputValue value)
    {
        // Получаем вектор направления из кнопок WASD
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            // Передаем текущее направление для анимации ходьбы
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);

            // Запоминаем это направление
            lastMoveDirection = moveInput;
        }
        else
        {
            // Если стоим, обнуляем текущую ходьбу
            animator.SetFloat("MoveX", 0f);
            animator.SetFloat("MoveY", 0f);
        }

        // Постоянно передаем последнее направление, чтобы Idle Blend Tree знал, куда смотреть
        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}