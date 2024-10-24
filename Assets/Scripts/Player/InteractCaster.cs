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
                if(Input.GetKeyDown(gm.kc_Interact))
                {
                    targetHoldable = hit.collider.GetComponent<Holdable>();
                    targetHoldable.pickup();
                }
            }
        }
        if(Input.GetKeyUp(gm.kc_Interact) && targetHoldable != null)
        {
            targetHoldable.drop();
            targetHoldable=null;
        }

        if(targetHoldable != null)
        {
            targetHoldable.hold();
        }

        
    }




}
