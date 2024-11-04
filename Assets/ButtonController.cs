using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private UnityEvent onPress;
    private AudioSource buttonAudioSource;
    [SerializeField] private AudioClip buttonAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        buttonAudioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressButton()
    {
        onPress.Invoke();
        gameManager.PlayAudioClip(buttonAudioSource, GameManager.audioType.SFX, buttonAudioClip, true, volumeScalar:0.5f);
    }
}
