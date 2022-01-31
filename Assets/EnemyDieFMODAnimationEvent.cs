using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayEnemyDie()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}