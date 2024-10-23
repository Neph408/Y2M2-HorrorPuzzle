using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldableTestScript : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    private Vector3 prevvec;
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
        rb.useGravity = !Input.GetKey(gameManager.kc_Interact);

        rb.excludeLayers = (Input.GetKey(gameManager.kc_Interact) && !isApproxInRange(transform.position, gameManager.GetPlayer().GetComponent<PlayerController>().ghp().transform.position, 5f)) ? maskAll : maskNone;

        if (Input.GetKey(gameManager.kc_Interact))
        {

            rb.velocity = (gameManager.GetPlayer().GetComponent<PlayerController>().ghp().transform.position - gameObject.transform.position) * 5f;

            prevvec = (gameManager.GetPlayer().GetComponent<PlayerController>().ghp().transform.position - gameObject.transform.position).normalized;
        }
        

        if(speedCap())
        {
            rb.velocity = rb.velocity.normalized * 100;
        }
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
