using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class temp : MonoBehaviour
{
    [SerializeField] private DoorController dc;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Material>().color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("open");
            dc.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("clise");
            dc.CloseDoor();
        }
    }
}
