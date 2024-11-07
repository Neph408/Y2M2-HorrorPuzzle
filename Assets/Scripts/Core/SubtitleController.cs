using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    GameManager gm;
    private TextMeshProUGUI subtitleText;
    [SerializeField] private GameObject subContainer;
    private string subtitleToShow = "";
    private string currentDisplaySubtitle = "";
    private int positionInQueue;
    private float typewriterStyleDelay = 0.5f;
    private float timeOfSubtitleEnd;
    [SerializeField] private float lineBreakDelay = 1f;
    [SerializeField] private float autoHideDuration = 5f;
    private float timeOfLastCharacter;
    private bool isWriting = false;
    private Color defaultSubtitleDisplayColour = Color.magenta * 0.85f;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        subtitleText = GetComponentInChildren<TextMeshProUGUI>();
        subContainer.SetActive(false);
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
            if (subtitleToShow[positionInQueue].ToString() == "\\" && positionInQueue + 1 < subtitleToShow.Length - 1)
            {
                if(subtitleToShow[positionInQueue+1].ToString() == "n")
                { 
                    currentDisplaySubtitle = "";
                    timeOfLastCharacter = Time.time + lineBreakDelay;
                    positionInQueue += 2;
                }
                else
                {
                    currentDisplaySubtitle += subtitleToShow[positionInQueue].ToString();
                    positionInQueue++;
                    timeOfLastCharacter = Time.time;
                    subtitleText.text = currentDisplaySubtitle;
                }
            }
            else
            {
                currentDisplaySubtitle += subtitleToShow[positionInQueue].ToString();
                positionInQueue++;
                timeOfLastCharacter = Time.time;
                subtitleText.text = currentDisplaySubtitle;
            }
        }
        else if(positionInQueue == subtitleToShow.Length)
        {
            timeOfSubtitleEnd = Time.time;
            isWriting = false;
            positionInQueue++;
        }

        if(!isWriting && Time.time - timeOfSubtitleEnd > autoHideDuration)
        {
            HideSubtitles();
        }
        
    }

    private void HideSubtitles()
    {
        subContainer.SetActive(false);
    }

    public void SetCurrentSubtitleString(string subtitleString, float internalDelay = 0.5f, Color? subtitleColourOverride = null, bool isTypewriterStyle = true, float breakDelay = 1f)
    {
        if(isTypewriterStyle)
        {
            isWriting = true;
            subContainer.SetActive(true);
            Debug.Log("Set subs to [[" + subtitleString + "]] with typewriter enabled");
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
            lineBreakDelay = breakDelay;
            timeOfLastCharacter = Time.time - internalDelay;
        }
        else
        {
            subContainer.SetActive(true);
            Debug.Log("Set subs to " + subtitleString + " with typewriter disabled");
            if (subtitleColourOverride != null)
            {
                subtitleText.color = (Color)subtitleColourOverride;
            }
            else
            {
                subtitleText.color = defaultSubtitleDisplayColour;
            }
            subtitleText.text = subtitleString;
            timeOfSubtitleEnd = Time.time;

            //leaving this stuff here for "safety"
            currentDisplaySubtitle = "";
            lineBreakDelay = breakDelay;
            typewriterStyleDelay = internalDelay;
            positionInQueue = 0;
            
        }

    }
}
