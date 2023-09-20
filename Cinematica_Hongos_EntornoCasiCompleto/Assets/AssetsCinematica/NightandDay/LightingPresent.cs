using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preser", order = 1)]
public class LightingPresent : ScriptableObject
{
    public Color ambientColor;
    public Color directionalColor;
    public Color fogColor;
   
}