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
    private InteractCaster interactCaster;

    [SerializeField] private LayerMask placementRayCastLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerInventory = GetComponent<PlayerInventory>();
        playerController = GetComponent<PlayerController>();
        interactCaster = GetComponent<InteractCaster>();
        gameManager.GetInventoryMenuController().AssignInventoryManager(this);
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

    public PlayerInventory.InventoryItem GetItemAtSlot(int val)
    {
        return playerInventory.GetItemAtSlot(val);
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


    public void Drop(int val)
    {
        PlayerInventory.InventoryItem item = playerInventory.GetItemAtSlot(val);
        GameObject objToGen = Resources.Load(item.GetObjPath()).GameObject();

        // NNED TO ADD VERTICAL OFFSET PER OBJECTY TO ENSURE OBJ IS NOT PLACED IN GROUND
        // this is like, if i can find the time sorta deal
        // aint necessary, just would look better
        RaycastHit hit;
        Vector3 dropPoint;
        if(Physics.Raycast(transform.position, playerController.GetCameraSwivel().transform.forward, out hit, interactCaster.getInteractDistance(), placementRayCastLayerMask))
        {
            dropPoint = hit.point + transform.up * 0.5f; //temp
            //Debug.Log("ray pouint");
        }
        else
        {
            dropPoint = playerController.GetHoldPoint().transform.position;
            //Debug.Log("no ray");
        }
        GameObject objGenned = Instantiate(objToGen, dropPoint, Quaternion.identity);

        objGenned.GetComponent<PickupObjectData>().SetParameters(item.GetDisplayName(), item.getDisplayDescription(), item.GetDisplaySpriteSmall(),item.GetDisplaySpriteLarge(), item.GetObjPath(), item.GetIsReadable(), item.GetReadableSprite(), item.GetReadableString());
        objGenned.name  = item.getObjName();

        playerInventory.ClearSlot(val);

        gameManager.UpdateInventoryDisplay(playerInventory.GetInventory());
        playerInventory.Vomit();
    }

}
