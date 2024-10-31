using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator lowerAnimator;
    [SerializeField] private Animator upperAnimator;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenDoor()
    {
        lowerAnimator.Play("DoorLowerOpen", 0, 0.0f);
        upperAnimator.Play("DoorUpperOpen", 0, 0.0f);
    }

    public void CloseDoor()
    {
        lowerAnimator.Play("DoorLowerClose", 0, 0.0f);
        upperAnimator.Play("DoorUpperClose", 0, 0.0f);
    }
}
