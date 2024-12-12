using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    internal static MenusManager menusManager;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    ScenesManager scenesManager;

    [SerializeField]
    GameObject optionsSubmenu;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject pauseSubMenu;

    void Start()
    {
        gameManager = GameManager.gameManager;
        scenesManager = ScenesManager.scenesManager;
    }

    void Awake()
    {
        if(menusManager == null)
        {
            menusManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    internal void Options()
    {
        optionsSubmenu.SetActive(true);
    }

    internal void InitializeMenu()
    {
        //Debug.Log("initialize menu");        
        PauseButtonOnOff(scenesManager.GetInGameplay());
    }

    internal void PauseButtonOnOff(bool _newState)
    {
        pauseButton.SetActive(_newState);                        
    }

    public void Pause()
    {
        if(scenesManager.GetInGameplay())
        {
            //Debug.Log("pause");
            gameManager.Pause();
            PauseSubMenuOnOff();                      
        }
    }

    internal void PauseSubMenuOnOff()
    {
        pauseSubMenu.SetActive(gameManager.GetPauseB());
    }
}
