using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Tarook/Dialogue/Dialogue")]
public class LinearDialogue : ScriptableObject
{
    public DialogueLine[] dialogue;
}
