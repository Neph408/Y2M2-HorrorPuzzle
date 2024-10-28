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

    public void UpdateDisplay(string title, string description, Sprite largeSprite)
    {
        itemTitleDisplay.text = title;
        itemDescriptionDisplay.text = description;
        itemLargeSpriteDisplay.sprite = largeSprite;
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
