using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "Tarook/Mod Data")]
public class ModifierData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public string description;
    public string interactText = "Pick up";
}
