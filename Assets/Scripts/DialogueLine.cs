using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Tarook/Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    public DialogueActor narrator;
    [TextArea(1, 4)]public string line;
}
