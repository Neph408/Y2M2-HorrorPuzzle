using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    
    private GameManager gm;
    [SerializeField] private GameObject menuCavas;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenSettingsScreen()
    {
        gm.SetSettingsVisibility(true);
        setMenuVisibility(false);
    }

    public void StartGame()
    {
        gm.StartGame();
    }

    public void CloseGame() 
    {
        gm.CloseGame();
    }

    public void setMenuVisibility(bool val)
    {
        menuCavas.SetActive(val);
    }
}
