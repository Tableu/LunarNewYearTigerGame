using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public GameObject Player;
    public float AggroRange;
    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy != null)
            {
                var dist = ((Vector2) Player.transform.position - (Vector2) Enemy.transform.position).sqrMagnitude;
                if (Mathf.Abs(dist) < AggroRange)
                {
                    EnemyMovement em = Enemy.GetComponent<EnemyMovement>();
                    if (em != null)
                    {
                        em.Player = Player;
                    }
                }
            }
        }
    }
}
