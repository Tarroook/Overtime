using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ModCounter : MonoBehaviour
{
    public int count = 1;
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.SetText("");
    }

    private void Update()
    {
        if (count > 1)
            tmp.SetText("x" + count);
        else
            tmp.SetText("");
    }
}
