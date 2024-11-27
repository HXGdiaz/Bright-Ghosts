using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public PlayerInput Input;
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;
    public float AirControlMultiplier = 0.33f; // ����������� ���������� �������� � �������
    public float SprintMultiplier = 2f;       // ����������� ���������

    private InputAction _move;
    private InputAction _jump;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        // �������� �������� Move � Jump
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
        // ��������� �����������
        var inputMove = _move.ReadValue<Vector2>();

        // ���� ������ � �������, ��������� ��������
        float currentSpeed = _isGrounded ? MoveSpeed : MoveSpeed * AirControlMultiplier;

        // �������� ������� Shift ��� ���������
        if (Keyboard.current.leftShiftKey.isPressed && _isGrounded) // �������� ������ �� �����
        {
            currentSpeed *= SprintMultiplier;
        }

        var move = new Vector3(inputMove.x, 0, inputMove.y) * currentSpeed * Time.deltaTime;
        transform.Translate(move);

        // ������ ��� ������� Space, ���� ����� �� �����
        if (_jump.triggered && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����������, �������� �� ������ �����
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ����������, ��� ������ ������ �� �������� �����
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
