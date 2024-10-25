using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerInventory playerInventory;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerInventory = GetComponent<PlayerInventory>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetInventorySize()
    { 
        return playerInventory.GetInventorySize(); 
    }

    public void AddToInventory(PlayerInventory.InventoryItem item)
    {
        playerInventory.AddToInventory(item);
        gameManager.UpdateInventoryDisplay(playerInventory.GetInventory());
    }

    /*
    public bool CheckForSpace()
        { return playerInventory.CheckForSpace(); }
    */

    public bool CheckForSpace()
    {
        PlayerInventory.InventoryItem[] inventory = playerInventory.GetInventory();
        for (int i = 0; i < playerInventory.GetInventorySize(); i++)
        {
            if (inventory[i].GetEntry() == false)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckForOccSlot()
    {
        if(playerInventory.GetFirstOccupiedSlot() != -1)
        {
            return true;
        }
        return false;
    }

    public int GetFirstOccupiedSlot()
    {
        return playerInventory.GetFirstOccupiedSlot();
    }


    public void drop(int val)
    {
        PlayerInventory.InventoryItem item = playerInventory.GetItemAtSlot(val);
        GameObject objToGen = Resources.Load(item.GetObjPath()).GameObject();

        // HOLDPOINT TRANSFORM POS NEEDS TO BE RAYCAST HERE, OTHERWISE YOU CAN PLACE UNDER THE GROUND
        GameObject objGenned = Instantiate(objToGen, playerController.GetHoldPoint().transform.position, Quaternion.identity);

        objGenned.GetComponent<PickupObjectData>().SetParameters(item.GetDisplayName(), item.getDisplayDescription(), item.GetDisplaySprite(), item.GetObjPath());
        objGenned.name  = item.getObjName();

        playerInventory.ClearSlot(val);

        gameManager.UpdateInventoryDisplay(playerInventory.GetInventory());
        playerInventory.Vomit();
    }

}
