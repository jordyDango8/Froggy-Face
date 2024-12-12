using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBuilderSceneController : MonoBehaviour
{
    #region variables
    internal delegate void SetIndicationsDelegate();
    internal static event SetIndicationsDelegate SetIndicationsEvent; 
    
    internal delegate void FinishIndicationsDelegate();
    internal static event FinishIndicationsDelegate FinishIndicationsEvent;     
    
    internal delegate void FinishAllIndicationsDelegate();
    internal static event FinishAllIndicationsDelegate FinishAllIndicationsEvent; 

    internal delegate void LoseDelegate();
    internal static event LoseDelegate LoseEvent;

    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    AudioManager audioManager;
    
    [SerializeField]
    ScenesManager scenesManager;

    [SerializeField]
    IndicationsController indicationsController;    

    [SerializeField]
    GenerateIndicationsHelper generateIndicationsHelper;    

    [SerializeField]
    GameObject readybutton;

    [SerializeField]
    GameObject resultsButton;

    [SerializeField]
    BuiltFaceController builtFaceController;

    [SerializeField]    
    internal static int partsChosen = 0; 

    [SerializeField]
    internal float waitBeforeGoToMainScreen = 5.0f;
    #endregion

    void Start()
    {        
        Assignments();
        Initialize();               
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
        scenesManager = ScenesManager.scenesManager;                
    } 

    void Initialize()
    {
        partsChosen = 0;        

        gameManager.SetFacePartIndex(2); // skip hair and face 
        gameManager.SetGameOver(false);

        audioManager.Play(EnumManager.audios.faceBuilder);        

        generateIndicationsHelper.GenerateRandomIndex();

        indicationsController.SetFirstIndications();
        
    }      

    public void Ready()
    {
        SetIndications();
    } 

    void SetIndications()
    {           
        //Debug.Log("set indications");
        switch(Random.Range(0, 6))
            {
                case 0:
                    audioManager.Play(EnumManager.audios.frogSound1);
                    break;
                case 1:
                    audioManager.Play(EnumManager.audios.frogSound2);
                    break;
                case 2:
                    audioManager.Play(EnumManager.audios.frogSound3);
                    break;
                case 3:
                    audioManager.Play(EnumManager.audios.frogSound4);
                    break;
                case 4:
                    audioManager.Play(EnumManager.audios.frogSound5);
                    break;
                case 5:
                    audioManager.Play(EnumManager.audios.frogSound6);
                    break;
                default:
                    Debug.Log("no frog sound");
                    break;

            }
        if(SetIndicationsEvent != null)
        {
            SetIndicationsEvent();
        }
    }

    public void ChoosePart()
    {
        StartCoroutine(ChoosePartCR()); //porque el evento que asigna el numero al jugador se llama después, arreglar    
    }    

    IEnumerator ChoosePartCR()
    {
        partsChosen += 1;
        if(partsChosen >= FacePartsButtonsController.facePartsButtonsQuantity)
        {
            if(FinishAllIndicationsEvent != null)
            {
                SetIndications(); //esto hace que se pongan grises, arreglar porque solo debe mostrar "ver resultados"
                yield return new WaitForSeconds(0.5f);
                ResultsButtonOnOff(true);
                FinishAllIndicationsEvent();            
                //StopCoroutine(ChoosePartCR());
            }            
        }
        else if(partsChosen == generateIndicationsHelper.indicationsQuantity)
        {            
            SetIndications();
            if(FinishIndicationsEvent != null)
            {
                FinishIndicationsEvent();               
            }                        
        }
    }   

    void ResultsButtonOnOff(bool _newState)
    {
        resultsButton.SetActive(_newState);
    }

    public void ShowBuiltFace()
    {
        builtFaceController.ShowBuiltFace();
        ResultsCR();
    }

    void ResultsCR()
    {                 
        audioManager.Stop(EnumManager.audios.faceBuilder);
        audioManager.Stop(EnumManager.audios.waterfall);

        if(gameManager.GetGameOver())
        {
            audioManager.Play(EnumManager.audios.wrongFace);
        }
        else
        {
            audioManager.Play(EnumManager.audios.rightFace);
        }                        
        
        gameManager.SetFacePartIndex(0); // here because pinball scene go to itself
    }    

    void FinishShowBuiltFace()
    {
        if(gameManager.GetGameOver())
        {
            Lose();
        }
        else
        {
            audioManager.Play(EnumManager.audios.yay);
            scenesManager.ChangeScene(EnumManager.scenes.WinScene);
        }
    }

    void Lose()
    {
        if(LoseEvent != null)
        {
            LoseEvent();
        }
        StartCoroutine(GoToMainScreen());
    }

    IEnumerator GoToMainScreen()
    {
        yield return new WaitForSeconds(waitBeforeGoToMainScreen);

        scenesManager.ChangeScene(EnumManager.scenes.MainScreenScene);
    }

    void OnEnable()
    {
        BuiltFaceController.FinishShowBuiltFaceEvent += FinishShowBuiltFace;
    }

    void OnDisable()
    {
        BuiltFaceController.FinishShowBuiltFaceEvent -= FinishShowBuiltFace;
    }

}
