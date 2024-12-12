using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogosSceneController : MonoBehaviour
{
    ScenesManager scenesManager;

    [SerializeField]
    SettingsMenu settingsMenu;

    void Start()
    {
        settingsMenu.LoadPreferences();

        scenesManager = ScenesManager.scenesManager;
        scenesManager.ChangeScene(EnumManager.scenes.MainScreenScene);            
    }

    
}
