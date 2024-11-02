using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    private GameObject currentMount;

    private GameManager gm;


    private const float targetTime = 0.75f;
    private Vector3 TransitionVector;
    private Vector3 TransitionQuaternion;
    private bool IsMoving = false;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementTime = 0.02f;

    private float remainingTime = 0;



    private bool SmoothMovement = false;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.GetEscapeOpen())
        {
            if (IsMoving)
            {
                SmoothMove();
            }
            else
            {
                UpdatePosition();
            }
        }
        
    }


    private void UpdatePosition()
    {
        if (currentMount != null)
        {

                gameObject.transform.position = currentMount.transform.position;
                gameObject.transform.rotation = currentMount.transform.rotation;        
        }
    }


    private void SmoothMove()
    {
        if (FuzzyPositionCheck(gameObject.transform.position, currentMount.transform.position, 0.0025f) || remainingTime <= 0f)
        {
            IsMoving = false;
            return;
        }

        Vector3 currentVelocity = Vector3.zero;
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, currentMount.transform.position, ref currentVelocity, movementTime);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, currentMount.transform.rotation, rotationSpeed * Time.deltaTime);
        remainingTime -= Time.deltaTime;
    }

    private bool FuzzyPositionCheck(Vector3 v1, Vector3 v2, float leniency)
    {
        return (Mathf.Abs((v1 - v2).magnitude) < leniency);
    }

    private void SetSmoothMovementParameters(bool val)
    {
        IsMoving = val;
        remainingTime = targetTime;
    }

    public bool GetIsMoving()
    {
        return IsMoving; 
    }

    public void SetNewMount(GameObject mount, bool IsSmooth = false)
    {
        SmoothMovement = IsSmooth;
        if (mount.GetComponent<CameraMount>() != false)
        {
            if (mount != currentMount)
            {
                /*
                if(currentMount != null)
                {
                    //currentMount.GetComponent<CameraMount>().setActiveMount(false);
                }
                */

                currentMount = mount;
                SetSmoothMovementParameters(IsSmooth);
                //currentMount.GetComponent<CameraMount>().setActiveMount(true);
            }
            else
            {
                Debug.LogWarning("Camera mount set to currently active mount, you are likely finding the wrong mount");
            }
        }
        else
        {
            Debug.LogWarning("CameraController.SetNewMount() pointing to non-camera mount, not moving");
        }
    }

    public GameObject GetMount()
    {
        return currentMount;
    }
}
