using Unity.Mathematics;
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
    void Start()
    {
        _inputActions = PlayerReferences.InputActions;
        _inputActions.Player.Movement.performed += OnMovement;
        _inputActions.Player.Movement.canceled += OnMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Animator.SetTrigger("ForwardAttack");
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
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
            float angle = Vector2.Angle(new Vector2(1,0), direction);

            if (angle <= 180 && angle > 135)
            {
                Animator.SetTrigger("SideWalk");
                Renderer.flipY = true;
                Renderer.flipX = false;
                if (direction.y < 0)
                {
                    angle = -angle;
                }
            }
            else if (angle <= 45 && angle > 0)
            {
                Animator.SetTrigger("SideWalk");
                Renderer.flipY = false;
                Renderer.flipX = false;
                if (direction.y < 0)
                {
                    angle = -angle;
                }
            }else if (angle <= 135 && angle > 45)
            {
                if (direction.y > 0)
                {
                    Animator.SetTrigger("BackwardWalk");
                    Renderer.flipX = false;
                    Renderer.flipY = false;
                }
                else
                {
                    Animator.SetTrigger("ForwardWalk");
                    Renderer.flipX = true;
                    Renderer.flipY = false;
                }
                angle += 270;
                
                Renderer.flipY = false;
            }
            transform.rotation = Quaternion.Euler(0,0,angle);
            Animator.ResetTrigger("Idle");
        }
        if(callbackContext.canceled)
        {
            Animator.ResetTrigger("Idle");
            Animator.SetTrigger("Idle");
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Movement.performed -= OnMovement;
        _inputActions.Player.Movement.canceled -= OnMovement;   
    }
}
