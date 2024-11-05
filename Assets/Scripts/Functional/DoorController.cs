using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;

public class DoorController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Animator lowerAnimator;
    [SerializeField] private Animator upperAnimator;
    private AudioSource doordioSource;
    [SerializeField] private GameObject doorIndicator;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    [SerializeField] private bool defaultState = false;

    [SerializeField] private bool doorEnabled = true;

    private bool state = false;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        doordioSource = GetComponentInChildren<AudioSource>();
        if(defaultState != state)
        {
            ToggleDoor(true, true);
        }
        SetLightState(doorEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetEscapeOpen())
        {
            upperAnimator.speed = 0f; lowerAnimator.speed = 0f;
            doordioSource.Pause();
        }
        else
        {
            upperAnimator.speed = 1f; lowerAnimator.speed = 1f;
            doordioSource.UnPause();
        }
        SetLightState(doorEnabled);
    }

    private void SetLightState(bool val)
    {
        if(!val)
        {
            doorIndicator.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            doorIndicator.GetComponent<MeshRenderer>().material.color = Color.black;
            CloseDoor(true);
        }
        else
        {
            doorIndicator.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            doorIndicator.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    public void ToggleDoor(bool noSound = false, bool forceOverride = false)
    {
        if(state)
        {
            CloseDoor(noSound,forceOverride);
        }
        else
        {
            OpenDoor(noSound,forceOverride);
        }
    }

    private void OpenDoor(bool noSound = false, bool forceOverride = false)
    {
        if ((doorEnabled && !state) || forceOverride)
        {
            lowerAnimator.Play("DoorLowerOpen", 0, 0.0f);
            upperAnimator.Play("DoorUpperOpen", 0, 0.0f);
            if (!noSound)
            {
                gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX, openSound, true);
            }

            state = true;
        }
    }

    private void CloseDoor(bool noSound = false, bool forceOverride = false)
    {
        if ((doorEnabled && state) || forceOverride)
        { 
            lowerAnimator.Play("DoorLowerClose", 0, 0.0f);
            upperAnimator.Play("DoorUpperClose", 0, 0.0f);
            if (!noSound)
            {
                gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX, closeSound, true);
            }

            state = false;
        }
    }

    public void InteractableOpenDoor()
    {
        OpenDoor();
    }
    public void InteractableCloseDoor()
    {
        CloseDoor();
    }

    public void InteractableToggleDoor()
    {
        ToggleDoor();
    }

    public void SetDoorEnabled(bool val)
    {
        doorEnabled = val;
    }
}
