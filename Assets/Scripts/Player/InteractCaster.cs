using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InteractCaster : MonoBehaviour
{
    private GameManager gm;
    private PlayerController pc;
    private GameObject camSwivel;
    private InventoryManager inventoryManager;

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
        camSwivel = pc.GetCameraSwivel();
        inventoryManager = GetComponent<InventoryManager>();
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

                GetVariableObjectInput(hit);
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

    void GetVariableObjectInput(RaycastHit hit) /// peak jank
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
                if(CheckForPickup(hit.collider.gameObject))
                {
                    PickupObject(hit.collider.gameObject);
                }
            }

            currentHoldTime = 0f;
        }
        currentRaycastHit = hit.collider.gameObject;
    }



    bool CheckForPickup(GameObject go)
    {
        if (go.GetComponent<PickupObjectData>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PickupObject(GameObject go)
    {
        if (pc.GetInventoryManager().CheckForSpace())
        {
            pc.GetInventoryManager().AddToInventory(go.GetComponent<PickupObjectData>().GetInventoryItem());
            Destroy(go);
        }
        else
        {
            //play sound
        }
    }    
}
