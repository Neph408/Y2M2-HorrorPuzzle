using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharCont : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody rb;

    private Vector3 moveVelocity;

    [SerializeField] private GameObject playerCameraSwivel;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float sprintMultiplier = 1.46666f;
    //[SerializeField] private float ;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.getEscapeOpen())
        {
            MovementControl();
        }
        
    }





    private void MovementControl()
    {
        moveVelocity = Vector3.zero;
        
        if(Input.GetKey(gm.kc_Forward))
        {
            moveVelocity += gameObject.transform.forward * movementSpeed;
        }
        if (Input.GetKey(gm.kc_Backward))
        {
            moveVelocity += -gameObject.transform.forward * movementSpeed;
        }
        if (Input.GetKey(gm.kc_Left))
        {
            moveVelocity += -gameObject.transform.right * movementSpeed;
        }
        if (Input.GetKey(gm.kc_Right))
        {
            moveVelocity += gameObject.transform.right * movementSpeed;
        }







        ApplyForce();
    }


    private void ApplyForce()
    {
        if(Input.GetKey(gm.kc_Sprint))
        {
            moveVelocity *= sprintMultiplier;
        }
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }
}
