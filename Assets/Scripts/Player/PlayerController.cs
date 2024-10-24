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

    private void Awake()
    {
          
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gm.AssignPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        hideMouse(!gm.getEscapeOpen());
        CheckForMenuOpen();
    }

    public GameObject getCameraSwivel()
    {
        return playerCameraSwivel; 
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
        if(Input.GetKeyDown(gm.kc_OpenMenu))
        {
            if(gm.getEscapeOpen())
            {
                if(gm.isSettingOpen())
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
