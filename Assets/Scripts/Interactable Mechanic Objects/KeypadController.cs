using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    private GameManager gm;
    private bool IsActive = false;
    private bool IsInteractable = true;

    [SerializeField] private UnityEvent activateOnFailure;
    [SerializeField] private UnityEvent activateOnSuccess;

    private AudioSource keypadAudioSource;

    //[SerializeField] private bool useRandomCode;
    //[SerializeField] private int randomCodeCharLength = 5;
    [SerializeField] private string code = "0000";

    private int codeLength = 0;

    private string currentInput = "";

    private float timeOfLastFailure = -2f;

    private KeypadKeyController keyController;
    private KeypadLightController lightController;
    private KeypadDisplayController displayController;
    private CameraMountLocator m_Locator;

    [SerializeField] private LayerMask keypadInteractionLayerMask;

    [SerializeField] private AudioClip audio_KeyPress;
    [SerializeField] private AudioClip audio_MaxInput;
    [SerializeField] private AudioClip audio_Success;
    [SerializeField] private AudioClip audio_Failure;
    [SerializeField] private AudioClip audio_RemoveInput;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        keypadAudioSource = GetComponentInChildren<AudioSource>();
        m_Locator = GetComponent<CameraMountLocator>();
        keyController = GetComponentInChildren<KeypadKeyController>();
        lightController = GetComponentInChildren<KeypadLightController>();
        displayController = GetComponentInChildren<KeypadDisplayController>();
        codeLength = code.Length;

        lightController.SetInactiveLights();
        displayController.SetTextToCurrentSetString();
    }

    // Update is called once per frame
    void Update()
    {
        SetActive();

        if(IsActive && !Camera.main.GetComponent<CameraController>().GetIsMoving())
        {
            

            if(Time.time - timeOfLastFailure > 0.75f)
            {
                CheckForInput();
                lightController.SetLightWaiting();
            }
            else
            {
                lightController.SetLightFailure();
                displayController.SetTextToCurrentSetString();
            }
        }
        else
        {
            WipeInputString();
            displayController.SetInactiveText();
            if (IsInteractable) { lightController.SetInactiveLights(); }
            
        }
    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cast, out hit, Mathf.Infinity, keypadInteractionLayerMask))
            {
                if (hit.collider.CompareTag("KeypadKey"))
                {
                    KeyPress(hit.collider.GetComponent<KeypadKeyData>().GetKeyFunction(), keyController.GetActiveString());
                }
            }
        }

        /*
         * Ok so, explainy time
         * i couldbe done this better
         * so much better
         * but you knwo what
         * this was so much faster to do and it just works(tm), so for now irdgas
         * also i know it kinda breaks if i change the active string to anything not 1-0,
         * well i mnea, it will
         * it just wont be intuitive
         * also if a jumble the numbers on the keypad, this would be awful to use
         * all in all, solid 4/10, works as intended for function it was intended for
         */
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) { KeyPress(KeypadKeyData.e_KeyFunction.K_1, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) { KeyPress(KeypadKeyData.e_KeyFunction.K_2, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) { KeyPress(KeypadKeyData.e_KeyFunction.K_3, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) { KeyPress(KeypadKeyData.e_KeyFunction.K_4, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) { KeyPress(KeypadKeyData.e_KeyFunction.K_5, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) { KeyPress(KeypadKeyData.e_KeyFunction.K_6, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) { KeyPress(KeypadKeyData.e_KeyFunction.K_7, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) { KeyPress(KeypadKeyData.e_KeyFunction.K_8, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) { KeyPress(KeypadKeyData.e_KeyFunction.K_9, keyController.GetActiveString()); }
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)) { KeyPress(KeypadKeyData.e_KeyFunction.K_0, keyController.GetActiveString()); }

        if (Input.GetKeyDown(KeyCode.Backspace)) { DeleteLastChar(); }
        if (Input.GetKeyDown(KeyCode.Delete)) { WipeInputString(); }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) { CheckInputAgainstCode(); }
    }






    private void SetActive()
    {

        IsActive = m_Locator.getMount() == Camera.main.GetComponent<CameraController>().GetMount();
    }

    public bool GetIsActive()
    {
        return IsActive;
    }

    public void SetCameraToObject()
    {
        Camera.main.GetComponent<CameraController>().SetNewMount(m_Locator.getMount());
    }

    private void AddCharToInputString(string val)
    {
        if(currentInput.Length >= codeLength)
        {
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_MaxInput, true);
        }
        else
        {
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_KeyPress,true);
            currentInput += val;
            displayController.UpdateText(currentInput);
        }

    }


    private void DeleteLastChar()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            displayController.UpdateText(currentInput);
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_KeyPress, true);
        }
        else
        {
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_MaxInput, true);
        }
    }

    private void WipeInputString(bool playAudio = false)
    {
        currentInput = "";
        displayController.UpdateText(currentInput);
        if (playAudio)
        {
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_RemoveInput, true);
        }
        
    }

    private void CheckInputAgainstCode()
    {
        if (currentInput == code)
        {
            lightController.SetLightSuccess();
            displayController.SetSuccessText();
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_Success, true);
            IsInteractable = false;
            Camera.main.GetComponent<CameraController>().SetNewMount(gm.GetPlayer().GetComponent<CameraMountLocator>().getMount(), true); // exit when interact again
            gm.GetHUDController().SetKeypadKeybindReminderVisibility(false);
            gm.SetIsInKeypad(false);
            activateOnSuccess.Invoke();
        }
        else
        {
            currentInput = "";
            displayController.SetFailText();
            displayController.UpdateText(currentInput);
            lightController.SetLightFailure();
            gm.PlayAudioClip(keypadAudioSource, GameManager.audioType.SFX, audio_Failure, true);
            timeOfLastFailure = Time.time;
            activateOnFailure.Invoke();
        }
    }

    public bool GetIsInteractable()
    {
        return IsInteractable;
    }

    public void KeyPress(KeypadKeyData.e_KeyFunction keyFunction, string[] activeString)
    {
        //Debug.Log("Keypressed was key " + keyFunction.ToString() + ", which functioned as key " + activeString[(int)keyFunction]);
        if(keyFunction != KeypadKeyData.e_KeyFunction.K_Clear && keyFunction != KeypadKeyData.e_KeyFunction.K_Hash)
        {
            AddCharToInputString(activeString[(int)keyFunction]);
        }
        else
        {
            if(keyFunction == KeypadKeyData.e_KeyFunction.K_Clear)
            {
                if (Input.GetKey(KeyCode.LeftShift)) { DeleteLastChar();  }
                else { WipeInputString(true); }
            }
            else
            {
                CheckInputAgainstCode();
            }
        }
        
    }

    public string GetCode()
    {
        return code;
    }
}