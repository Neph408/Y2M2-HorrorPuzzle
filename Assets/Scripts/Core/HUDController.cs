using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    private GameObject TooltipOverlay;
    private TooltipController ttc;
    private SubtitleController stc;
    [SerializeField] private TextMeshProUGUI keypadKeybindReminder;

    // Start is called before the first frame update
    void Start()
    {
        ttc = GetComponentInChildren<TooltipController>();
        stc = GetComponentInChildren<SubtitleController>();
        TooltipOverlay = ttc.gameObject;
        TooltipOverlay.SetActive(false);
        SetKeypadKeybindReminderVisibility(false);
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


    public void SetKeypadKeybindReminderVisibility(bool val)
    {
        keypadKeybindReminder.gameObject.SetActive(val);
        keypadKeybindReminder.text = "Press [" + GameManager.Instance.kc_Interact.ToString() + "] to exit";
    }

    public SubtitleController GetSubtitleController()
    {
        return stc;
    }
}
