using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    private GameObject currentMount;

    private GameManager gm;

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
        UpdatePosition();
    }


    private void UpdatePosition()
    {
        if (currentMount != null)
        {
            gameObject.transform.position = currentMount.transform.position;
            gameObject.transform.rotation = currentMount.transform.rotation;
        }
    }



    public void SetNewMount(GameObject mount)
    {
        if (mount.GetComponent<CameraMount>() != false)
        {
            if (mount != currentMount)
            {
                if(currentMount != null)
                {
                    currentMount.GetComponent<CameraMount>().setActiveMount(false);
                }
                currentMount = mount;
                currentMount.GetComponent<CameraMount>().setActiveMount(true);
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
