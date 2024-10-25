using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField] private int slotIndex;

    private Image spriteImage;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        spriteImage = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetSlotIndex()
    {
        return slotIndex; 
    }

    public void setText(string val)
    {
        text.text = val;
    }

    public void setSprite(Sprite val)
    {
        spriteImage.sprite = val;
    }
}
