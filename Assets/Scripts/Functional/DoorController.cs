using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Animator lowerAnimator;
    [SerializeField] private Animator upperAnimator;
    private AudioSource doordioSource;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    [SerializeField] private bool defaultState = false;

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
            ToggleDoor(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoor(bool noSound = false)
    {
        if(state)
        {
            CloseDoor(noSound);
        }
        else
        {
            OpenDoor(noSound);
        }
    }

    public void OpenDoor(bool noSound = false)
    {
        lowerAnimator.Play("DoorLowerOpen", 0, 0.0f);
        upperAnimator.Play("DoorUpperOpen", 0, 0.0f);
        if (!noSound)
        {
            gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX, openSound, true);
        }
        
        state = true;
    }

    public void CloseDoor(bool noSound = false)
    {
        lowerAnimator.Play("DoorLowerClose", 0, 0.0f);
        upperAnimator.Play("DoorUpperClose", 0, 0.0f);
        if (!noSound)
        {
            gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX, closeSound, true);
        }
        
        state=false;
    }
}
