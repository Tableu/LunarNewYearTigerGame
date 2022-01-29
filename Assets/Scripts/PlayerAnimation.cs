using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private InputActions _inputActions;
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
        
    }
    
    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        Vector2 direction = callbackContext.ReadValue<Vector2>();
        if (!direction.Equals(Vector2.zero))
        {
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                Animator.SetTrigger("SideWalk");  
            }else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
                Animator.SetTrigger("SideWalk");
            }
            
            if (direction.y > 0)
            {
                Animator.SetTrigger("BackwardWalk");   
            }else if (direction.y < 0)
            {
                Animator.SetTrigger("ForwardWalk");
            }
        }
        else
        {
            Animator.SetTrigger("Idle");
        }
    }
}
