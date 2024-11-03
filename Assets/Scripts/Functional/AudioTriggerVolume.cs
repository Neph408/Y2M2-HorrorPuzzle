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

    [Header("Subtitles on Entry")]
    [Space(5)]
    [SerializeField] private bool entryUsesSubtitle = false;
    [SerializeField] private string entrySubtitleString = "";
    [SerializeField] private float entrySubtitleTickDelay = 0.05f;
    [SerializeField] private Color entrySubtitleColour = Color.magenta;
    [SerializeField] private bool entryUsesTypewriterStyle = true;

    [Header("Audio on Exit")]
    [Space(5)]
    [SerializeField] private bool useExitAudio = true;
    [SerializeField] private AudioSource targetExitAudioSource;
    [SerializeField] private AudioClip targetExitAudioFile;
    [SerializeField] private GameManager.audioType exitAudioCategory;
    [SerializeField, Tooltip("Dont use random pitch for dialogue")] private bool shouldExitUseRandomPitch;
    [SerializeField, Range(-128, 128)] private int exitPriorityBoost;

    [Header("Subtitles on Exit")]
    [Space(5)]
    [SerializeField] private bool exitUsesSubtitle = false;
    [SerializeField] private string exitSubtitleString = "";
    [SerializeField] private float exitSubtitleTickDelay = 0.05f;
    [SerializeField] private Color exitSubtitleColour = Color.magenta;
    [SerializeField] private bool exitUsesTypewriterStyle = true;

    [Header("Additional Triggers")]
    [Space(5)]
    [SerializeField] private UnityEvent additionalOnEnterEventTriggers;
    [SerializeField] private UnityEvent additionalOnExitEventTriggers;

    

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
            if(useEntryAudio)
            {
                gm.PlayAudioClip(targetEntryAudioSource, entryAudioCategory, targetEntryAudioFile, shouldEntryUseRandomPitch, entryPriorityBoost);
            }
            
            additionalOnEnterEventTriggers.Invoke();
            if(entryUsesSubtitle)
            {
                gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(entrySubtitleString,entrySubtitleTickDelay,entrySubtitleColour,entryUsesTypewriterStyle);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useExitAudio)
            {
                gm.PlayAudioClip(targetExitAudioSource, exitAudioCategory, targetExitAudioFile, shouldExitUseRandomPitch, exitPriorityBoost);
            }

            additionalOnExitEventTriggers.Invoke();

            if (exitUsesSubtitle)
            {
                gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(exitSubtitleString, exitSubtitleTickDelay, exitSubtitleColour,exitUsesTypewriterStyle);
            }
        }
    }
}
