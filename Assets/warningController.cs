using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class warningController : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    private float hideTime = 5f;
    private float timeOfCall = -100000;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        textDisplay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeOfCall > hideTime )
        {
            textDisplay.enabled = false ;
        }
    }


    public void DisplayWarning(float val = 5f)
    {
        textDisplay.enabled = true ;
        timeOfCall = Time.time;
        hideTime = val;
    }
}
