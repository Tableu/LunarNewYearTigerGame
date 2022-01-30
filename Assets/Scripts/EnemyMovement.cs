using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Rigidbody2D.velocity.x) > _maxSpeed)
        {
            Rigidbody2D.velocity = new Vector2(_maxSpeed, Rigidbody2D.velocity.y);
        }

        if (Mathf.Abs(Rigidbody2D.velocity.y) > _maxSpeed)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x,_maxSpeed);
        }
    }
}
