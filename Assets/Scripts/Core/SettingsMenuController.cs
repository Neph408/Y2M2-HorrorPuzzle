using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject go_DisplayObjectGroup;
    [SerializeField] private GameObject go_AudioObjectGroup;
    [SerializeField] private GameObject go_ControlsObjectGroup;
    [SerializeField] private GameObject go_GameplayObjectGroup;

    [SerializeField] private GameObject go_keyAssignWarning;


    GameManager gm;

    private bool inputBeingRegistered = false;


    private int currentDisplay = 0;

    // Start is called before the first frame update
    void Awake()
    {
        currentDisplay = 0;
    }

    void Start()
    {
        gm = GameManager.Instance;
        updateGroupVisibility();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCurrentDisplay(int val)
    {
        currentDisplay = val;
        updateGroupVisibility();
    }


    private void updateGroupVisibility()
    {
        switch (currentDisplay)
        {
            case 0:
                go_DisplayObjectGroup.SetActive(true);
                go_AudioObjectGroup.SetActive(false);
                go_ControlsObjectGroup.SetActive(false);
                go_GameplayObjectGroup.SetActive(false);
                break;
            case 1:
                go_DisplayObjectGroup.SetActive(false);
                go_AudioObjectGroup.SetActive(true);
                go_ControlsObjectGroup.SetActive(false);
                go_GameplayObjectGroup.SetActive(false);
                break;
            case 2:
                go_DisplayObjectGroup.SetActive(false);
                go_AudioObjectGroup.SetActive(false);
                go_ControlsObjectGroup.SetActive(true);
                go_GameplayObjectGroup.SetActive(false);
                break;
            case 3:
                go_DisplayObjectGroup.SetActive(false);
                go_AudioObjectGroup.SetActive(false);
                go_ControlsObjectGroup.SetActive(false);
                go_GameplayObjectGroup.SetActive(true);
                break;
        }
    }

    public void setButtonBeingRegistered(bool val)
    {
        inputBeingRegistered = val;
    }
    public bool getButtonBeingRegistered()
    {
        return inputBeingRegistered;
    }


    public void DisplayAssignmentWarning(float val = 5f)
    {
        go_keyAssignWarning.GetComponent<warningController>().DisplayWarning(val);
    }
}
