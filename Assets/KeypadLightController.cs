using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class KeypadLightController : MonoBehaviour
{

    [SerializeField] private GameObject light_Left;
    [SerializeField] private GameObject light_Centre;
    [SerializeField] private GameObject light_Right;

    [SerializeField] private float lightIntensity = 9f;
    [SerializeField] private Color lightColourInactive = Color.white;
    [SerializeField] private Color lightColourFail = Color.red;
    [SerializeField] private Color lightColourWait = Color.yellow;
    [SerializeField] private Color lightColourSuccess = Color.green;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void SetInactiveLights()
    {
        light_Left.GetComponent<MeshRenderer>().material.color = lightColourInactive;
        light_Left.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourInactive * lightIntensity);
        light_Centre.GetComponent<MeshRenderer>().material.color = lightColourInactive;
        light_Centre.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourInactive * lightIntensity);
        light_Right.GetComponent<MeshRenderer>().material.color = lightColourInactive;
        light_Right.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourInactive * lightIntensity);
    }

    public void SetLightSuccess()
    {
        light_Left.GetComponent<MeshRenderer>().material.color = lightColourSuccess;
        light_Left.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourSuccess * lightIntensity);
        light_Centre.GetComponent<MeshRenderer>().material.color = lightColourSuccess;
        light_Centre.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourSuccess * lightIntensity);
        light_Right.GetComponent<MeshRenderer>().material.color = lightColourSuccess;
        light_Right.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourSuccess * lightIntensity);
    }
    
    public void SetLightFailure()
    {
        light_Left.GetComponent<MeshRenderer>().material.color = lightColourFail;
        light_Left.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourFail * lightIntensity);
        light_Centre.GetComponent<MeshRenderer>().material.color = lightColourFail;
        light_Centre.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourFail * lightIntensity);
        light_Right.GetComponent<MeshRenderer>().material.color = lightColourFail;
        light_Right.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourFail * lightIntensity);
    }
    public void SetLightWaiting()
    {
        light_Left.GetComponent<MeshRenderer>().material.color = lightColourWait;
        light_Left.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourWait * lightIntensity);
        light_Centre.GetComponent<MeshRenderer>().material.color = lightColourWait;
        light_Centre.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourWait * lightIntensity);
        light_Right.GetComponent<MeshRenderer>().material.color = lightColourWait;
        light_Right.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightColourWait * lightIntensity);
    }
}
