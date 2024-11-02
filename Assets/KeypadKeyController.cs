using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKeyController : MonoBehaviour
{
    private KeypadKeyData[] keys;
    private string[] keyStrings = new string[12];
    private KeypadController kc;
    [SerializeField,Tooltip("Will not override C and #")] private bool useDefaultKeyStrings = true;
    [SerializeField,Tooltip("Ignore any advice, will however not override function")] private bool overrideOverridenKeyValues = false;
    [SerializeField,Tooltip("Will not override C and #")] private string[] customKeyStrings = new string[12];


    // Start is called before the first frame update
    void Start()
    {
        keyStrings = new string[12]{ "1","2","3","4","5","6","7","8","9","C","0","#"};
        keys = GetComponentsInChildren<KeypadKeyData>();
        kc = GetComponentInParent<KeypadController>();

        if(overrideOverridenKeyValues)
        {
            customKeyStrings[9] = "C";
            customKeyStrings[11] = "#";
        }
        

        foreach (KeypadKeyData key in keys)
        {
            key.GetText().text = useDefaultKeyStrings ? keyStrings[key.GetKeyArrayPosition()] : customKeyStrings[key.GetKeyArrayPosition()];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] GetActiveString()
    {
        return useDefaultKeyStrings ? keyStrings : customKeyStrings;
    }

    public bool GetIsActive()
    {
        return kc.GetIsActive();
    }
}
