using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodePanelController : MonoBehaviour
{
    [SerializeField] private TextMeshPro textPanelText;
    [SerializeField] private KeypadController kpc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kpc.GetCode() != textPanelText.text)
        {
            textPanelText.text = kpc.GetCode();
        }
    }
}
