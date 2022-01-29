using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputActions _inputActions;
    public PlayerReferences PlayerReferences;
    public float speed;

    private void Start()
    {
        _inputActions = PlayerReferences.InputActions;
    }

    void FixedUpdate()
    {
        transform.Translate(_inputActions.Player.Movement.ReadValue<Vector2>()*speed);
    }
}
