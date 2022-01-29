using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputActions _inputActions;
    public float speed;
    
    private void Awake()
    {
        _inputActions = new InputActions();
    }

    void Update()
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
}
