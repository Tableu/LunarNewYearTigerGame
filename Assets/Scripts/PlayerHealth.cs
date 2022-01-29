using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private bool _inKnockback;
    private int _knockbackFrameIndex;
    private Vector2 _knockbackDirection;
    private List<float> _knockback;
    private Damage _dmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_inKnockback)
        {
            _knockbackFrameIndex++;
            if (_knockbackFrameIndex < _knockback.Count)
            {
                transform.Translate(_knockback[_knockbackFrameIndex]*_knockbackDirection);
            }
            else
            {
                _inKnockback = false;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _dmg = other.gameObject.GetComponent<Damage>();
        if (_dmg != null && !_inKnockback)
        {
            health -= _dmg.damage;
            _knockbackDirection = (transform.position - other.transform.position).normalized;
            _inKnockback = true;
            _knockback = _dmg.knockbackStrength;
            transform.Translate(_knockback[0] * _knockbackDirection);
            _knockbackFrameIndex = 1;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
