using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private const int inventorySize = 12;
    public struct InventoryItem
    {
        private string name;
        private string displayName;
        private string displayDescription;
        private Sprite displayImage;
        private GameObject gameObject;


        public InventoryItem(string objName, GameObject go)
        {
            name = go.name;
            displayName = objName;
            displayDescription = "NEEDS DESCTIPTUION";
            displayImage = null;
            gameObject = go;
        }
}

    public GameObject[] inventory;




    // Start is called before the first frame update
    void Start()
    {
        inventory = new GameObject[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckForSpace()
    {
        if (true) { return true; }
        else { return false; }
    }

    private int GetFirstFreeSlot()
    {
        return 0;
    }

    public void AddToInventory(GameObject go)
    {
        
    }


}

