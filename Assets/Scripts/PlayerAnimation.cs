using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private InputActions _inputActions;
    private Vector2 _currentDirection;
    private Vector2 _movementDirection;
    private bool _transformed = true;
    public bool Attacking;
    public PlayerReferences PlayerReferences;
    public SpriteRenderer Renderer;
    public Animator Animator;
    public Transform SideHitbox;
    void Start()
    {
        _inputActions = PlayerReferences.InputActions;
        _inputActions.Player.Movement.performed += OnMovement;
        _inputActions.Player.Movement.canceled += OnMovement;
        _inputActions.Player.Transform.started += OnTransform;
        _inputActions.Player.Dash.started += OnDash;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos-(Vector2)transform.position).normalized;
        if (Mouse.current.leftButton.wasPressedThisFrame && !Attacking)
        {
            SetDirection(direction, Vector2.zero);
            Animator.SetBool("Attacking", true);
            Animator.ResetTrigger("Release");
            Animator.SetTrigger("Attack");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && Attacking)
        {
            Animator.SetTrigger("Release");
        }
        
        Animator.SetBool("Attacking", Attacking);
        SetDirection(direction, _movementDirection);
    }

    private void OnTransform(InputAction.CallbackContext callbackContext)
    {
        Animator.SetTrigger("Transform");
        Animator.SetBool("Transformed", !Animator.GetBool("Transformed"));
        _transformed = !_transformed;
    }

    private void OnDash(InputAction.CallbackContext callbackContext)
    {
        if (_transformed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePos - (Vector2) transform.position).normalized;
            SetDirection(direction, Vector2.zero);
            Animator.ResetTrigger("Idle");
            Animator.SetTrigger("Idle");
        }
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _movementDirection = callbackContext.ReadValue<Vector2>();
        if (Attacking)
        {
            return;
        }
        if (callbackContext.performed)
        {
            Animator.ResetTrigger("Idle");
        }
        if(callbackContext.canceled)
        {
            Animator.ResetTrigger("Idle");
            Animator.SetTrigger("Idle");
        }
    }

    private void SetDirection(Vector2 mouseDirection, Vector2 movementDirection)
    {
        if (Attacking)
        {
            return;
        }
        float angle = Vector2.Angle(new Vector2(1,0), mouseDirection);
        Animator.SetBool("Reverse", false);

        if (_movementDirection != Vector2.zero)
        {
            Animator.SetBool("Walk", true);
            Animator.ResetTrigger("Idle");
        }
        else
        {
            Animator.SetBool("Walk", false);
        }
        if (angle <= 180 && angle > 135)
        {
            if (_currentDirection == Vector2.left)
            {
                return;
            }
            Animator.SetBool("Left", true);
            Animator.SetBool("Right", false);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            //var pos = SideHitbox.localPosition;
            //if (Mathf.Sign(pos.x) > 0)
            //{
            //    SideHitbox.localPosition = new Vector3(pos.x * -1, pos.y, pos.z);
            //}

            if (movementDirection.x > 0)
            {
                Animator.SetBool("Reverse", true);
            }
            _currentDirection = Vector2.left;
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (angle <= 45 && angle > 0)
        {
            if (_currentDirection == Vector2.right)
            {
                return;
            }
            Animator.SetBool("Left", false);
            Animator.SetBool("Right", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            var pos = SideHitbox.localPosition;
            SideHitbox.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
            if (movementDirection.x < 0)
            {
                Animator.SetBool("Reverse", true);
            }
            _currentDirection = Vector2.right;
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if (angle <= 135 && angle > 45)
        {
            if (mouseDirection.y > 0)
            {
                if (_currentDirection == Vector2.down)
                {
                    return;
                }
                Animator.SetBool("Backward", true);
                Animator.SetBool("Forward", false);
                if (movementDirection.y < 0)
                {
                    Animator.SetBool("Reverse", true);
                }
                _currentDirection = Vector2.down;
            }
            else
            {
                if (_currentDirection == Vector2.up)
                {
                    return;
                }
                Animator.SetBool("Forward", true);
                Animator.SetBool("Backward", false);
                if (movementDirection.y > 0)
                {
                    Animator.SetBool("Reverse", true);
                }
                _currentDirection = Vector2.up;
            }
            Animator.SetBool("Left", false);
            Animator.SetBool("Right", false);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Movement.performed -= OnMovement;
        _inputActions.Player.Movement.canceled -= OnMovement;   
        _inputActions.Player.Transform.performed -= OnTransform;
        _inputActions.Player.Dash.started -= OnDash;
    }
}
