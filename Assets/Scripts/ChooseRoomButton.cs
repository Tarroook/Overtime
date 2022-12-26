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
    private GameObject mainText;
    public int maxElementsPerRow = 3;

    private List<GameObject> images;

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
            Debug.LogWarning("No layout found in chooseRoomUI : " + roomNB);
        mainText = transform.GetChild(0).gameObject;
        if (mainText == null)
            Debug.LogWarning("No text found in chooseRoomUI : " + roomNB);
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

    void addImage(Sprite sprite) // move text + stack
    {
        if(images == null)
        {
            images = new List<GameObject>();
            mainText.GetComponent<RectTransform>().offsetMax = new Vector2(0, 60);
        }
        GameObject element = new GameObject("Image");

        RectTransform rectTransform = element.AddComponent<RectTransform>();

        Image image = element.AddComponent<Image>();
        image.sprite = sprite;

        element.transform.SetParent(layout.transform, false);
    }
}
