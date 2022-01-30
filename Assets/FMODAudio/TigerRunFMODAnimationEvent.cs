using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerRunFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayTigerRun()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}
