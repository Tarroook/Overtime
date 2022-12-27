using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mod")]
public class ModifierData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public string description;
    public string interactText = "Pick up";
}
