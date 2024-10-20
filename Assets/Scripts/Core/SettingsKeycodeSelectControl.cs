using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsKeycodeSelectControl : MonoBehaviour
{
    private enum keycodeSettingsList
    {
        kc_Forward,
        kc_Backward,
        kc_Left,
        kc_Right,
        kc_Crouch,
        kc_Sprint,
        kc_Interact,
        kc_OpenInventory,
        kc_AltTextView,
        kc_DropItem,
        kc_OpenMenu,
        kc_SkipAudio
    }
    [SerializeField] private GameObject go_Button;
    private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private string title;
    [SerializeField] private keycodeSettingsList role;
    GameManager gm;

    private SettingsMenuController smc;


    private bool isAwaitingInput = false;
    private string awaitingInputStr = "[Press Key]";



    private KeyCode currentValue;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        smc = gm.GetComponentInChildren<SettingsMenuController>();
        buttonText = go_Button.GetComponentInChildren<TextMeshProUGUI>();
        titleText.text = title;
        updateVisualState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAwaitingInput)
        {
            readForInput();
        }
        go_Button.GetComponent<Button>().interactable = !smc.getButtonBeingRegistered();

    }

    private void updateVisualState()
    {
        getVisualState();
        buttonText.text = currentValue.ToString().Replace("Alpha","").Replace("Keypad","Keypad ").Replace("Caps","Caps ").Replace("Scroll","Scroll ").Replace("Page","Page ").Replace("Left","Left ").Replace("Right","Right ").Replace("Up", "Up ").Replace("Down", "Down ");// glorious, glorious shittery
    }
    
    private void getVisualState()
    {
        currentValue = ReadWriteState();
    }

    public void registerCode()
    {
        buttonText.text = awaitingInputStr;
        isAwaitingInput = true;
        smc.setButtonBeingRegistered(true);
    }

    private void SetKeycode(KeyCode val)
    {
        ReadWriteState(val);
    }
    
    private void readForInput() // a little bit of heresy goes a long way, here be an unpleasantly nested if statement, suffer
    {
        if (Input.anyKeyDown)
        {
            foreach(KeyCode k in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(k))
                {
                    if (k != KeyCode.Escape && checkForDupes(k) == false)
                    {
                        ReadWriteState(k);
                    }
                    else
                    {
                        if (k != KeyCode.Escape) // cus checkfordupes is just more looping that i really dont want to be running, so its just checking its not the easy one
                        {
                            smc.DisplayAssignmentWarning(2f);
                        }
                        
                    }
                    updateVisualState();
                    smc.setButtonBeingRegistered(false);
                    isAwaitingInput= false;
                    break;
                }
            }
        }
    }



    private KeyCode ReadWriteState(KeyCode val = KeyCode.None, bool conf = false) // conf is just there to confirm if you mean to use none as an input, becase keycode cant take NULL as a value, which is honestly bullshit
    {
        switch (role)
        {
            case keycodeSettingsList.kc_Forward:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Forward = val; }
                return gm.kc_Forward;
            case keycodeSettingsList.kc_Backward:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Backward = val; }
                return gm.kc_Backward;
            case keycodeSettingsList.kc_Left:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Left = val; }
                return gm.kc_Left;
            case keycodeSettingsList.kc_Right:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Right = val; }
                return gm.kc_Right;
            case keycodeSettingsList.kc_Crouch:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Crouch = val; }
                return gm.kc_Crouch;
            case keycodeSettingsList.kc_Sprint:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Sprint = val; }
                return gm.kc_Sprint;
            case keycodeSettingsList.kc_Interact:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_Interact = val; }
                return gm.kc_Interact;
            case keycodeSettingsList.kc_OpenInventory:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_OpenInventory = val; }
                return gm.kc_OpenInventory;
            case keycodeSettingsList.kc_AltTextView:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_AltTextView = val; }
                return gm.kc_AltTextView;
            case keycodeSettingsList.kc_DropItem:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_DropItem = val; }
                return gm.kc_DropItem;
            case keycodeSettingsList.kc_OpenMenu:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_OpenMenu = val; }
                return gm.kc_OpenMenu;
            case keycodeSettingsList.kc_SkipAudio:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { gm.kc_SkipAudio = val; }
                return gm.kc_SkipAudio;
            default:
                if (val != KeyCode.None || (val == KeyCode.None && conf)) { Debug.LogWarning("Something has gone wrong with keycode assignment, figure this out, this shouldnt appear if everything is correctly assigned"); }
                return KeyCode.None;
        }
    }

    private bool checkForDupes(KeyCode val)
    {
        if (gm.getActiveKeycodes().Contains(val))
        { return true; }
        else
        { return false; }
    }
}
