using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    private GameManager gm;
    private bool IsActive = false;

    [SerializeField] private string code = "0000";

    private int codeLength = 0;

    private CameraMountLocator m_Locator;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        m_Locator = GetComponent<CameraMountLocator>();

        codeLength = code.Length;

    }

    // Update is called once per frame
    void Update()
    {
        SetActive();


    }



    private void SetActive()
    {

        IsActive = m_Locator.getMount() == Camera.main.GetComponent<CameraController>().GetMount();
    }


    public void SetCameraToObject()
    {
        Camera.main.GetComponent<CameraController>().SetNewMount(m_Locator.getMount());
    }
}