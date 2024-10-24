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
    private PlayerInventory playerInventory;

    private void Awake()
    {
          
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gm.AssignPlayer(gameObject);
        playerInventory = gameObject.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        hideMouse(!gm.GetEscapeOpen());
        CheckForMenuOpen();
    }

    public GameObject getCameraSwivel()
    {
        return playerCameraSwivel; 
    }

    public PlayerInventory GetPlayerInventory()
    {
        return playerInventory;
    }

    public GameObject getHoldPoint()
    {
        return holdPoint;
    }

    public GameObject getPlayerCameraMount()
    {
        return playerCameraMount;
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
                gm.SetInventoryVisibility(false);
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
