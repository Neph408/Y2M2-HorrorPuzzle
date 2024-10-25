using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    private GameObject TooltipOverlay;
    private TooltipController ttc;
    // Start is called before the first frame update
    void Start()
    {
        ttc = GetComponentInChildren<TooltipController>();
        TooltipOverlay = ttc.gameObject;
        TooltipOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTooltip(bool val, KeyCode key = KeyCode.E, string intInfo = "[Default Text]", string objInfo = "[Object Name]")
    {
        TooltipOverlay.SetActive(val);
        if (val)
        {
            ttc.SetDisplayInfo(key, intInfo, objInfo);
        }

    }

}
