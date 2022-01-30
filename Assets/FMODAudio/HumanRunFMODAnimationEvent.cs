using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRunFMODAnimationEvent : MonoBehaviour
{
    public string path;
    void PlayHumanRun()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}
