using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SettingsSliderControl : MonoBehaviour
{

    private enum sliderSettingsList
    {
        maxFPS, MasterVol, SFXVol, DialogueVol,AmbianceVol, MusicVol, MouseSens 
    }
    [SerializeField] private sliderSettingsList role;
    [SerializeField] private Slider slide;
    [SerializeField] private GameObject go_inpField;
    [SerializeField] private TextMeshProUGUI tmp;
    private TMP_InputField inputField;
    private GameManager gm;
    [SerializeField] private string SettingTitle;

    private SettingsMenuController smc;

    private int currentInputValue;

    // Start is called before the first frame update
    void Start()
    {
        inputField = go_inpField.GetComponent<TMP_InputField>();
        tmp.text = SettingTitle;
        gm = GameManager.Instance;
        smc = gm.GetComponentInChildren<SettingsMenuController>();
        valueUpdate(0);
    }

    // Update is called once per frame
    void Update()
    {
        slide.interactable = !smc.getButtonBeingRegistered();
        inputField.interactable = !smc.getButtonBeingRegistered();
    }

    public void valueUpdate(int val) // 0 = slider change, 1 = tb change
    {
        if (val == 0)
        {
            inputField.text = slide.value.ToString();
            currentInputValue = (int)slide.value;
        }
        else if (val == 1)
        {
            int textAsInt = int.Parse(inputField.text);
            if (textAsInt > slide.maxValue)
            {
                slide.value = slide.maxValue;
                inputField.text = slide.maxValue.ToString();
            }
            else if (textAsInt < slide.minValue)
            {
                slide.value = slide.minValue;
                inputField.text = slide.minValue.ToString();
            }
            else
            {
                slide.value = textAsInt;
            }
            currentInputValue = textAsInt;
        }
        setCurrentVal();
    }

    public void setCurrentVal()
    {
     switch (role)
        {
            case sliderSettingsList.maxFPS:
                gm.setMaxFPS(currentInputValue);
                break;
            case sliderSettingsList.MasterVol:
                gm.setMasterVol(currentInputValue);
                break;
            case sliderSettingsList.SFXVol:
                gm.setSFXVol(currentInputValue);
                break;
            case sliderSettingsList.AmbianceVol:
                gm.setAmbiVol(currentInputValue);
                break;
            case sliderSettingsList.MusicVol:
                gm.setMusicVol(currentInputValue);
                break;
            case sliderSettingsList.DialogueVol:
                gm.setDialVol(currentInputValue);
                break;
            case sliderSettingsList.MouseSens:
                gm.setMouseSens(currentInputValue);
                break;

        }
    }
}
