using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject settingsCanvas;
    private GameObject escapeCanvas;
    private SettingsMenuController smc;
    private EscapeMenuController emc;

    private LevelManager currentLevelManager;

    private GameObject playerObject;

    [SerializeField] private string titleScene;
    [SerializeField] private string gameScene;
    [SerializeField] private string testScene;

    private GameObject[] inventoryBackup;


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
    private bool isEscapeOpen = false;



    
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
            Debug.Log("*poof*");
        }
        smc = gameObject.GetComponentInChildren<SettingsMenuController>();
        emc = gameObject.GetComponentInChildren<EscapeMenuController>();
        settingsCanvas = smc.gameObject;
        escapeCanvas = emc.gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
       SetSettingsVisibility(false);
        SetEscapeMenuVisibility(false);
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

        if (val && SceneManager.GetActiveScene().name != "MainMenu")
        {
            CloseEscapeMenu(true);
        }
        else
        {
            OpenEscapeMenu(true);
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

    public void backupInventory(GameObject[] goa)
    {
        inventoryBackup = goa;
    }

    public GameObject[] pullInventory()
    {
        return inventoryBackup; 
    }


    public void OpenEscapeMenu(bool isTemporaryHide = false)
    {
        SetEscapeMenuVisibility(true, isTemporaryHide);
        gamePaused = 0;
    }

    public void CloseEscapeMenu(bool isTemporaryHide = false)
    {
        SetEscapeMenuVisibility(false, isTemporaryHide);
        gamePaused = 1;
    }

    private void SetEscapeMenuVisibility(bool val, bool isTemporaryHide = false)// the actually, and actually, actually method, works 40% of the time, 70% of the time
    {
        escapeCanvas.SetActive(val);
        if (!isTemporaryHide)
        {
            isEscapeOpen = val;
        }
        
    }

    public bool getEscapeOpen()
        { return isEscapeOpen; }





    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "TestScene")
        {
            ClearLevelManager();
        }
    }


    public void AssignPlayer(GameObject go)
    {
        if (playerObject == null)
        {
            playerObject = go;
        }
        else
        {
            Debug.LogWarning("Player already assigned, this is likely bad");
        }
    }

    public GameObject GetPlayer() 
    {
        return playerObject; 
    }

    public void AssignLevelManager(LevelManager lm)
    {
        currentLevelManager = lm;
    }

    public LevelManager getLevelManager()
    {
        return currentLevelManager;
    }

    private void ClearLevelManager()
    {
        currentLevelManager = null;
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

    public float getMouseSens() 
    {
        return mouseSens; 
    }
}
