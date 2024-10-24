using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharCont : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody rb;

    private Vector3 moveVelocity;

    private float lookX = 0;

    private bool isSprint = false;
    private bool isCrouch = false;


    [SerializeField] private GameObject playerCameraSwivel;

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float sprintMultiplier = 1.46666f;
    [SerializeField] private float crouchMultiplier = 0.55555f;
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
        if(!gm.GetEscapeOpen())
        {
            LookControl();
            MovementControl();
        }
        else
        {
            pauseMovement();
        }
        
    }

    private void pauseMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void LookControl()
    {
        gameObject.transform.Rotate(Vector3.up * gm.getMouseSens() * 0.2f * Input.GetAxis("Mouse X"));
        lookX += gm.getMouseSens() * -0.1f * Input.GetAxis("Mouse Y");
        lookX = Mathf.Clamp(lookX, -89.99f, 89.99f);
        playerCameraSwivel.transform.localRotation = Quaternion.Euler(lookX,0,0);

        
    }

    private void MovementControl()
    {
        moveVelocity = Vector3.zero;
        

        if(Input.GetKey(gm.kc_Forward))
        {
            moveVelocity += gameObject.transform.forward;
        }
        if (Input.GetKey(gm.kc_Backward))
        {
            moveVelocity += -gameObject.transform.forward;
        }
        if (Input.GetKey(gm.kc_Left))
        {
            moveVelocity += -gameObject.transform.right;
        }
        if (Input.GetKey(gm.kc_Right))
        {
            moveVelocity += gameObject.transform.right;
        }

        moveVelocity.Normalize();
        moveVelocity *= movementSpeed;




        ApplyForce();
    }


    private void ApplyForce()
    {

        if(Input.GetKey(gm.kc_Sprint))
        {
            isSprint = true;
            isCrouch = false;
            moveVelocity *= sprintMultiplier;
        }
        else if(Input.GetKey(gm.kc_Crouch))
        {
            isCrouch = true;
            isSprint = false;
            moveVelocity *= crouchMultiplier;
        }
        else
        {
            isSprint = false;
            isCrouch = false;
        }
        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;
        rb.angularVelocity = Vector3.zero;
    }
}
