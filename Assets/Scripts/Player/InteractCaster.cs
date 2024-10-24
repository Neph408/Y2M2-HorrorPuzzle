using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InteractCaster : MonoBehaviour
{
    private GameManager gm;
    private PlayerController pc;
    private GameObject camSwivel;

    private Holdable targetHoldable;

    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private float raycastDistance;

    [SerializeField] private float pickupHoldTime = 0.75f;
    private float currentHoldTime = 0f;
    private bool isKeyDown = false;

    private GameObject currentRaycastHit;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        pc = gameObject.GetComponent<PlayerController>();
        camSwivel = pc.getCameraSwivel();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(camSwivel.transform.position, camSwivel.transform.forward,out hit, raycastDistance, raycastMask);

        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Holdable"))
            {

                getVariableObjectInput(hit);
            }
            /*
            else if (hit.collider.CompareTag("Keypad"))
            {

            }
            else if (hit.collider.CompareTag("Button"))
            {

            }
            */
        }
        else
        {
            if (currentRaycastHit != null)
            {
                currentRaycastHit.GetComponent<Holdable>().Glow(false, Color.blue);
            }
            currentRaycastHit = null;
            currentHoldTime = 0f;
        }


        if (targetHoldable != null)
        {
            targetHoldable.Hold();
            targetHoldable.Glow(true, Color.green);
        }

        if (Input.GetKeyUp(gm.kc_Interact) && targetHoldable != null)
        {
            targetHoldable.Drop();
            targetHoldable=null;
        }

        

        
    }

    void getVariableObjectInput(RaycastHit hit) /// peak jank
    {
        if(currentRaycastHit != null)
        {
            if (currentRaycastHit != hit.collider.gameObject)
            {
                currentRaycastHit.GetComponent<Holdable>().Glow(false, Color.blue);
                currentHoldTime = 0f;
            }
            hit.collider.gameObject.GetComponent<Holdable>().Glow(true, Color.blue);
        }
        

        

        if (Input.GetKeyDown(gm.kc_Interact))
        {
            isKeyDown = true;
        }
        else if( Input.GetKey(gm.kc_Interact) && isKeyDown)
        {
            currentHoldTime += Time.deltaTime;
            if(currentHoldTime > pickupHoldTime)
            {
                targetHoldable = hit.collider.GetComponent<Holdable>();
                targetHoldable.Pickup();
                isKeyDown = false;
            }
        }
        if(Input.GetKeyUp(gm.kc_Interact))
        { 
            isKeyDown = false;
            
            if(currentHoldTime < pickupHoldTime)
            {
                if(checkForPickup(hit.collider.gameObject))
                {
                    pickupObject(hit.collider.gameObject);
                }
            }

            currentHoldTime = 0f;
        }
        currentRaycastHit = hit.collider.gameObject;
    }



    bool checkForPickup(GameObject go)
    {
        if (go.GetComponent<ObjectPickup>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void pickupObject(GameObject go)
    {
        pc.GetPlayerInventory().AddToInventory(go);
    }    
}
