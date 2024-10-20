using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBoolControl : MonoBehaviour
{
    private enum boolSettingsList
    { autoAltText}
    [SerializeField] private GameObject go_Button;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private string title;
    [SerializeField] private boolSettingsList role;
    GameManager gm;

    private bool currentState = false;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        titleText.text = title;
        updateVisualState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleState()
    {
        currentState = !currentState;
        updateVisualState();
        updateSettings();
    }

    private void updateVisualState()
    {
        go_Button.GetComponentInChildren<TextMeshProUGUI>().text = currentState.ToString();
    }

    void updateSettings()
    {
        switch (role)
        {
            case boolSettingsList.autoAltText:
                gm.setAutoAlt(currentState);
                break;
        }
    }
}
