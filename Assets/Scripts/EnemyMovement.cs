using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    { 
        _maxSpeed = MaxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        if (Player != null)
        {
            Rigidbody2D.velocity = Vector2.zero;
            direction = ((Vector2)Player.transform.position-(Vector2)transform.position).normalized;
            Rigidbody2D.AddForce(direction*Speed, ForceMode2D.Impulse);
        }
        if (Mathf.Abs(Rigidbody2D.velocity.x) > _maxSpeed)
        {
            Rigidbody2D.velocity = new Vector2(_maxSpeed*Mathf.Sign(direction.x), Rigidbody2D.velocity.y);
        }

        if (Mathf.Abs(Rigidbody2D.velocity.y) > _maxSpeed)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,Mathf.Sign(_maxSpeed*direction.y));
        }
    }
}
