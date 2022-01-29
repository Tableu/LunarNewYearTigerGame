using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private InputActions _inputActions;
    private Vector2 _prevDirection;
    private bool _attacking;
    private int _attackFrame;
    public int attackFrameCount;
    public PlayerReferences PlayerReferences;
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
            _attacking = true;
            _attackFrame = 0;
            Animator.SetTrigger("ForwardAttack");
        }

        if (_attacking)
        {
            if (_attackFrame >= attackFrameCount)
            {
                _attacking = false;
            }
            else
            {
                _attackFrame++;
            }
        }
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        if (_attacking)
        {
            return;
        }
        if (callbackContext.performed)
        {
            Vector2 direction = callbackContext.ReadValue<Vector2>();
            if (!direction.Equals(Vector2.zero))
            {
                if (direction.x > 0 && direction.x > _prevDirection.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    Animator.SetTrigger("SideWalk");
                }
                else if (direction.x < 0 && direction.x < _prevDirection.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    Animator.SetTrigger("SideWalk");
                }

                if (direction.y > 0 && direction.y > _prevDirection.y)
                {
                    Animator.SetTrigger("BackwardWalk");
                }
                else if (direction.y < 0 && direction.y < _prevDirection.y)
                {
                    Animator.SetTrigger("ForwardWalk");
                }
                Animator.ResetTrigger("Idle");
                _prevDirection = direction;
            }
        }
        if(callbackContext.canceled)
        {
            Animator.SetTrigger("Idle");
            _prevDirection = Vector2.zero;
        }
    }

    private void OnDestroy()
    {
        _inputActions.Player.Movement.performed -= OnMovement;
        _inputActions.Player.Movement.canceled -= OnMovement;   
    }
}
