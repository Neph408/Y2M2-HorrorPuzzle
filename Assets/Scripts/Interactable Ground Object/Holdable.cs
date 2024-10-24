using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holdable : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private Outline ol;

    private Vector3 prevvec;

    private Vector3 pauseVelocity;
    private Vector3 pauseAngVelocity;

    private const float speedLimit = 35f;





    
    [SerializeField] private LayerMask maskAll;
    [SerializeField] private LayerMask maskNone;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        ol = GetComponent<Outline>();
        ol.OutlineMode = Outline.Mode.SilhouetteOnly;

    }

    // Update is called once per frame
    void Update()
    {
        CapSpeed();
        CheckForPause();
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = new Vector3(Random.Range(0,10), Random.Range(0, 10), Random.Range(0, 10));
            transform.rotation = Quaternion.identity;
        }
    }


    private void CheckForPause()
    { 
        if(rb.isKinematic != gameManager.GetEscapeOpen())
        {
            if(!rb.isKinematic)
            {
                pauseAngVelocity = rb.angularVelocity;
                pauseVelocity = rb.velocity;
                rb.isKinematic = gameManager.GetEscapeOpen();
            }
            else
            {
                rb.isKinematic = gameManager.GetEscapeOpen();
                rb.angularVelocity = pauseAngVelocity;
                rb.velocity = pauseVelocity;
                pauseAngVelocity = Vector3.zero;
                pauseVelocity = Vector3.zero;
            }
            
        }
        

    }

    public void Glow(bool val, Color col)
    {
        ol.OutlineMode = (val) ? Outline.Mode.OutlineAll : Outline.Mode.SilhouetteOnly;
        ol.OutlineColor = col;
    }


    public void Pickup()
    {
        rb.useGravity = false;
    }

    public void Drop()
    {
        rb.useGravity = true;   
    }


    public void Hold()
    {
        

        rb.excludeLayers = (Input.GetKey(gameManager.kc_Interact) && !IsApproxInRange(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position, 5f)) ? maskAll : maskNone;

        if (Input.GetKey(gameManager.kc_Interact))
        {

            rb.velocity = (gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position - gameObject.transform.position) * 5f;

            prevvec = (gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position - gameObject.transform.position).normalized;
        }


        //CapSpeed();

        rb.angularVelocity *= 0.99f; // rbs are in fixedUpdate so this shouldnt cause a massive amount of issues 
                                    // famous last words
    }




    bool IsApproxInRange(Vector3 v1, Vector3 v2, float dis)
    {
        if(Mathf.Abs(v1.x - v2.x) < dis && Mathf.Abs(v1.y - v2.y) < dis && Mathf.Abs(v1.z - v2.z) < dis)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CapSpeed()
    {
        if (CheckIfExceedingSpeedCap())
        {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }


    bool CheckIfExceedingSpeedCap()
    {
        //Debug.Log("Current Velocity for Object " + gameObject.name + " : " + rb.velocity.magnitude.ToString());
        if (Mathf.Abs(rb.velocity.magnitude) > speedLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    }
