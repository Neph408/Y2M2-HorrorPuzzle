using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private const int inventorySize = 12;
    private GameManager gameManager;
    public struct InventoryItem
    {
        private string objName;
        private int id;
        private string displayName;
        private string displayDescription;
        private Sprite displaySprite;
        private GameObject prefabGameObject;
        private bool entry;


        public InventoryItem(string n_objName, int n_ID, string n_displayName, string n_displayDescription, Sprite n_displaySprite, GameObject n_prefabGameObject)
        {
            objName = n_objName;
            id = n_ID;
            displayName = n_displayName;
            displayDescription = n_displayDescription; 
            displaySprite = n_displaySprite;
            prefabGameObject = n_prefabGameObject;
            entry = true;
        }

        public InventoryItem(string n_objName, int n_ID, GameObject n_prefabGameObject)
        {
            objName = n_objName;
            id = n_ID;
            displayName = "$DISPLAY_NAME$";
            displayDescription = "$DISPLAY_DESCRIPTION$";
            displaySprite = GameManager.Instance.GetPlaceholderSprite();
            prefabGameObject = n_prefabGameObject;
            entry = true;
        }

        public string GetDataAsReadableString()
        {
            string temp = string.Empty;
            temp += "Name: " + objName + " | ";
            temp += "ID: " + id.ToString()+ " | ";
            temp += "DName: " + displayName+ " | ";
            temp += "DDesc: " + displayDescription+ " | ";
            temp += "SpriteName: " + displaySprite.name + " | ";
            temp += "PrefabName: " + prefabGameObject.name ; 
            return temp;
        }



        public void ClearEntry()
        {
            objName = null;
            id = -1;
            displayName = null;
            displayDescription = null;
            displaySprite = null;
            prefabGameObject = null;
            entry = false;
        }


        public bool GetEntry()
        {
            return entry;
        }
}

    public InventoryItem[] inventory;

    /*
     * 
     * THIS SHIT IS SO MESSY
     * MY GOD ITS HARD TO KEEP UIP WITH
     * IT HURTS MY PEA BRAIN
     * 
     */


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        inventory = new InventoryItem[inventorySize];
        foreach (InventoryItem item in inventory)
        {
            item.ClearEntry();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckForSpace()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].GetEntry() == false)
            {
                return true;
            }
        }
        return false;
    }

    private int GetFirstFreeSlot()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].GetEntry() == false)
            {
                return i; 
            }
        }
        Debug.LogWarning("PlayerInventory : GetFirstFreeSlot failed to find free slot");
        return -1; // will cause error, thats the plan
    }

    public void AddToInventory(InventoryItem iI)
    {
        inventory[GetFirstFreeSlot()] = iI;
        Vomit();
        
    }

    public void Vomit()
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.GetEntry())
            {
                Debug.Log(item.GetDataAsReadableString());
            }
            else
            {
                Debug.Log("Empty Entry");
            }
        }
    }

}

