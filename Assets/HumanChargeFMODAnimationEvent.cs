using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanChargeFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayHumanCharge()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}