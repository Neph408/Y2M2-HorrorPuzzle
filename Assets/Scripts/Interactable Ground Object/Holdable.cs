using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Holdable : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private OutlineOnHover oloh;

    private Vector3 prevvec;

    private Vector3 pauseVelocity;
    private Vector3 pauseAngVelocity;

    private const float speedLimit = 35f;


    private bool movingToHoldPoint = false;

    [SerializeField] private string objectName = "[DefaultName]";
    [SerializeField] private bool usePickupObjectDataDisplayName = true;
    [SerializeField] private string interactInfo = "[Press]";
    [SerializeField] private bool useDefaultInteractionInfo = true;

    
    private LayerMask maskAll = ~0; 
    private LayerMask maskNone = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        oloh = GetComponent<OutlineOnHover>();
        /*ol.OutlineMode = Outline.Mode.SilhouetteOnly;*/

        if (gameObject.GetComponent<PickupObjectData>() != null && usePickupObjectDataDisplayName)
        {
            objectName = gameObject.GetComponent<PickupObjectData>().GetDisplayName();
        }

        if(gameObject.GetComponent<PickupObjectData>() != null)
        {
            interactInfo = "Press";
        }
        else
        {
            interactInfo = "Hold";
        }

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

        ForceRespawn();
    }

    private void ForceRespawn()
    {
        if(transform.position.y < -100f)
        {
            transform.position = new Vector3(0, 10, 0);
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
    /*
    public void Glow(bool val, Color col)
    {
        ol.OutlineMode = (val) ? Outline.Mode.OutlineAll : Outline.Mode.SilhouetteOnly;
        ol.OutlineColor = col;
    }
    */

    public string GetInteractionInfo()
    {
        return interactInfo;
    }

    public string GetObjectName()
    {
        return objectName;
    }

    public void Grab()
    {
        rb.useGravity = false;
    }

    public void Drop()
    {
        rb.useGravity = true;
        oloh.Glow(false, Color.blue);
    }


    public void Hold()
    {
        
        if (GetDistance(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position) > gameManager.GetHoldableCollisionDisableDistance())
        {
            movingToHoldPoint = true;
        }
        if (GetDistance(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position) < 2.5f)
        {
            movingToHoldPoint = false;
        }


        //rb.excludeLayers = (Input.GetKey(gameManager.kc_Interact) && !IsApproxInRange(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().GetHoldPoint().transform.position, gameManager.GetHoldableCollisionDisableDistance())) ? maskAll : maskNone;
        if (Input.GetKey(gameManager.kc_Interact) && movingToHoldPoint)
        {
            rb.excludeLayers = maskAll;
            oloh.Glow(true, Color.red);
        }
        else
        {
            rb.excludeLayers = maskNone;
            oloh.Glow(true, oloh.GetCurrentColour());
        }


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
        //if(Mathf.Abs(v1.x - v2.x) < dis && Mathf.Abs(v1.y - v2.y) < dis && Mathf.Abs(v1.z - v2.z) < dis)
        if(Mathf.Abs(new Vector3(v2.x - v1.x, v2.y - v1.y, v2.z - v1.z).magnitude) < dis)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float GetDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Abs(Vector3.Distance(v2, v1));
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
