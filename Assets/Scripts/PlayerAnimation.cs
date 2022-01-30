using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private InputActions _inputActions;
    private Vector2 _currentDirection;
    private Vector2 _movementDirection;
    private bool _transformed = true;
    public bool Attacking;
    public bool Releasing;
    public PlayerReferences PlayerReferences;
    public SpriteRenderer Renderer;
    public Animator Animator;
    public Transform SideHitbox;
    public RopeDart RopeDart;
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
            Attacking = true;
            Animator.SetBool("Attacking", Attacking);
            Animator.ResetTrigger("Release");
            Animator.ResetTrigger("Attack");
            Animator.SetTrigger("Attack");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && Attacking)
        {
            Attacking = false;
            Animator.SetBool("Attacking", Attacking);
            Animator.SetTrigger("Release");
        }
        Animator.SetBool("Releasing", Releasing);
        
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
        }
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _movementDirection = callbackContext.ReadValue<Vector2>();
    }

    private void SetDirection(Vector2 mouseDirection, Vector2 movementDirection)
    {
        float angle = Vector2.Angle(new Vector2(1,0), mouseDirection);
        Animator.SetBool("Reverse", false);

        if (_movementDirection != Vector2.zero)
        {
            Animator.SetBool("Walk", true);
        }
        else
        {
            Animator.SetBool("Walk", false);
        }
        if (angle <= 180 && angle > 135)
        {
            RopeDart.Left();
            if(!_transformed || !Attacking)
                transform.rotation = Quaternion.Euler(0,180,0);
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
        }
        else if (angle <= 45 && angle > 0)
        {
            RopeDart.Right();
            if(!_transformed || !Attacking)
                transform.rotation = Quaternion.Euler(0,0,0);
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
        }else if (angle <= 135 && angle > 45)
        {
            if(!_transformed || !Attacking)
                transform.rotation = Quaternion.Euler(0,0,0);
            if (mouseDirection.y > 0)
            {
                RopeDart.Backward();
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
                RopeDart.Forward();
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
