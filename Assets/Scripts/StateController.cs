using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public GameObject MenuUI;
    public Camera menuCam;
    public Camera playerCam;

    enum GameMode
    {
        MainMenu,
        Gameplay
    }

    GameMode gameMode = GameMode.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameMode)
        {
            case GameMode.MainMenu:
                UpdateMainMenu();
                break;
            case GameMode.Gameplay:
                //UpdateGameplay();
                break;
        }
    }

    void UpdateMainMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGameplay();
        }
    }

    void StartMainMenu()
    {
        gameMode                        = GameMode.MainMenu;
        MenuUI.gameObject.SetActive(true);
        playerCam.enabled = false;
        menuCam.enabled = true;
    }

    void StartGameplay()
    {
        gameMode                        = GameMode.Gameplay;
        MenuUI.gameObject.SetActive(false);
        playerCam.enabled = true;
        menuCam.enabled = false;
    }

}
