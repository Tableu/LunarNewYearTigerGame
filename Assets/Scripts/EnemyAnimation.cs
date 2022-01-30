using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public EnemyMovement Enemy;
    public Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.Player != null)
        {
            Vector2 direction = ((Vector2)Enemy.Player.transform.position-(Vector2)transform.position).normalized;
            SetDirection(direction);
        }
    }

    private void SetDirection(Vector2 playerDirection)
    {
        float angle = Vector2.Angle(new Vector2(1,0), playerDirection);

        if (angle <= 180 && angle > 135)
        {
            Animator.SetBool("Side", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if (angle <= 45 && angle > 0)
        {
            Animator.SetBool("Side", true);
            Animator.SetBool("Backward", false);
            Animator.SetBool("Forward", false);
            transform.rotation = Quaternion.Euler(0,180,0);
        }else if (angle <= 135 && angle > 45)
        {
            if (playerDirection.y > 0)
            {
                Animator.SetBool("Side", false);
                Animator.SetBool("Backward", true);
                Animator.SetBool("Forward", false);
            }
            else
            {
                Animator.SetBool("Side", false);
                Animator.SetBool("Backward", false);
                Animator.SetBool("Forward", true);
            }
        }
    }
}
