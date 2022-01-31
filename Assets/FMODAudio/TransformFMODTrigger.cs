using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformFMODTrigger : MonoBehaviour
{
    public string path;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { if (Keyboard.current[Key.E].wasPressedThisFrame)
        
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
         

        }
        
    }
    
}