using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        gm.AssignLevelManager(this);
        if(Camera.main.GetComponent<CameraController>().GetMount() == null)
        {
            Camera.main.GetComponent<CameraController>().SetNewMount(gm.GetPlayer().GetComponent<PlayerController>().GetPlayerCameraMount());
        }
        gm.SetHUDVisibility(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
