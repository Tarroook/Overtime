using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject imagePrefab;

    private List<GameObject> images;

    //int currentRow = 0;

    private void OnEnable()
    {
        ModifierInteractable.onPickedItem += addMod;
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
        addImage(modToAdd.modData.sprite);
        if (onSelectedRoomButton != null)
            onSelectedRoomButton(roomNB - 1);
    }

    void addImage(Sprite sprite)
    {
        if (images == null)
        {
            images = new List<GameObject>();
            mainText.GetComponent<RectTransform>().offsetMax = new Vector2(0, 60);
        }

        bool isAleradyIn = false;
        foreach (GameObject imageObject in images)
        {
            if (imageObject.GetComponent<Image>().sprite.Equals(sprite))
            {
                imageObject.transform.GetComponentInChildren<ModCounter>().count++;
                isAleradyIn = true;
                break;
            }
        }
        if (!isAleradyIn)
        {
            GameObject newImage = Instantiate(imagePrefab, layout.transform);
            newImage.GetComponent<Image>().sprite = sprite;
            images.Add(newImage);
        }
        /*
        int count = 0;
        foreach (GameObject imageObject in sprites)
        {
            Image img = imageObject.GetComponent<Image>();
            if (img.sprite.Equals(sprite))
            {
                count++;
                TextMeshProUGUI tmp = imageObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tmp.SetText(count.ToString());
            }
        }

        GameObject element = new GameObject("Image");
        RectTransform rectTransform = element.AddComponent<RectTransform>();
        Image image = element.AddComponent<Image>();
        image.sprite = sprite;
        element.transform.SetParent(layout.transform, false);

        GameObject textObject = new GameObject("Text");
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.SetText((count + 1).ToString());
        text.fontSize = image.rectTransform.sizeDelta.y / 2; // Half the height of the image
        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(50, -50); // Offset from the bottom right corner of the image
        textObject.transform.SetParent(image.transform, false);
        */
    }
}
