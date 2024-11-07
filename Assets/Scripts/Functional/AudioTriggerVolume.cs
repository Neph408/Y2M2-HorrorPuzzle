using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AudioTriggerVolume : MonoBehaviour
{

    GameManager gm;

    [Header("Display in play")]
    [Space(5)]
    [SerializeField] private bool displayHologramOfColliderInPlay = false;
    [SerializeField] private Color displayColor = new Color(0f,16f,64f,0.1f);

    [Header("Additional Tags for Trigger Detection")]
    [Space(5)]
    [SerializeField] private string[] additionalEntryTriggerTags;
    [SerializeField] private string[] additionalExitTriggerTags;



    [Space(10)]
    [Header("Audio on Entry")]
    [Space(5)]
    [SerializeField] private bool useEntryAudio = true;
    [SerializeField] private AudioSource targetEntryAudioSource;
    [SerializeField] private AudioClip targetEntryAudioFile;
    [SerializeField] private GameManager.audioType entryAudioCategory;
    [SerializeField,Tooltip("Dont use random pitch for dialogue")] private bool shouldEntryUseRandomPitch;
    [SerializeField,Range(-128,128)] private int entryPriorityBoost;
    [SerializeField] private bool entryAudioIsSingleTimeUse;

    [Header("Subtitles on Entry (Subtitles will not play without accompying audio)")]
    [Space(5)]
    [SerializeField] private bool entryUsesSubtitle = false;
    [SerializeField,TextArea,Tooltip("use \\n to start new line")] private string entrySubtitleString = "";
    [SerializeField] private float entrySubtitleTickDelay = 0.05f;
    [SerializeField] private Color entrySubtitleColour = Color.magenta;
    [SerializeField] private bool entryUsesTypewriterStyle = true;
    [SerializeField] private float entrySubLineBreakDelay = 1f;

    [Header("Trigger on audio finish")]
    [Space(5)]
    [SerializeField] private bool useTriggerOnEntryAudioFinish;
    [SerializeField] private UnityEvent additionalTriggerOnEntryAudioFinish;

    [Space(10)]

    [Header("Audio on Exit")]
    [Space(5)]
    [SerializeField] private bool useExitAudio = true;
    [SerializeField] private AudioSource targetExitAudioSource;
    [SerializeField] private AudioClip targetExitAudioFile;
    [SerializeField] private GameManager.audioType exitAudioCategory;
    [SerializeField, Tooltip("Dont use random pitch for dialogue")] private bool shouldExitUseRandomPitch;
    [SerializeField, Range(-128, 128)] private int exitPriorityBoost;
    [SerializeField] private bool exitAudioIsSingleTimeUse;

    [Header("Subtitles on Exit (Subtitles will not play without accompying audio)")]
    [Space(5)]
    [SerializeField] private bool exitUsesSubtitle = false;
    [SerializeField, TextArea, Tooltip("use \\n to start new line")] private string exitSubtitleString = "";
    [SerializeField] private float exitSubtitleTickDelay = 0.05f;
    [SerializeField] private Color exitSubtitleColour = Color.magenta;
    [SerializeField] private bool exitUsesTypewriterStyle = true;
    [SerializeField] private float exitSubLineBreakDelay = 1f;

    [Header("Trigger on audio finish")]
    [Space(5)]
    [SerializeField] private bool useTriggerOnExitAudioFinish;
    [SerializeField] private UnityEvent additionalTriggerOnExitAudioFinish;

    [Space(10)]

    [Header("Additional Triggers")]
    [Space(2)]
    [Header("Instant Triggers")]
    [Space(5)]
    [SerializeField] private bool entryAdditionalTriggersAreSingleTimeUse;
    [SerializeField] private UnityEvent additionalOnEnterEventTriggers;
    [SerializeField] private bool exitAdditionalTriggersAreSingleTimeUse;
    [SerializeField] private UnityEvent additionalOnExitEventTriggers;
    [Space(5)]
    [Header("Time delayed triggers")]
    [Space(5)]
    [SerializeField] private bool delayedEntryTriggersAreSingleTimeUse;
    [SerializeField] private float delayedEntryTriggerDelayDuration = 2f;
    [SerializeField] private UnityEvent delayedEntryTriggerEvents;
    [Space(2)]
    [SerializeField] private bool delayedExitTriggersAreSingleTimeUse;
    [SerializeField] private float delayedExitTriggerDelayDuration = 2f;
    [SerializeField] private UnityEvent delayedExitTriggerEvents;


    private bool canUseEntAud = true;
    private bool canUseExtAud = true;
    private bool canUseEntAddTrig = true;
    private bool canUseExtAddTrig = true;
    private bool canUseEntDelayAddTrig = true;
    private bool canUseExtDelayAddTrig = true;

    private bool waitingForEntAudioFinish = false;
    private bool waitingForExtAudioFinish = false;

    private bool isWaitingForEntDelay = false;
    private float entDelayStartTimeStamp = 0f;
    
    private bool isWaitingForExtDelay = false;
    private float extDelayStartTimeStamp = 0f;

    private bool entWasPlaying = false;
    private bool extWasPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        if(displayHologramOfColliderInPlay)
        {
            GetComponent<Renderer>().material.color = displayColor;//tf is this, why is rgb 0-255, but a is 0-1
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (additionalEntryTriggerTags.Length == 0) { additionalEntryTriggerTags = new string[1] { "" }; }
        if (additionalExitTriggerTags.Length == 0) { additionalExitTriggerTags= new string[1] { "" }; }
    }
    private void Update()
    {
        if(waitingForEntAudioFinish)
        {
            if(targetEntryAudioSource.isPlaying == false)
            {
                waitingForEntAudioFinish = false;
                additionalTriggerOnEntryAudioFinish.Invoke();
            }
        }
        if(waitingForExtAudioFinish)
        {
            if(targetExitAudioSource.isPlaying == false)
            {
                waitingForExtAudioFinish = false;
                additionalTriggerOnExitAudioFinish.Invoke();
            }
        }

        if(isWaitingForEntDelay)
        {
            if(Time.time-entDelayStartTimeStamp > delayedEntryTriggerDelayDuration)
            {
                delayedEntryTriggerEvents.Invoke();
                isWaitingForEntDelay=false;
            }
        }

        if(isWaitingForExtDelay)
        {
            if(Time.time- extDelayStartTimeStamp > delayedExitTriggerDelayDuration)
            {
                delayedExitTriggerEvents.Invoke();
                isWaitingForExtDelay = false;
            }
        }

        if (gm.GetEscapeOpen())
        {
            if(targetEntryAudioSource != null)
            {
                if (targetEntryAudioSource.isPlaying)
                {
                    targetEntryAudioSource.Pause();
                    entWasPlaying = true;
                }
            }
            if (targetExitAudioSource != null)
            {
                if (targetExitAudioSource.isPlaying)
                {
                    targetExitAudioSource.Pause();
                    entWasPlaying = true;
                }
            }
        }
        else
        {
            if (entWasPlaying)
            {
                targetEntryAudioSource.UnPause();
                entWasPlaying = false;
            }
            if (extWasPlaying)
            {
                targetExitAudioSource.UnPause();
                entWasPlaying = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player") || additionalEntryTriggerTags.Contains(other.tag))
        {
            if(useEntryAudio && canUseEntAud)
            {
                if(useTriggerOnEntryAudioFinish)
                {
                    waitingForEntAudioFinish = true;
                }
                gm.PlayAudioClip(targetEntryAudioSource, entryAudioCategory, targetEntryAudioFile, shouldEntryUseRandomPitch, entryPriorityBoost);
                
                if (entryUsesSubtitle)
                {
                    gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(entrySubtitleString, entrySubtitleTickDelay, entrySubtitleColour, entryUsesTypewriterStyle,entrySubLineBreakDelay);
                }
                if (entryAudioIsSingleTimeUse)
                {
                    canUseEntAud = false;
                }
            }

            

            if (canUseEntAddTrig)
            {
                additionalOnEnterEventTriggers.Invoke();
                if(entryAdditionalTriggersAreSingleTimeUse)
                {
                    canUseEntAddTrig = false;
                }
            }
            
            if(canUseEntDelayAddTrig)
            {
                isWaitingForEntDelay = true;
                entDelayStartTimeStamp = Time.time;
                if(delayedEntryTriggersAreSingleTimeUse)
                {
                    canUseEntDelayAddTrig = false;
                }
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || additionalExitTriggerTags.Contains(other.tag))
        {
            if (useExitAudio && canUseExtAud)
            {
                if (useTriggerOnExitAudioFinish)
                {
                    waitingForExtAudioFinish = true;
                }
                gm.PlayAudioClip(targetExitAudioSource, exitAudioCategory, targetExitAudioFile, shouldExitUseRandomPitch, exitPriorityBoost);
                if (exitUsesSubtitle)
                {
                    gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(exitSubtitleString, exitSubtitleTickDelay, exitSubtitleColour, exitUsesTypewriterStyle,exitSubLineBreakDelay);
                }
                if(exitAudioIsSingleTimeUse)
                {
                    canUseExtAud = false;
                }
            }

            
            if (canUseExtAddTrig)
            {
                additionalOnExitEventTriggers.Invoke();
                if (exitAdditionalTriggersAreSingleTimeUse)
                {
                    canUseExtAddTrig = false;
                }
            }

            if (canUseExtDelayAddTrig)
            {
                isWaitingForExtDelay = true;
                extDelayStartTimeStamp = Time.time;
                if (delayedExitTriggersAreSingleTimeUse)
                {
                    canUseExtDelayAddTrig = false;
                }
            }
        }
    }
}
