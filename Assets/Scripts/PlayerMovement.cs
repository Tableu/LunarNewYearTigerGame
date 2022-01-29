using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputActions _inputActions;
    private bool _isDashing;
    private float _dashStart;
    private Vector2 _playerDirection;
    public PlayerReferences PlayerReferences;
    public float Speed;
    public float DashSpeed;
    public float DashDuration;

    private void Start()
    {
        _inputActions = PlayerReferences.InputActions;
        _inputActions.Player.Dash.started += Dash;
        _inputActions.Player.Movement.performed += SetPlayerDirection;
    }

    void FixedUpdate()
    {
        if (_isDashing)
        {
            if (Time.time - _dashStart < DashDuration)
            {
                transform.Translate(_playerDirection*DashSpeed);
            }
            else
            {
                _isDashing = false;
            }
        }
        else
        {
            transform.Translate(_inputActions.Player.Movement.ReadValue<Vector2>()*Speed);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        _isDashing = true;
        _dashStart = Time.time;
    }

    private void SetPlayerDirection(InputAction.CallbackContext context)
    {
        _playerDirection = _inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
