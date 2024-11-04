using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAndTooltipOnHover : MonoBehaviour
{
    private Outline ol;
    private GameManager gm;

    [Header("Tooltip Config")]
    [Space(5)]
    [SerializeField] private bool useTooltipOnHover = true;
    [SerializeField] private string objectName = "[DefaultName]";
    [SerializeField,Tooltip("If is a pickup object")] private bool usePickupObjectDataDisplayName = true;
    [SerializeField] private string interactInfo = "[Press]";
    [SerializeField] private bool useDefaultInteractionInfo = true;

    [Header("Outline Config")]
    [Space(5)]
    [SerializeField] private bool useOutlineOnHover = true;
    private const int outlineWidth = 4;
    [SerializeField,Tooltip("Default is 4")] private bool useCustomOutlineWidth = false;
    [SerializeField,Range(1,30)] private int customOutlineWidth = 4;

    [Header("Outline Colour Config")]
    [Space(5)]
    [SerializeField] private Color highlightColour = Color.yellow;
    [SerializeField,Tooltip("For when there is an alternate action on an object (E.g. Hold)")] private Color alternateColour = Color.green;

    private Color startupMainColour;
    private Color startupAltColour;

    // Start is called before the first frame update

    private void Awake()
    {
        ol = GetComponent<Outline>();
        ol.OutlineMode = Outline.Mode.OutlineVisible;
        ol.OutlineWidth = useCustomOutlineWidth ? customOutlineWidth : outlineWidth;
        ol.enabled = false;
        if(gameObject.GetComponent<PickupObjectData>() == null)
        {
            usePickupObjectDataDisplayName = false;
        }
        startupMainColour = highlightColour;
        startupAltColour = alternateColour;
    }
    void Start()
    {
        gm = GameManager.Instance;
        if (gameObject.GetComponent<PickupObjectData>() != null && usePickupObjectDataDisplayName)
        {
            objectName = gameObject.GetComponent<PickupObjectData>().GetDisplayName();
        }

        if (useDefaultInteractionInfo)
        {
            if (gameObject.GetComponent<PickupObjectData>() != null)
            {
                interactInfo = "Press";
            }
            else if (gameObject.GetComponent<Holdable>() != null)
            {
                interactInfo = "Hold";
            }
            else
            {
                interactInfo = "Press";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Glow(bool val, bool useAlternate = false)
    {
        if (useOutlineOnHover)
        {
            ol.enabled = val;
            ol.OutlineColor = useAlternate ? alternateColour : highlightColour;
        }

        if (useTooltipOnHover)
        {
            gm.GetHUDController().DisplayTooltip(val, gm.kc_Interact, interactInfo, objectName);
        }


    }

    public Color GetCurrentOutlineColour()
    {
        return ol.OutlineColor; 
    }

    public Color GetCurrentAltColour()
    {
        return alternateColour;
    } 
    public Color GetCurrentMainColour()
    {
        return highlightColour;
    }

    public void revertMainColour()
    {
        highlightColour = startupMainColour;
    }
    public void revertAltColour()
    {
        alternateColour = startupAltColour;
    }

    public void SetMainColour(Color newCol)
    {
        highlightColour = newCol;
    }
    public void SetAltColour(Color newCol)
    {
        alternateColour = newCol;
    }

}
