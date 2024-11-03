using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private UnityEvent onPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressButton()
    {
        onPress.Invoke();
    }
}
