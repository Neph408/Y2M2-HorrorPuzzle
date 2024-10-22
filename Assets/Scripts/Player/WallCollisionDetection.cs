using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCollisionDetection : MonoBehaviour
{
    private Vector3 directionOfCollision = Vector3.zero;
    private bool b_isActiveCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {

            directionOfCollision = (other.ClosestPoint(gameObject.transform.position) - gameObject.transform.position);

            b_isActiveCollision = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        b_isActiveCollision = false ;
        directionOfCollision = Vector3.zero;
    }

    public bool isActiveCollision()
    {
        return b_isActiveCollision;
    }


    public Vector3 getCollisionVector()
    { 
        return directionOfCollision;
    }
}
