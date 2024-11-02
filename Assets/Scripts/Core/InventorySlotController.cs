using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    [SerializeField] private int slotIndex;
    [SerializeField] private InventoryMenuController imc;
    [SerializeField] private GameManager gm;

    private Image spriteImage;
    private TextMeshProUGUI text;

    [SerializeField] private Image overlayImage;

    [SerializeField] private AudioClip audio_Hover;
    [SerializeField] private AudioClip audio_Click;
    // Start is called before the first frame update
    void Start()
    {
        spriteImage = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        gm = GameManager.Instance;
        imc = gm.GetInventoryMenuController();
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

    public void Hover()
    {
        //Debug.Log("hover over inv slot " + slotIndex.ToString());
        gm.PlayAudioClip(gm.GetPlayerAudioSource(), audio_Hover, true);
        imc.SetCurrentHover(slotIndex);
    }

    public void UnHover()
    {
        //Debug.Log("no longer over inv slot " + slotIndex.ToString());
        imc.SetCurrentHover(-1);
    }

    public void Select()
    {
        //Debug.Log("click on inv slot " + slotIndex.ToString());
        imc.SetLastSelected(slotIndex);
        gm.PlayAudioClip(gm.GetPlayerAudioSource(), audio_Click, true);
    }

    public void SetOverlayImageTransparency(float val)
    {
        overlayImage.color = new Color(192, 192, 192, val);
    }
}
