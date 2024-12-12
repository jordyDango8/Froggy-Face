using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    #region variables
    [SerializeField]
    internal static ScenesManager scenesManager;

    [SerializeField]
    GameObject fade;

    [SerializeField]
    float fadeDuration = 1.0f;

    [SerializeField]
    bool inGameplay = false;
    #endregion

    void Awake()
    {
        if(scenesManager == null)
        {
            scenesManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    internal bool GetInGameplay()
    {
        return inGameplay;
    }

    void SetInGameplay()
    {
        if(SceneManager.GetActiveScene().name.Equals(EnumManager.scenes.PinballScene.ToString())
         ||SceneManager.GetActiveScene().name.Equals(EnumManager.scenes.FaceBuilderScene.ToString()))
        {
            inGameplay = true;
        }
        else
        {
            inGameplay = false;
        }
    }

    internal void ChangeScene(EnumManager.scenes _newScene)
    {
        StartCoroutine(ChangeSceneCR(_newScene));
    }

    IEnumerator ChangeSceneCR(EnumManager.scenes _newScene)
    {
        FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(_newScene.ToString());
    }

    internal void FadeIn()
    {
        fade.GetComponent<Animator>().SetTrigger(EnumManager.animParameters.fadeIn.ToString());
        SetInGameplay();
    }

    internal void FadeOut()
    {
        fade.GetComponent<Animator>().SetTrigger(EnumManager.animParameters.fadeIn.ToString());
    }
}
