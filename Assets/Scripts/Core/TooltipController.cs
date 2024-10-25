using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyDisplay;
    [SerializeField] private TextMeshProUGUI interactionInfo;
    [SerializeField] private TextMeshProUGUI objectInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDisplayInfo(KeyCode key, string intInfo, string objInfo)
    {
        keyDisplay.text = key.ToString();
        interactionInfo.text = intInfo;
        objectInfo.text = objInfo;
    }
}
