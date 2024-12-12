using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneController : MonoBehaviour
{
    #region variables
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    ScenesManager scenesManager;

    [SerializeField]
    float waitBeforeGoToMainScreen = 10.0f;
    #endregion

    void Start()
    {
        Assignments();
        
        StartCoroutine(GoToMainScreen());

        audioManager.Play(EnumManager.audios.win);
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;
        scenesManager = ScenesManager.scenesManager;
        audioManager = AudioManager.audioManager;
    }

    IEnumerator GoToMainScreen()
    {
        yield return new WaitForSeconds(waitBeforeGoToMainScreen);

        audioManager.Stop(EnumManager.audios.win);
        scenesManager.ChangeScene(EnumManager.scenes.MainScreenScene);
    }
}
