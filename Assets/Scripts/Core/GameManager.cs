using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject settingsCanvas;
    private SettingsMenuController smc;


    [SerializeField] private string titleScene;
    [SerializeField] private string gameScene;
    [SerializeField] private string testScene;




    private int gamePaused = 1; // 1 = no, 0 = yes
    /* 
     * SETTINGS VARIBALES
     */
    //Controls
    private float mouseSens = 10f;

    public KeyCode kc_Forward = KeyCode.W;
    public KeyCode kc_Backward = KeyCode.S;
    public KeyCode kc_Left = KeyCode.A;
    public KeyCode kc_Right = KeyCode.D;

    public KeyCode kc_Crouch = KeyCode.LeftControl;
    public KeyCode kc_Sprint = KeyCode.LeftShift;

    public KeyCode kc_Interact = KeyCode.E;
    public KeyCode kc_OpenInventory = KeyCode.Tab;
    public KeyCode kc_AltTextView = KeyCode.LeftAlt;
    public KeyCode kc_DropItem = KeyCode.Q;
    public KeyCode kc_OpenMenu = KeyCode.Escape;
    public KeyCode kc_SkipAudio = KeyCode.Return;
    //Display
    private int targetFPS = 75;
    //Audio
    private float masterVol;
    private float sfxVol;
    private float musicVol;
    private float ambianceVol;
    private float dialogueVol;
    //Gameplay
    private bool autoAltText;



    private bool isSettingsOpen = false;



    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        smc = gameObject.GetComponentInChildren<SettingsMenuController>();
        settingsCanvas = smc.gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
       SetSettingsVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetSettingsVisibility(bool val)
    {
        settingsCanvas.SetActive(val);
        isSettingsOpen = val;
        smc.setCurrentDisplay(0);

        if (GameObject.FindGameObjectWithTag("TitleScreenEventManager") != null)
        {
            GameObject.FindGameObjectWithTag("TitleScreenEventManager").GetComponent<TitleScreenManager>().setMenuVisibility(true);
        }
    }

    public bool isSettingOpen()
    {
        return isSettingsOpen; 
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.LeftControl) == true && Input.GetKey(KeyCode.F6))
        {
            SceneManager.LoadScene(testScene);
        }
        else
        {
            SceneManager.LoadScene(gameScene);
        }

    }


    public void setMaxFPS(int val)
    {
        targetFPS = val;
    }    

    public void setMasterVol(int val)
    {
        masterVol = val;
    }
    public void setMusicVol(int val)
    {
        musicVol = val;
    }
    public void setSFXVol(int val)
    {
        sfxVol  = val;
    }
    public void setAmbiVol(int val)
    {
        ambianceVol = val;
    }
    public void setDialVol(int val)
    {
        dialogueVol = val;
    }
    public void setAutoAlt(bool val)
    {
        autoAltText = val;
    }

    public void setMouseSens(float val)
    {
        mouseSens = val; 
    }

    public KeyCode[] getActiveKeycodes()
    {
        return new KeyCode[] { kc_Forward, kc_Backward, kc_Left, kc_Right, kc_Crouch, kc_Sprint, kc_OpenInventory, kc_SkipAudio, kc_AltTextView, kc_DropItem, kc_Interact };
    }
}
