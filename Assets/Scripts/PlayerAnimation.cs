using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private InputActions _inputActions;
    private Vector2 _prevDirection;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !Attacking)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePos-(Vector2)transform.position).normalized;
            SetDirection(direction, Vector2.zero);
            Animator.ResetTrigger("Release");
            Animator.SetTrigger("Attack");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && Attacking)
        {
            Animator.SetTrigger("Release");
        }
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        if (Attacking)
        {
            return;
        }
        if (callbackContext.performed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = (mousePos-(Vector2)transform.position).normalized;

            SetDirection(direction, callbackContext.ReadValue<Vector2>());
            Animator.SetTrigger("Walk");
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
        float angle = Vector2.Angle(new Vector2(1,0), mouseDirection);
        Animator.SetBool("Reverse", false);
        
        if (angle <= 180 && angle > 135)
        {
            Animator.SetBool("Side", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            var pos = SideHitbox.localPosition;
            if (Mathf.Sign(pos.x) > 0)
            {
                SideHitbox.localPosition = new Vector3(pos.x * -1, pos.y, pos.z);
            }

            if (movementDirection.x > 0)
            {
                Animator.SetBool("Reverse", true);
            }

            Renderer.flipX = true;
        }
        else if (angle <= 45 && angle > 0)
        {
            Animator.SetBool("Side", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            var pos = SideHitbox.localPosition;
            SideHitbox.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
            if (movementDirection.x < 0)
            {
                Animator.SetBool("Reverse", true);
            }
            Renderer.flipX = false;
        }else if (angle <= 135 && angle > 45)
        {
            if (mouseDirection.y > 0)
            {
                Animator.SetBool("Backward", true);
                Animator.SetBool("Forward", false);
                if (movementDirection.y < 0)
                {
                    Animator.SetBool("Reverse", true);
                }
            }
            else
            {
                Animator.SetBool("Forward", true);
                Animator.SetBool("Backward", false);
                if (movementDirection.y > 0)
                {
                    Animator.SetBool("Reverse", true);
                }
            }
            Animator.SetBool("Side", false);
            Renderer.flipX = false;
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Movement.performed -= OnMovement;
        _inputActions.Player.Movement.canceled -= OnMovement;   
    }
}
