using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holdable : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private Vector3 prevvec;

    private Vector3 pauseVelocity;
    private Vector3 pauseAngVelocity;




    [SerializeField] private LayerMask maskAll;
    [SerializeField] private LayerMask maskNone;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        checkForPause();
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = new Vector3(Random.Range(0,10), Random.Range(0, 10), Random.Range(0, 10));
            transform.rotation = Quaternion.identity;
        }
    }


    private void checkForPause()
    { 
        if(rb.isKinematic != gameManager.getEscapeOpen())
        {
            if(!rb.isKinematic)
            {
                pauseAngVelocity = rb.angularVelocity;
                pauseVelocity = rb.velocity;
                rb.isKinematic = gameManager.getEscapeOpen();
            }
            else
            {
                rb.isKinematic = gameManager.getEscapeOpen();
                rb.angularVelocity = pauseAngVelocity;
                rb.velocity = pauseVelocity;
                pauseAngVelocity = Vector3.zero;
                pauseVelocity = Vector3.zero;
            }
            
        }
        

    }

    public void pickup()
    {
        rb.useGravity = false;
    }

    public void drop()
    {
        rb.useGravity = true;   
    }


    public void hold()
    {
        

        rb.excludeLayers = (Input.GetKey(gameManager.kc_Interact) && !isApproxInRange(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().getHoldPoint().transform.position, 5f)) ? maskAll : maskNone;

        if (Input.GetKey(gameManager.kc_Interact))
        {

            rb.velocity = (gameManager.GetPlayer().GetComponent<PlayerController>().getHoldPoint().transform.position - gameObject.transform.position) * 5f;

            prevvec = (gameManager.GetPlayer().GetComponent<PlayerController>().getHoldPoint().transform.position - gameObject.transform.position).normalized;
        }


        if (speedCap())
        {
            rb.velocity = rb.velocity.normalized * 100;
        }

        rb.angularVelocity *= 0.99f;
    }




    bool isApproxInRange(Vector3 v1, Vector3 v2, float dis)
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
    bool speedCap() // oh yeah peak jank
    {
        if (Mathf.Abs(rb.velocity.magnitude) > 100f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    }
