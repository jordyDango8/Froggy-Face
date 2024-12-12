using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScreenSceneController : MonoBehaviour
{
    #region variables
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    ScenesManager scenesManager;

    [SerializeField]
    CameraManager cameraManager;

    [SerializeField]
    MenusManager menusManager;
    
    [SerializeField]
    TextMeshProUGUI version;
    #endregion

    void Start()
    {          
        Assignments();  
        Initialize();        
        SetVersion();
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;  
        audioManager = AudioManager.audioManager;
        scenesManager = ScenesManager.scenesManager; 
        cameraManager = CameraManager.cameraManager;
        menusManager = MenusManager.menusManager;
    }

    void Initialize()
    {
        scenesManager.FadeIn();
        
        audioManager.Play(EnumManager.audios.mainMenu);
        
        cameraManager.InitializeCamera(GetComponent<Camera>());
    }

    void SetVersion()
    {
        version.text = "version " + Application.version;
    }

    public void StartGame()
    {
        audioManager.Stop(EnumManager.audios.mainMenu);
        scenesManager.ChangeScene(EnumManager.scenes.PinballScene);
    }

    public void Options()
    {
        menusManager.Options();
    }

    public void GoToPrivacyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/froggy-face-privacy-policy/p%C3%A1gina-principal");
    }

    public void Exit()
    {
        gameManager.Exit();
    }    
}
