using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public Rigidbody2D Rigidbody2D;
    public Movement Movement;
    public Slider HealthBar;
    private bool _inKnockback;
    private Vector2 _knockbackDirection;
    private float _knockbackStart;
    private Damage _dmg;

    private void Start()
    {
        if (HealthBar != null)
        {
            HealthBar.maxValue = MaxHealth;
            HealthBar.value = CurrentHealth;
        }
    }

    void FixedUpdate()
    {
        if (_inKnockback)
        {
            if (Time.time - _knockbackStart < _dmg.knockbackDuration)
            {
                Rigidbody2D.AddForce(_dmg.knockbackStrength*_knockbackDirection, ForceMode2D.Impulse);
            }
            else
            {
                _inKnockback = false;
                if (Movement)
                {
                    Movement.enabled = true;
                }
            }
        }
    }

    public void ApplyDamage(Damage dmg)
    {
        if (_inKnockback)
        {
            return;
        }
        _dmg = dmg;
        CurrentHealth -= _dmg.damage;
        _knockbackDirection = (transform.position - dmg.transform.position).normalized;
        _inKnockback = true;
        Rigidbody2D.AddForce(_dmg.knockbackStrength*_knockbackDirection, ForceMode2D.Impulse);
        if (Movement != null)
        {
            Movement.enabled = false;
        }
        _knockbackStart = Time.time;
        if (HealthBar != null)
        {
            HealthBar.value = CurrentHealth;
        }
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
