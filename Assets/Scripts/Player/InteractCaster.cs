using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InteractCaster : MonoBehaviour
{
    /*
     
     need to move tooltips to being own component, not something hard set in this
     
     */
    
    
    
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

    private bool isHolding = false;

    private GameObject currentRaycastHit;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        pc = gameObject.GetComponent<PlayerController>();
        camSwivel = pc.GetCameraSwivel();
        inventoryManager = GetComponent<InventoryManager>();
        
    }

    void Update()
    {
        if(gm.GetPlayer().GetComponent<CameraMountLocator>().getMount() == Camera.main.GetComponent<CameraController>().GetMount())
        {
            RunRaycast();
        }
        else
        {
            if(Input.GetKeyDown(gm.kc_Interact))
            {
                Camera.main.GetComponent<CameraController>().SetNewMount(gm.GetPlayer().GetComponent<CameraMountLocator>().getMount(), true); // exit when interact again
                gm.GetHUDController().SetKeypadKeybindReminderVisibility(false);
                gm.SetIsInKeypad(false);
            }
        }
    }


 
    private void RunRaycast()
    {
        RaycastHit hit;

        Physics.Raycast(camSwivel.transform.position, camSwivel.transform.forward,out hit, raycastDistance, raycastMask);

        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.tag);
            if (CheckForHoldable(hit.collider.gameObject))
            {
                gm.GetHUDController().DisplayTooltip(true, gm.kc_Interact, hit.collider.GetComponent<Holdable>().GetInteractionInfo(), hit.collider.GetComponent<Holdable>().GetObjectName());
                if(!(gm.GetInventoryOpen() || gm.GetEscapeOpen()))
                {
                    GetHoldableVariableObjectInput(hit);
                }
                
            }
            else if (hit.collider.CompareTag("Keypad"))
            {
                if (hit.collider.GetComponent<KeypadController>().GetIsInteractable())
                {
                    KeypadInteraction(hit);
                }

            }
            /*
            else if (hit.collider.CompareTag("Button"))
            {

            }
            */
        }
        else
        {
            gm.GetHUDController().DisplayTooltip(false);
            if (currentRaycastHit != null)
            {
                currentRaycastHit.GetComponent<OutlineOnHover>().Glow(false, Color.blue);
            }
            currentRaycastHit = null;
            if(!isHolding)
            {
                currentHoldTime = 0f;
            }
            
        }


        if (targetHoldable != null)
        {
            targetHoldable.Hold();
            targetHoldable.GetComponent<OutlineOnHover>().Glow(true, Color.green);
        }

        if (Input.GetKeyUp(gm.kc_Interact) && targetHoldable != null)
        {
            targetHoldable.Drop();
            isHolding = false;
            targetHoldable=null;
        }

        

        
    }

    private void KeypadInteraction(RaycastHit hit)
    {
        currentRaycastHit = hit.collider.gameObject;
        currentRaycastHit.GetComponent<OutlineOnHover>().Glow(true, Color.yellow);
        gm.GetHUDController().DisplayTooltip(true, gm.kc_Interact, "Press to interact", "Keypad");
        if (Input.GetKeyDown(gm.kc_Interact))
        {
            gm.GetHUDController().DisplayTooltip(false);
            Camera.main.GetComponent<CameraController>().SetNewMount(currentRaycastHit.GetComponent<CameraMountLocator>().getMount(), true);
            gm.GetHUDController().SetKeypadKeybindReminderVisibility(true);
            gm.SetIsInKeypad(true);
        }
    }




















    
    private void GetHoldableVariableObjectInput(RaycastHit hit) /// peak jank
    {
        if(currentRaycastHit != null)
        {
            if (currentRaycastHit != hit.collider.gameObject)
            {
                currentRaycastHit.GetComponent<OutlineOnHover>().Glow(false, Color.blue);
                currentHoldTime = 0f;
            }
            hit.collider.gameObject.GetComponent<OutlineOnHover>().Glow(true, Color.blue);
        }
        

        

        if (Input.GetKeyDown(gm.kc_Interact))
        {
            isKeyDown = true;
        }

        else if( Input.GetKey(gm.kc_Interact) && isKeyDown && CheckForHoldable(hit.collider.gameObject))
        {
            currentHoldTime += Time.deltaTime;
            if(currentHoldTime > pickupHoldTime)
            {
                targetHoldable = hit.collider.GetComponent<Holdable>();
                targetHoldable.Grab();
                isHolding = true;
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

    bool CheckForHoldable(GameObject go)
    {
        if (go.GetComponent<Holdable>() != null)
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

    public float getInteractDistance()
    {
        return raycastDistance;
    }
}
