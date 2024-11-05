using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioTrigger : MonoBehaviour
{
    private GameManager gm;
    [Header("Audio")]
    [Space(5)]
    [SerializeField] private AudioSource targetEntryAudioSource = null;
    [SerializeField] private AudioClip targetEntryAudioFile = null;
    [SerializeField] private GameManager.audioType entryAudioCategory;
    [SerializeField, Tooltip("Dont use random pitch for dialogue")] private bool shouldEntryUseRandomPitch;
    [SerializeField, Range(-128, 128)] private int entryPriorityBoost;
    [SerializeField] private bool entryAudioIsSingleTimeUse;

    [Header("Subtitles (Subtitles will not play without accompying audio)")]
    [Space(5)]
    [SerializeField] private bool entryUsesSubtitle = false;
    [SerializeField] private string entrySubtitleString = "";
    [SerializeField] private float entrySubtitleTickDelay = 0.05f;
    [SerializeField] private Color entrySubtitleColour = Color.magenta;
    [SerializeField] private bool entryUsesTypewriterStyle = true;
    [SerializeField] private float entrySubLineBreakDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {

            gm.PlayAudioClip(targetEntryAudioSource, entryAudioCategory, targetEntryAudioFile, shouldEntryUseRandomPitch, entryPriorityBoost);

            if (entryUsesSubtitle)
            {
                gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(entrySubtitleString, entrySubtitleTickDelay, entrySubtitleColour, entryUsesTypewriterStyle, entrySubLineBreakDelay);
            }
    
    }
}