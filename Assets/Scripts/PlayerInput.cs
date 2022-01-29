using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActions _inputActions;
    public float speed;
    public Animator Animator;
    
    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Movement.performed += OnMovement;
        _inputActions.Player.Movement.canceled += OnMovement;
    }

    void FixedUpdate()
    {
        transform.Translate(_inputActions.Player.Movement.ReadValue<Vector2>()*speed);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        Vector2 direction = callbackContext.ReadValue<Vector2>();
        if (!direction.Equals(Vector2.zero))
        {
            if (direction.x > 0)
            {
                
            }else if (direction.x < 0)
            {
                
            }
            
            if (direction.y > 0)
            {
                Animator.SetTrigger("BackwardWalk");   
            }else if (direction.y < 0)
            {
                Animator.SetTrigger("ForwardWalk");
            }
            Animator.SetBool("Idle", false);
        }
        else
        {
            Animator.SetBool("Idle", true);
        }
    }
}
