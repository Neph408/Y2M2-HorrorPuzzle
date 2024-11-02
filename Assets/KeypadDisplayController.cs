using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadDisplayController : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText;
    private string displayString;
    private const string defaultDisplayText = "Enter code..";
    private const string failDisplayText = "Error..";
    private const string successDisplayText = "Code Accepted";

    // Start is called before the first frame update
    void Start()
    {
        displayString = defaultDisplayText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextToCurrentSetString()
    {
        displayText.text = displayString;
    }

    public void SetFailText()
    {
        displayString = failDisplayText;
        SetTextToCurrentSetString();
    }

    public void SetSuccessText()
    {
        displayString = successDisplayText;
        SetTextToCurrentSetString();
    }

    public void SetInactiveText()
    {
        displayString = defaultDisplayText;
        SetTextToCurrentSetString();
    }

    public void UpdateText(string display)
    {
        displayText.text = display;
    }
}
