using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusicTrigger : MonoBehaviour
{ FMOD.Studio.EventInstance Music;
    public string path;
    public bool PlayOnAwake;

    // Start is called before the first frame update
    void Start()
    {
       Music = FMODUnity.RuntimeManager.CreateInstance(path);
        if (PlayOnAwake)
        {
            Music.start();
            Music.release();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
