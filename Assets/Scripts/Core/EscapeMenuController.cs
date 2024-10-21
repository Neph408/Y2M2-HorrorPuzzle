using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        gm.CloseEscapeMenu();
    }

    public void OpenSettings()
    {
        gm.SetSettingsVisibility(true);
    }

    public void LoadMainMenu()
    {
        gm.CloseGame(); // fix later
    }

}
