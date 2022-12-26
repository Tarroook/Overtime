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

    private GameObject layout;
    public int maxElementsPerRow = 3;

    //int currentRow = 0;

    private void OnEnable()
    {
        ItemInteractable.onPickedItem += addMod;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => clickedButton());
        layout = transform.GetChild(1).gameObject;
        if (layout == null)
            Debug.LogWarning("No vertical layout found in chooseRoomUI");
    }

    private void addMod(Modifier mod)
    {
        modToAdd = mod;
    }

    private void clickedButton()
    {
        foreach(Transform child in modToAdd.transform)
        {
            if(child.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                addImage(child.gameObject.GetComponent<SpriteRenderer>().sprite);
            }
        }
        if (onSelectedRoomButton != null)
            onSelectedRoomButton(roomNB - 1);
    }

    void addImage(Sprite sprite)
    {/*
        HorizontalLayoutGroup horizontalLayout;
        if (layout.transform.childCount == 0)
        {
            horizontalLayout = addNewRow().GetComponent<HorizontalLayoutGroup>();
        }
        else
        {
            horizontalLayout = layout.transform.GetChild(currentRow).GetComponent<HorizontalLayoutGroup>();

            if (horizontalLayout.transform.childCount >= maxElementsPerRow)
            {
                currentRow++;
                horizontalLayout = addNewRow().GetComponent<HorizontalLayoutGroup>();
            }
        }

        GameObject element = new GameObject("Element");

        RectTransform rectTransform = element.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 100);

        Image image = element.AddComponent<Image>();
        image.sprite = sprite;

        element.transform.SetParent(horizontalLayout.transform, false);
        */
        GameObject element = new GameObject("Image");

        RectTransform rectTransform = element.AddComponent<RectTransform>();

        Image image = element.AddComponent<Image>();
        image.sprite = sprite;

        element.transform.SetParent(layout.transform, false);
    }

    GameObject addNewRow()
    {
        GameObject row = new GameObject("Row");

        RectTransform rectTransform = row.AddComponent<RectTransform>();

        HorizontalLayoutGroup layoutGroup = row.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childControlWidth = false;
        layoutGroup.childForceExpandWidth = false;
        row.transform.SetParent(layout.transform, false);

        return row;
    }
}
