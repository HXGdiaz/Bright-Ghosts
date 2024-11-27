using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public PlayerInput Input;
    public float MoveSpeed;

    private InputAction _move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _move = Input.actions["Move"];
    }

    private void Update()
    {
        var inputMove = _move.ReadValue<Vector2>();
        var move = new Vector3(inputMove.x, 0, inputMove.y) * MoveSpeed * Time.deltaTime;

        transform.Translate(move);
    }

}
