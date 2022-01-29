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
        _inputActions.Player.Movement.started += Dash;
    }

    void FixedUpdate()
    {
        transform.Translate(_inputActions.Player.Movement.ReadValue<Vector2>()*speed);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        
    }
}
