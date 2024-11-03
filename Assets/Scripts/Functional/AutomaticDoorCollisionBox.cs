using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutomaticDoorCollisionBox : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    [SerializeField] private bool displayHologramOfColliderInPlay = false;
    [SerializeField] private Color displayColor = new Color(0f, 16f, 64f, 0.1f);
    // Start is called before the first frame update
    void Start()
    {
        if (displayHologramOfColliderInPlay)
        {
            GetComponent<Renderer>().material.color = displayColor;//tf is this, why is rgb 0-255, but a is 0-1
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorController.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.CloseDoor();
        }
    }
}
