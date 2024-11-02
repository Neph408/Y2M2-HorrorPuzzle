using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AudioTriggerVolume : MonoBehaviour
{

    GameManager gm;

    [SerializeField] private bool displayHologramOfColliderInPlay = false;
    [SerializeField] private Color displayColor = new Color(0f,16f,64f,0.1f);

    [SerializeField] private bool useAudio = true;
    [SerializeField] private AudioSource targetAudioSource;
    [SerializeField] private AudioClip targetAudioFile;

    [SerializeField] private bool useSubtitle = false;
    [SerializeField] private string subtitleString = "";
    [SerializeField] private float subtitleTickDelay = 0.05f;

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
            if(useAudio)
            {
                targetAudioSource.clip = targetAudioFile;
                targetAudioSource.Play();
            }
            
            additionalOnEnterEventTriggers.Invoke();
            if(useSubtitle)
            {
                gm.GetHUDController().GetSubtitleController().SetCurrentSubtitleString(subtitleString,subtitleTickDelay,Color.yellow);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            additionalOnExitEventTriggers.Invoke();
        }
    }
}
