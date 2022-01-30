using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageTriggerFMOD : MonoBehaviour
{
    public string path;
    public Health script;
    public int OldHealth;

    // Start is called before the first frame update
    void Start()
    {
        OldHealth = script.CurrentHealth;



    }

    // Update is called once per frame
    void Update()
    {
        if (OldHealth != script.CurrentHealth)
            FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
        OldHealth = script.CurrentHealth;
    }
}