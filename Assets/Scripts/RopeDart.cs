using UnityEngine;
using UnityEngine.InputSystem;

public class RopeDart : MonoBehaviour
{
    private float _size;
    private bool _throwing;
    private bool _returning;
    private Vector2 _target;
    private float _angle;
    public float Speed;
    public float MaxSize;
    public SpriteRenderer SpriteRenderer;
    void Start()
    {
        _size = 0;
    }
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !_throwing && !_returning)
        {
            _throwing = true;
            _target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        if (_size < MaxSize && _throwing)
        {
            SpriteRenderer.size = new Vector2(_size, SpriteRenderer.size.y);
            _size += Speed;
            if (_size >= MaxSize)
            {
                _throwing = false;
                _returning = true;
            }
        }

        if (_returning)
        {
            SpriteRenderer.size = new Vector2(_size, SpriteRenderer.size.y);
            _size -= Speed;
            if (_size < 0)
            {
                _throwing = false;
                _returning = false;
                SpriteRenderer.size = new Vector2(0, SpriteRenderer.size.y);
            }
        }

        if (_returning || _throwing)
        {
            var direction = (_target-(Vector2)transform.position).normalized;
            _angle = Vector2.Angle(direction, Vector2.right);
            if (direction.y < 0)
            {
                _angle = -_angle;
            }
            transform.rotation = Quaternion.Euler(0,0,_angle);
        }
    }
}