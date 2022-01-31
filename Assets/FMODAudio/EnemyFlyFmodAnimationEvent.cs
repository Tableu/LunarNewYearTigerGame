using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyFmodAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayEnemyFly()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}