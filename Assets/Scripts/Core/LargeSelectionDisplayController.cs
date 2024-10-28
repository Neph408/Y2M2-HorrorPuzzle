using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LargeSelectionDisplayController : MonoBehaviour
{

    [SerializeField] private Image itemLargeSpriteDisplay;
    [SerializeField] private TextMeshProUGUI itemTitleDisplay;
    [SerializeField] private TextMeshProUGUI itemDescriptionDisplay;

    [SerializeField] private Button readButton;

    private InventoryMenuController inventoryMenuController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignIMC(InventoryMenuController imc)
    {
        inventoryMenuController = imc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay(string title, string description, Sprite largeSprite, bool readable)
    {
        itemTitleDisplay.text = title;
        itemDescriptionDisplay.text = description;
        itemLargeSpriteDisplay.sprite = largeSprite;
        readButton.interactable = readable;
    }

    public void PressRead()
    {
        inventoryMenuController.ReadSelected();
    }

    public void PressDrop()
    {
        inventoryMenuController.DropSelected();
    }

}
