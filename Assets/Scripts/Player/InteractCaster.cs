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

    private GameObject currentGlow;
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

    /*
     
     i regret putting the olog as some weird random thing to check for, it shouldve been merged with tooltip info and treated as its own thing
     fml
    but for now it works, so it stays a lost cause
    Update: did that, its sooo much better 

     */
 
    private void RunRaycast()
    {
        RaycastHit hit;

        Physics.Raycast(camSwivel.transform.position, camSwivel.transform.forward,out hit, raycastDistance, raycastMask);

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<OutlineAndTooltipOnHover>() != null)
            {
                if (currentGlow == null)
                {
                    currentGlow = hit.collider.gameObject;
                }
                if(currentGlow != hit.collider.gameObject)
                {
                    currentGlow.GetComponent<OutlineAndTooltipOnHover>().Glow(false);
                }
                currentGlow = hit.collider.gameObject;
                currentGlow.GetComponent<OutlineAndTooltipOnHover>().Glow(true);
            }


            //Debug.Log(hit.collider.tag);
            if (CheckForHoldable(hit.collider.gameObject))
            {
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
            
            else if (hit.collider.CompareTag("Button"))
            {
                ButtonInteract(hit);
            }
            
        }
        else
        {
            if (currentGlow != null)
            {
                currentGlow.GetComponent<OutlineAndTooltipOnHover>().Glow(false);
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
        if (Input.GetKeyDown(gm.kc_Interact))
        {
            currentRaycastHit.GetComponent<OutlineAndTooltipOnHover>().Glow(false);
            Camera.main.GetComponent<CameraController>().SetNewMount(currentRaycastHit.GetComponent<CameraMountLocator>().getMount(), true);
            gm.GetHUDController().SetKeypadKeybindReminderVisibility(true);
            gm.SetIsInKeypad(true);
        }
    }

    private void ButtonInteract(RaycastHit hit)
    {
        currentRaycastHit = hit.collider.gameObject;
        if(Input.GetKeyDown(gm.kc_Interact))
        {
            currentRaycastHit.GetComponent<ButtonController>().PressButton();
        }
    }


















    
    private void GetHoldableVariableObjectInput(RaycastHit hit) /// peak jank
    {
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
