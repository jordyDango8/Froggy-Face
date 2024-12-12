using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PinballSceneController : MonoBehaviour
{
    #region variables
    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    ScenesManager scenesManager;
    
    [SerializeField]
    CameraManager cameraManager;
    
    [SerializeField]
    AudioManager audioManager;
    
    [SerializeField]
    MenusManager menusManager;

    [SerializeField]
    Player player;

    [SerializeField]
    GameObject initialBouncersFather;
    
    [SerializeField]
    GameObject redirectButtons;
    
    [SerializeField]
    GameObject startButton;
    
    [SerializeField]
    TextMeshProUGUI redirectionCountTMP;

    //[SerializeField]
    int redirectMax = 3;
    
    [SerializeField]
    static int redirectCount = 0;

    [SerializeField]
    float waitAfterGotFacePart = 3f;

    [SerializeField]
    GameObject obstacles;
    #endregion

    void Start()
    {
        Assignments();
        Initialize();      
        //obstacles.SetActive(false); // for test  
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;        
        scenesManager = ScenesManager.scenesManager;
        cameraManager = CameraManager.cameraManager;
        menusManager = MenusManager.menusManager;
        audioManager = AudioManager.audioManager;
    }

    void Initialize()
    {
        scenesManager.FadeIn();

        cameraManager.InitializeCamera(GetComponent<Camera>());

        menusManager.InitializeMenu();

        if(gameManager.GetFacePartIndex() == 0)
        {
            audioManager.Play(EnumManager.audios.pinball);
            audioManager.Play(EnumManager.audios.waterfall);
        }

        UpdateRedirectionCount();

        RedirectButtonsOnOff(false);
    }
    
    void DisplayDownButton()
    {        
        RedirectButtonsOnOff(true);
        for(int i = 0; i < redirectButtons.transform.childCount-1; i++) // -1 because of text child 
        {
            redirectButtons.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
        redirectButtons.transform.GetChild(2).GetComponent<Image>().color = Color.white;
    } 

    public void Pause()
    {
        menusManager.Pause();        
    }

    public void Restart()
    {
        scenesManager.ChangeScene(EnumManager.scenes.PinballScene);
    }

    void Redirected()
    {
        ChangeRedirectionCount(1);
        UpdateRedirectionCount();
    }

    void UpdateRedirectionCount()
    {
        redirectionCountTMP.text = (redirectMax - redirectCount).ToString();        
    }

    internal void ChangeRedirectionCount(int amount)
    {
        redirectCount += amount;
        if(redirectCount == 1)
        {
            StartGamePressed();
            
        }
        if(redirectCount >= redirectMax)
        {
            RedirectButtonsOnOff(false);
        }
    }

    void StartGamePressed()
    {
        for(int i = 0; i < redirectButtons.transform.childCount-1; i++) // -1 because of text child 
        {
            redirectButtons.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        initialBouncersFather.SetActive(false);
        player.ApplyGravity();
    }

    void GotFacePart()
    {
        StartCoroutine(GotFacePartCR());
    }

    IEnumerator GotFacePartCR()
    {
        ResetRedirectCount();    
        RedirectButtonsOnOff(false);
        menusManager.PauseButtonOnOff(false);
        yield return new WaitForSeconds(waitAfterGotFacePart);

        //Debug.Log("facePartsIndex= " + gameManager.GetFacePartIndex());
        //Debug.Log("partsQuantity= " + gameManager.partsQuantity);
        if(gameManager.GetFacePartIndex() < gameManager.GetPartsQuantity())
        //if(gameManager.GetFacePartIndex() < 0) //for test
        {
            scenesManager.ChangeScene(EnumManager.scenes.PinballScene);
        }
        else
        {
            audioManager.Stop(EnumManager.audios.pinball);            
            scenesManager.ChangeScene(EnumManager.scenes.FaceBuilderScene);
        }
    }

    void RedirectButtonsOnOff(bool _newState)
    {
        redirectButtons.SetActive(_newState);
    }
    
    void ResetRedirectCount()
    {
        redirectCount = 0;
    }

    public void Exit()
    {
        scenesManager.ChangeScene(EnumManager.scenes.FaceBuilderScene);
    }

    void OnEnable()
    {
        CameraController.FinishTiltEvent += DisplayDownButton;
        Player.RedirectEvent += Redirected;
        ExitButtonHelper.ExitEvent += Exit;
        BoxHelper.GotFacePartEvent += GotFacePart;
    }

    void OnDisable()
    {
        CameraController.FinishTiltEvent -= DisplayDownButton;
        Player.RedirectEvent -= Redirected;
        ExitButtonHelper.ExitEvent -= Exit;
        BoxHelper.GotFacePartEvent -= GotFacePart;
    }

}
