using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerChargeFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayTigerCharge()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);

    }
}
