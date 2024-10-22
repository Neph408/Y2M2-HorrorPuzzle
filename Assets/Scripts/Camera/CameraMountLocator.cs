using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMountLocator : MonoBehaviour
{
    [SerializeField] private GameObject cameraMount;

    private void Start()
    {
        if(cameraMount == null)
        {
            if(GetComponent<CameraMount>() == null)
            {
                if(GetComponentInChildren<CameraMount>() == null)
                {
                    Debug.LogWarning(gameObject.name + " CML could not locate a camera mount on the game object or in its hierarchy, check the CameraMount Script is present somehwere within the objkect hierarchy or manually assign the camera");
                }
                else
                {
                    cameraMount = GetComponentInChildren<CameraMount>().gameObject;
                }
            }
            else
            {
                cameraMount = gameObject;
            }
        }
    }
    public GameObject getMount()
    {
        return cameraMount; 
    }

}
