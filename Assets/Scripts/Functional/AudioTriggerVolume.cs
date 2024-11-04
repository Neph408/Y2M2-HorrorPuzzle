using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private string entrySubtitleString = "";
    [SerializeField] private float entrySubtitleTickDelay = 0.05f;
    [SerializeField] private Color entrySubtitleColour = Color.magenta;
    [SerializeField] private bool entryUsesTypewriterStyle = true;
    [SerializeField] private float entrySubLineBreakDelay = 1f;

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
    [SerializeField] private string exitSubtitleString = "";
    [SerializeField] private float exitSubtitleTickDelay = 0.05f;
    [SerializeField] private Color exitSubtitleColour = Color.magenta;
    [SerializeField] private bool exitUsesTypewriterStyle = true;
    [SerializeField] private float exitSubLineBreakDelay = 1f;

    [Header("Additional Triggers")]
    [Space(5)]
    [SerializeField] private bool entryAdditionalTriggersAreSingleTimeUse;
    [SerializeField] private UnityEvent additionalOnEnterEventTriggers;
    [SerializeField] private bool exitAdditionalTriggersAreSingleTimeUse;
    [SerializeField] private UnityEvent additionalOnExitEventTriggers;

    private bool canUseEntAud = true;
    private bool canUseExtAud = true;
    private bool canUseEntAddTrig = true;
    private bool canUseExtAddTrig = true;


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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(useEntryAudio && canUseEntAud)
            {
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
            


            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useExitAudio && canUseExtAud)
            {
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

        }
    }
}
