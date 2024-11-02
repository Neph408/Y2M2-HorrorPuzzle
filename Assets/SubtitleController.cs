using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    GameManager gm;
    private TextMeshProUGUI subtitleText;
    private string subtitleToShow = "";
    private string currentDisplaySubtitle = "";
    private int positionInQueue;
    private float typewriterStyleDelay = 0.5f;
    private float timeOfSubtitleEnd;
    private float autoHideDuration = 5f;
    private float timeOfLastCharacter;
    private bool isWriting = false;
    private Color defaultSubtitleDisplayColour = Color.magenta * 0.85f;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        subtitleText = GetComponentInChildren<TextMeshProUGUI>();
        subtitleText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTypewriterSubtitle();
    }


    private void UpdateTypewriterSubtitle()
    {

        if (!gm.GetEscapeOpen() && isWriting && positionInQueue < subtitleToShow.Length && Time.time - timeOfLastCharacter >= typewriterStyleDelay)
        {
            currentDisplaySubtitle += subtitleToShow[positionInQueue].ToString();
            positionInQueue++;
            timeOfLastCharacter = Time.time;
            subtitleText.text = currentDisplaySubtitle;
        }
        else if(positionInQueue == subtitleToShow.Length)
        {
            timeOfSubtitleEnd = Time.time;
            isWriting = false;
        }

        if(!isWriting && Time.time - timeOfSubtitleEnd > autoHideDuration)
        {
            HideSubtitles();
        }
        
    }

    private void HideSubtitles()
    {
        subtitleText.enabled = false;
    }

    public void SetCurrentSubtitleString(string subtitleString, float internalDelay = 0.5f, Color? subtitleColourOverride = null)
    {
        isWriting = true;
        subtitleText.enabled = true;
        Debug.Log("Set subs to " +  subtitleString);
        if (subtitleColourOverride != null)
        {
            subtitleText.color = (Color)subtitleColourOverride;
        }
        else
        {
            subtitleText.color = defaultSubtitleDisplayColour;
        }
        subtitleToShow = subtitleString;
        currentDisplaySubtitle = "";
        typewriterStyleDelay = internalDelay;
        positionInQueue = 0;
        timeOfLastCharacter = Time.time - internalDelay;
    }
}
