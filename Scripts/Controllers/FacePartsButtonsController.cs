using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePartsButtonsController : MonoBehaviour
{
    [SerializeField]
    PlayerDataManager playerDataManager;

    [SerializeField]
    float waitBeforeSetFacePartsButtonImage = 0.1f;

    [SerializeField]
    GameObject facePartsButtonsFather;
    
    [SerializeField]
    GameObject[] facePartsButtons;

    [SerializeField]
    internal static int facePartsButtonsQuantity;  

    [SerializeField]
    float waitBeforeOnOffImageButtons = 0.1f; 

    void Start()
    {
        Assignments();  
        Initialize();      
    }

    void Assignments()
    {
        playerDataManager = PlayerDataManager.playerDataManager;

        facePartsButtonsQuantity = facePartsButtons.Length;
    }    

    void Initialize()
    {
        FacePartsButtonsOnOff(false);
    }

    void FrogSaidIndications()
    {
        SetFacePartsButtonsImage();
        ChangeFacePartsButtonsState(false);        
    }

    void SetFacePartsButtonsImage()
    {
        StartCoroutine(SetFacePartsButtonsImageCR());  
    }    

    IEnumerator SetFacePartsButtonsImageCR()
    {
        yield return new WaitForSeconds(waitBeforeSetFacePartsButtonImage);
            
        for(int i = 0; i < facePartsButtonsQuantity; i++)
        {
            //Debug.Log("button= " + facePartsButtons[i].name);
            //Debug.Log("item= " + playerDataManager.gottenFaceParts[i+2].originalSprite);
            facePartsButtons[i].GetComponent<FacePartButtonHelper>().AssignImageClass(playerDataManager.gottenFaceParts[i+2]);
        }        
    }

    void ShowFacePartsButtonsImage()
    {
        StartCoroutine(FacePartsButtonsImageOnOff(true));
    }

    IEnumerator FacePartsButtonsImageOnOff(bool _newState)
    {
        yield return new WaitForSeconds(waitBeforeOnOffImageButtons);
        foreach(GameObject button in facePartsButtons)
        {
            //button.SetActive(_newState);
            button.GetComponent<FacePartButtonHelper>().ImageOnOff(_newState);
        }        
    }

    void EnableButtons()
    {
        ChangeFacePartsButtonsState(true);
    }

    void ChangeFacePartsButtonsState(bool _newState)
    {
        foreach(GameObject button in facePartsButtons)
        {
            button.GetComponent<FacePartButtonHelper>().DisableEnableButton(_newState);
        }   
    }

    void HideFacePartsButtons()                    
    {
        FacePartsButtonsOnOff(false);
    }

    void FacePartsButtonsOnOff(bool _newState)
    {
        facePartsButtonsFather.SetActive(_newState);
    } 

    void FinishAllIndications()
    {
        HideFacePartsButtons();        
    }    

    void OnEnable()
    {
        FaceBuilderSceneController.SetIndicationsEvent += FrogSaidIndications;
        FaceBuilderSceneController.FinishIndicationsEvent += ShowFacePartsButtonsImage;
        IndicationsController.QuitIndicationsEvent += EnableButtons;
        FaceBuilderSceneController.FinishAllIndicationsEvent += HideFacePartsButtons;
    }

    void OnDisable()
    {
        FaceBuilderSceneController.SetIndicationsEvent -= FrogSaidIndications;
        FaceBuilderSceneController.FinishIndicationsEvent -= ShowFacePartsButtonsImage;
        IndicationsController.QuitIndicationsEvent -= EnableButtons;
        FaceBuilderSceneController.FinishAllIndicationsEvent -= HideFacePartsButtons;
    }

}