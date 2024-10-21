using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour
{
    private bool isInUse = false;

    //[SerializeField] private float cameraSettings;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActiveMount(bool val)
    {
        isInUse = val;
    }
}
