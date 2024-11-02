using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
    for all your non-movement or otherwise player-controlled mechanic needs
     */
    GameManager gm;
    [SerializeField] private GameObject playerCameraMount;
    [SerializeField] private GameObject holdPoint;
    [SerializeField] private GameObject playerCameraSwivel;
    private AudioSource playerAudioSource;
    private InventoryManager inventoryManager;

    private void Awake()
    {
          
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        inventoryManager = gameObject.GetComponent<InventoryManager>();
        gm.AssignPlayer(gameObject);
        playerAudioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        hideMouse(!(gm.GetEscapeOpen() || gm.GetInventoryOpen() || gm.GetIsInKeypad()));
        CheckForMenuOpen();

        //tempDropItem();
    }

    void tempDropItem()
    {
        if(Input.GetKeyDown(KeyCode.Q) && inventoryManager.CheckForOccSlot())
        {
            inventoryManager.Drop(inventoryManager.GetFirstOccupiedSlot());
        }
    }


    public GameObject GetCameraSwivel()
    {
        return playerCameraSwivel; 
    }

    public InventoryManager GetInventoryManager()
    {
        return inventoryManager;
    }

    public GameObject GetHoldPoint()
    {
        return holdPoint;
    }

    public GameObject GetPlayerCameraMount()
    {
        return playerCameraMount;
    }

    public AudioSource GetPlayerAudioSource()
    {
        return playerAudioSource;
    }

    private void CheckForMenuOpen()
    {
        if(Input.GetKeyDown(gm.kc_OpenInventory) && !gm.GetEscapeOpen() && !gm.GetSettingsOpen())
        {
            gm.SetInventoryVisibility(!gm.GetInventoryOpen());
        }


        if(Input.GetKeyDown(gm.kc_OpenMenu))
        {
            if (gm.GetInventoryOpen())
            {
                if(gm.GetInventoryMenuController().GetRPCVisibility())
                { 
                    gm.GetInventoryMenuController().SetRPCVisibility(false);
                }
                else
                {
                    gm.SetInventoryVisibility(false);
                }
                
            }
            else if (gm.GetEscapeOpen())
            {
                if(gm.GetSettingsOpen())
                {
                    gm.SetSettingsVisibility(false);
                }
                else
                {
                    gm.CloseEscapeMenu();
                }
            }
            else
            {
                gm.OpenEscapeMenu();
            }
        }
    }

    private void hideMouse(bool val)
    { 
        Cursor.lockState = val ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !val;
    }


}
