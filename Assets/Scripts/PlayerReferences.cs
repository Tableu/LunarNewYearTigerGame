using System;
using UnityEngine;

[CreateAssetMenu(menuName = "References/Player References")]
[Serializable]
public class PlayerReferences : ScriptableObject
{
    public InputActions InputActions;
    public int DamageMultiplier;
    private void OnEnable()
    {
        InputActions = new InputActions();
        InputActions.Enable();
        DamageMultiplier = 1;
    }
}
