using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public PlayerInput Input;
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;
    public float AirControlMultiplier = 0.33f; // Коэффициент уменьшения скорости в воздухе
    public float SprintMultiplier = 2f;       // Коэффициент ускорения

    private InputAction _move;
    private InputAction _jump;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        // Получаем действия Move и Jump
        _move = Input.actions["Move"];
        _jump = Input.actions["Jump"];

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }
    }

    void Update()
    {
        // Обработка перемещения
        var inputMove = _move.ReadValue<Vector2>();

        // Если объект в воздухе, уменьшить скорость
        float currentSpeed = _isGrounded ? MoveSpeed : MoveSpeed * AirControlMultiplier;

        // Проверка нажатия Shift для ускорения
        if (Keyboard.current.leftShiftKey.isPressed && _isGrounded) // Ускоряем только на земле
        {
            currentSpeed *= SprintMultiplier;
        }

        var move = new Vector3(inputMove.x, 0, inputMove.y) * currentSpeed * Time.deltaTime;
        transform.Translate(move);

        // Прыжок при нажатии Space, если игрок на земле
        if (_jump.triggered && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Определяем, касается ли объект земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Определяем, что объект больше не касается земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
