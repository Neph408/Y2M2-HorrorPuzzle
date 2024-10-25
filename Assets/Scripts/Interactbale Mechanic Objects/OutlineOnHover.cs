using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    private Outline ol;
    // Start is called before the first frame update
    void Start()
    {
        ol = GetComponent<Outline>();
        ol.OutlineMode = Outline.Mode.OutlineVisible;
        ol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Glow(bool val, Color col)
    {
        ol.enabled = val;
        //ol.OutlineWidth = val ? 2 : 0;
        //ol.OutlineMode = (val) ? Outline.Mode.OutlineAll : Outline.Mode.OutlineHidden;
        ol.OutlineColor = col;
    }

    public Color GetCurrentColour()
    {

    return ol.OutlineColor; 
}
}
