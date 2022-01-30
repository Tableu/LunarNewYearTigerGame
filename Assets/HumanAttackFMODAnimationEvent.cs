using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttackFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayHumanAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}