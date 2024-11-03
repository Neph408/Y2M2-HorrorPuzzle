using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadPanelController : MonoBehaviour
{
    private InventoryMenuController imc;

    private bool visibility = false;

    [SerializeField] private Image readImage;
    [SerializeField] private GameObject contrastPanel;
    [SerializeField] private TextMeshProUGUI altTextText;
    [SerializeField] private TextMeshProUGUI keybindText;

    //private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignIMC(InventoryMenuController inventoryMenuController)//, GameManager gameManager)
    {
        imc = inventoryMenuController;
        //gm = gameManager;
    }

    public void SetRPCDisplay(Sprite image, string AltText, bool autoDisplay, KeyCode altTextKeycode)
    { 
        readImage.sprite = image;
        altTextText.text = AltText;
        contrastPanel.SetActive(autoDisplay);
        keybindText.text = "[" + altTextKeycode.ToString() + "] to toggle alt text" ;
    }

    public void ToggleAltTextPanel()
    {
        contrastPanel.SetActive(!contrastPanel.activeSelf);
    }

    public void CloseScreen()
    {
        SetVisibility(false);
        imc.SetRPCVisibility(false);
    }

    public void SetVisibility(bool val)
    {
        visibility = val;
    }

    public bool GetVisibility()
        { return visibility; }
}
