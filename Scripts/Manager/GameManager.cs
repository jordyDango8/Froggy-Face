using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region variables
    internal static GameManager gameManager;

    [SerializeField]
    SpritesManager spritesManager;

    ScenesManager scenesManager;
    
    [SerializeField]
    int facePartIndex = 0;               
    
    [SerializeField]
    bool pauseB = false;
    
    [SerializeField]
    int partsQuantity = 7;             
    
    [SerializeField]
     bool gameOver = false;
    #endregion
    
    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Assignments();
    }

    void Assignments()
    {
        scenesManager = ScenesManager.scenesManager;
        spritesManager = SpritesManager.spritesManager;        
    }

    internal void Pause()
    {
        pauseB = !pauseB;
        if(pauseB)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }      

    internal int GetPartsQuantity()
    {
        return partsQuantity;
    }

    internal bool GetPauseB()
    {
        return pauseB;
    }           

    internal void SetFacePartIndex(int _newValue)
    {
        facePartIndex = _newValue;
        //Debug.Log("facePartIndex= " + facePartIndex);
    }

    internal int GetFacePartIndex()
    {
        return facePartIndex;
    }

    internal void SetGameOver(bool _newState)
    {
        gameOver = _newState;
        //Debug.Log("gameover= " + gameOver);
    }

    internal bool GetGameOver()
    {
        return gameOver;
    }

    internal void Exit()
    {
        Debug.Log("exit");
        StartCoroutine(ExitCR());
    }

    IEnumerator ExitCR()
    {
        scenesManager.FadeOut();
        yield return new WaitForSeconds(1);

        Application.Quit();
    }

}
