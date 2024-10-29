using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryMenuController : MonoBehaviour
{
    private GameManager gameManager;
    private LargeSelectionDisplayController lsdc;
    private InventoryManager inventoryManager;

    private GameObject rpcgo;
    private ReadPanelController rpc;

    private InventorySlotController[] inventorySlotControllers;

    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private string emptySlotName;
    [SerializeField] private string emptySlotDescription;

    private int currentHover = 0;
    private int lastSelected = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rpc = GetComponentInChildren<ReadPanelController>();
        rpcgo = rpc.gameObject;
        lsdc = GetComponentInChildren<LargeSelectionDisplayController>();
        lsdc.AssignIMC(this);
        rpc.AssignIMC(this);//, gameManager);
        SetRPCVisibility(false);

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
        if(Input.GetKeyDown(gameManager.kc_OpenInventory))
        {
            currentHover = -1;
            UpdateLargeDisplay();
        }
        if(Input.GetKeyDown(gameManager.kc_AltTextView))
        {
            rpc.ToggleAltTextPanel();
        }
    }

    public void AssignInventoryManager(InventoryManager im) // i hate this, but it works, so it stays
    {
        inventoryManager = im;
    }

    public void UpdateInventoryDisplay(PlayerInventory.InventoryItem[] inventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetEntry() == true)
            {
                inventorySlotControllers[i].setText(inventory[i].GetDisplayName());
                inventorySlotControllers[i].setSprite(inventory[i].GetDisplaySpriteSmall());
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

    public void SetCurrentHover(int val)
    {
        currentHover = val;
        UpdateLargeDisplay();
    }

    public void SetLastSelected(int val)
    {
        if (inventoryManager.GetItemAtSlot(val).GetEntry())
        {
            //inventorySlotControllers[lastSelected].SetOverlayImageTransparency(255);
            lastSelected = val;
            //inventorySlotControllers[lastSelected].SetOverlayImageTransparency(128);
            UpdateLargeDisplay();
        }
        else
        {
            //playsound
        }
    }

    private void UpdateLargeDisplay(bool wasDropped = false)
    {
        PlayerInventory.InventoryItem item = inventoryManager.GetItemAtSlot((currentHover == -1) ? lastSelected : currentHover);
        if(item.GetEntry())
        {
            lsdc.UpdateDisplay(item.GetDisplayName(), item.getDisplayDescription(), item.GetDisplaySpriteLarge(), item.GetIsReadable());
        }
        /*else
        {
            lsdc.UpdateDisplay(emptySlotName, emptySlotDescription, emptySlotSprite, false);
        }*/
        if(wasDropped)
        {
            lsdc.UpdateDisplay(emptySlotName, emptySlotDescription, emptySlotSprite, false);
        }
       
    }


    public void DropSelected()
    {
        if(inventoryManager.GetItemAtSlot(lastSelected).GetEntry())
        {
            inventoryManager.Drop(lastSelected);
            UpdateLargeDisplay(true);
        }
        else
        {
            //playsound
        }
        
    }

    public void ReadSelected()
    {
        if (inventoryManager.GetItemAtSlot(lastSelected).GetEntry() && inventoryManager.GetItemAtSlot(lastSelected).GetIsReadable())
        {
            Debug.Log("reads");
            SetRPCVisibility(true);
            rpc.SetRPCDisplay(inventoryManager.GetItemAtSlot(lastSelected).GetReadableSprite(), inventoryManager.GetItemAtSlot(lastSelected).GetReadableString(), gameManager.getAutoAltText(), gameManager.kc_AltTextView);
        }
        else if (inventoryManager.GetItemAtSlot(lastSelected).GetEntry())
        {
            Debug.Log("no be readable");
        }
        else
        {
            Debug.Log("slot empty");
        }
    }


    public bool GetRPCVisibility()
    {
        return rpc.GetVisibility();
    }

    public void SetRPCVisibility(bool val)
    {
        rpc.SetVisibility(val);
        rpcgo.SetActive(val);
    }

}
