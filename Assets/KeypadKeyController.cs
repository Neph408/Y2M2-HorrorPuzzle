using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKeyController : MonoBehaviour
{
    private KeypadKeyData[] keys;
    private string[] keyStrings = new string[12];

    // Start is called before the first frame update
    void Start()
    {
        keyStrings = new string[12]{ "1","2","3","4","5","6","7","8","9","C","0","#"};
        keys = GetComponentsInChildren<KeypadKeyData>();

        foreach (KeypadKeyData key in keys)
        {
            key.GetText().text = keyStrings[key.GetKeyArrayPosition()];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
