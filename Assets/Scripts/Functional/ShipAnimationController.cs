using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipAnimationController : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private float soundSpeedGate = 8f;
    private Rigidbody rb;
    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    [SerializeField] private float autoEndDuration = 5f;
    private float timeOfRun = 0;

    private bool wasPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponentInChildren<AudioSource>();
        timeOfRun = -autoEndDuration * 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic == true) { timeOfRun = Time.time; }
        if(Time.time - timeOfRun > autoEndDuration && rb.isKinematic == false)
        {
            rb.isKinematic = true;
        }

        if(gm.GetEscapeOpen())
        {
            if(audioSource.isPlaying)
            {
                audioSource.Pause();
                wasPlaying = true;
            }
        }
        else
        {
            if(wasPlaying)
            {
                audioSource.UnPause();
                wasPlaying = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collided with " + collision.gameObject.tag);
        if(rb.velocity.magnitude > soundSpeedGate)
        {

            gm.PlayAudioClip(audioSource, GameManager.audioType.SFX, clip, true, -25, 0.4f);


        }
    }

    public void run()
    {
        timeOfRun = Time.time;
        rb.isKinematic = false;
        rb.velocity = new Vector3(0, -15, 0);
        rb.angularVelocity = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
    }
    public void SetPosToIntendedSpot()
    {
        rb.isKinematic = false ;
        transform.localPosition = new Vector3(0, 14, 0);
        transform.rotation = Quaternion.identity;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
