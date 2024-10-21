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
            currentMount = mount;
        }
        else
        {
            Debug.LogWarning("Camera SNM pointing to non-camera mount, not moving");
        }
    }

    public GameObject GetMount()
    {
        return currentMount;
    }
}
