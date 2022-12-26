using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRoomButton : MonoBehaviour
{
    public int roomNB = 0;
    private Button button;
    private Modifier modToAdd;

    public delegate void clickButtonAction(int roomNB);
    public static event clickButtonAction onSelectedRoomButton;

    private void OnEnable()
    {
        ItemInteractable.onPickedItem += addMod;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => clickedButton());
    }

    private void addMod(Modifier mod)
    {
        modToAdd = mod;
    }

    private void clickedButton()
    {
        if (onSelectedRoomButton != null)
            onSelectedRoomButton(roomNB - 1);
    }
}
