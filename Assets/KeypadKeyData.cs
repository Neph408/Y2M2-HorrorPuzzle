using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadKeyData : MonoBehaviour
{
    public enum e_KeyFunction { K_1, K_2, K_3, K_4, K_5, K_6, K_7, K_8, K_9, K_Clear, K_0, K_Hash }

    [SerializeField] private e_KeyFunction KeyFunction;

    [SerializeField] private TextMeshPro textbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public e_KeyFunction GetKeyFunction()
    {
        return KeyFunction;
    }

    public int GetKeyArrayPosition()
    {
        return (int)KeyFunction;
    }

    public TextMeshPro GetText()
    {
        return textbox; 
    }
}
