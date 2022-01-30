using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttackFMODAnimationEvent : MonoBehaviour
{
    public string path; 
    // Start is called before the first frame update
    void PlayTigerAttack()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
        
    }

   
}
