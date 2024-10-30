using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    private TextMeshProUGUI subtitleText;

    // Start is called before the first frame update
    void Start()
    {
        subtitleText = GetComponentInChildren<TextMeshProUGUI>();
        subtitleText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //subtitleText.text = subtitleText.text + "a";
    }
}
