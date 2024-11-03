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

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        doordioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenDoor()
    {
        lowerAnimator.Play("DoorLowerOpen", 0, 0.0f);
        upperAnimator.Play("DoorUpperOpen", 0, 0.0f);
        gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX,openSound,true);
    }

    public void CloseDoor()
    {
        lowerAnimator.Play("DoorLowerClose", 0, 0.0f);
        upperAnimator.Play("DoorUpperClose", 0, 0.0f);
        gameManager.PlayAudioClip(doordioSource, GameManager.audioType.SFX , closeSound,true);
    }
}
