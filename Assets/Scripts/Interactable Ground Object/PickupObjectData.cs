using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectData : MonoBehaviour
{
    private GameManager gameManager;
    // stores info on the object for pickup
    private string objName;
    private int objID;

    [SerializeField] private string displayName;
    [SerializeField] private string displayDescription;
    [SerializeField] private Sprite displaySprite;
    [SerializeField] private GameObject prefabAsset;
    





    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        objName = gameObject.name;
        objID = gameObject.GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParameters(string n_displayName, string n_displayDescription, Sprite n_displaySprite, GameObject n_prefabAsset)
    {
        displayName = n_displayName;
        displayDescription = n_displayDescription;
        displaySprite = n_displaySprite;
        prefabAsset = n_prefabAsset;
    }


    public PlayerInventory.InventoryItem GetInventoryItem()
    {
        return new PlayerInventory.InventoryItem(objName,objID,displayName,displayDescription,displaySprite,prefabAsset);
    }

}
