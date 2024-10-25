using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuController : MonoBehaviour
{
    private GameManager gameManager;
    private InventorySlotController[] inventorySlotControllers;
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private string emptySlotName;
    [SerializeField] private string emptySlotDescription;
    // Start is called before the first frame update
    void Start()
    {
     
        gameManager = GameManager.Instance;

        inventorySlotControllers = new InventorySlotController[12];

        foreach (InventorySlotController controller in GetComponentsInChildren<InventorySlotController>())
        {
            inventorySlotControllers[controller.GetSlotIndex()] = controller;
        }



        SetupInventoryDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventoryDisplay(PlayerInventory.InventoryItem[] inventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetEntry() == true)
            {
                inventorySlotControllers[i].setText(inventory[i].GetDisplayName());
                inventorySlotControllers[i].setSprite(inventory[i].GetDisplaySprite());
            }
            else
            {
                inventorySlotControllers[i].setText(emptySlotName);
                inventorySlotControllers[i].setSprite(emptySlotSprite);
            }
            
        }

    }

    public void SetupInventoryDisplay()
    {
        foreach (InventorySlotController controller in inventorySlotControllers)
        {
            controller.setText(emptySlotName);
            controller.setSprite(emptySlotSprite);
        }
    }
}
