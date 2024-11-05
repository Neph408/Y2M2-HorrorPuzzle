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

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.tag);
        //if(rb.velocity.magnitude > soundSpeedGate)
        //{

            gm.PlayAudioClip(audioSource, GameManager.audioType.SFX, clip, true, -25, 0.8f);


            //}
    }

    public void run()
    {
        rb.isKinematic = false;
        rb.velocity = new Vector3(0, -15, 0);
        rb.angularVelocity = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
    }
}
