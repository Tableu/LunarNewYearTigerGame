using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    private InputActions _inputActions;
    private bool _isDashing;
    private bool _isCharging;
    private float _dashTimer;
    private Vector2 _playerDirection;
    public PlayerReferences PlayerReferences;
    public float ChargeSpeed;
    public float ChargeStart;
    public float DashSpeed;
    public float DashDuration;
    public float DashCooldown;

    private void Start()
    {
        _maxSpeed = MaxSpeed;
        _inputActions = PlayerReferences.InputActions;
        _inputActions.Player.Dash.started += Dash;
        _inputActions.Player.Movement.performed += SetPlayerDirection;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlayerReferences.DamageMultiplier = 1;
            _maxSpeed = ChargeSpeed;
            ChargeStart = Time.time;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (Time.time - ChargeStart > 1)
            {
                PlayerReferences.DamageMultiplier = 2;
            }
            _maxSpeed = MaxSpeed;
        }
    }

    void FixedUpdate()
    {
        if (_isDashing)
        {
            if (Time.time - _dashTimer < DashDuration)
            {
                Rigidbody2D.AddForce(_playerDirection*DashSpeed, ForceMode2D.Impulse);
            }
            else
            {
                _isDashing = false;
                _dashTimer = Time.time;
            }
        }
        else
        {
            Vector2 dir = _inputActions.Player.Movement.ReadValue<Vector2>();
            if (!dir.Equals(Vector2.zero))
            {
                Rigidbody2D.AddForce(dir*Speed, ForceMode2D.Impulse);
                if (Mathf.Abs(Rigidbody2D.velocity.x) > _maxSpeed)
                {
                    Rigidbody2D.velocity = new Vector2(_maxSpeed * dir.x, Rigidbody2D.velocity.y);
                }

                if (Mathf.Abs(Rigidbody2D.velocity.y) > _maxSpeed)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,_maxSpeed * dir.y);
                }

                if (dir.x == 0)
                {
                    Rigidbody2D.velocity = new Vector2(0,Rigidbody2D.velocity.y);
                }

                if (dir.y == 0)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
                }
            }
            else
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (Time.time - _dashTimer >= DashDuration)
        {
            _isDashing = true;
            _dashTimer = Time.time;
        }
    }

    private void SetPlayerDirection(InputAction.CallbackContext context)
    {
        _playerDirection = _inputActions.Player.Movement.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Dash.started -= Dash;
        _inputActions.Player.Movement.performed -= SetPlayerDirection;
    }
}
