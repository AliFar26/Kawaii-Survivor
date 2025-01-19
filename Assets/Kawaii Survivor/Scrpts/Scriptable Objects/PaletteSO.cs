using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Palette", menuName ="Scriptable Objects/New Palette",order =0)]
public class PaletteSO : ScriptableObject
{
    [field: SerializeField] public Color[] levelColor {  get; private set; }
    [field: SerializeField] public Color[] levelOutLineColors {  get; private set; }
}
