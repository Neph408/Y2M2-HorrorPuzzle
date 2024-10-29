using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PickupObjectData : MonoBehaviour
{
    private GameManager gameManager;
    // stores info on the object for pickup
    private string objName;
    private int objID;

    [SerializeField] private string displayName;
    [SerializeField] private string displayDescription;
    [SerializeField] private Sprite displaySpriteSmall;
    [SerializeField] private Sprite displaySpriteLarge;
    [SerializeField] private string prefabAsset;
    [SerializeField] private bool isReadable;
    [SerializeField] private Sprite readableSprite;
    [SerializeField] private string readableString;







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

    public void SetParameters(string n_displayName, string n_displayDescription, Sprite n_displaySpriteSmall, Sprite n_displaySpriteLarge, string n_prefabAsset, bool n_isReadable, Sprite n_readableSprite, string n_readableString)
    {
        displayName = n_displayName;
        displayDescription = n_displayDescription;
        displaySpriteSmall = n_displaySpriteSmall;
        displaySpriteLarge = n_displaySpriteLarge;
        prefabAsset = n_prefabAsset;
        isReadable = n_isReadable;
        readableSprite = n_readableSprite;
        readableString = n_readableString;
    }

    public string GetDisplayName()
    { 
        return displayName; 
    }

    public PlayerInventory.InventoryItem GetInventoryItem()
    {
        return new PlayerInventory.InventoryItem(objName,objID,displayName,displayDescription,displaySpriteSmall,displaySpriteLarge,prefabAsset, isReadable, readableSprite, readableString);
    }


    public bool GetIsReadable()
    {
        return isReadable; 
    }
}
