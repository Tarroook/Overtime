using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectionUIScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        ModifierInteractable.onPickedItem += showUI;
        ChooseRoomButton.onSelectedRoomButton += hideUI;
    }

    private void OnDisable()
    {
        ModifierInteractable.onPickedItem -= showUI;
        ChooseRoomButton.onSelectedRoomButton -= hideUI;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void showUI(Modifier mod)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void hideUI(int roomNB)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
