using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    private CanvasGroup cg;
    private Interact interact;
    private TextMeshProUGUI textUI;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        foreach(Transform child in GameObject.FindGameObjectWithTag("Player").transform)
        {
            if(child.GetComponent<Interact>() != null)
            {
                interact = child.GetComponent<Interact>();
                break;
            }
        }
        foreach (Transform child in transform)
        {
            if (child.GetComponent<TextMeshProUGUI>() != null)
            {
                textUI = child.GetComponent<TextMeshProUGUI>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(interact.interactables.Count > 0)
        {
            displayPrompt();
        }
        else
        {
            cg.alpha = 0;
        }
    }

    private void displayPrompt()
    {
        string text = "Interact";
        Interactable i = interact.interactables[0];
        if (i is ItemInteractable)
        {
            text = ((ItemInteractable)i).mod.interactText;
        }
        else if (i is Door)
        {
            if (((Door)i).isOpened)
                text = "Go to next room";
            else
                return;
        }
        textUI.SetText(text);

        cg.alpha = 1;
    }
}
