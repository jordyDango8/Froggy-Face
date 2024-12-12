using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacePartButtonHelper : MonoBehaviour
{
    #region  variables
    internal delegate void chooseFacePartDelegate(int _newFacePartNumber, ImageInfo _newImage);
    internal static event chooseFacePartDelegate ChooseFacePartEvent; //suscribe on DataManager

    [SerializeField]
    GameManager gameManager;
    
    [SerializeField]
    int myFacePartNumber;
    
    [SerializeField]
    ImageInfo mySprites;
    
    [SerializeField]
    Image myImage;

    [SerializeField]
    Image buttonImage;

    [SerializeField]
    Button myButton;
    #endregion   

    internal void DisableEnableButton(bool _newState)
    {
        //Debug.Log("disable enable= " + _newState);
        //Debug.Log("myImage= " + myImage);
        if(_newState)
        {
            myImage.color = Color.white;
        }
        else
        {
            myImage.color = Color.gray;
        }
        myButton.interactable = _newState;
    }

    internal void AssignImageClass(ImageInfo _newSprites)
    {
        mySprites = _newSprites;
        ChangeSprite(mySprites.editedSprite);
    }

    void ChangeSprite(Sprite _newSprite)
    {
        myImage.sprite = _newSprite;
    }

    internal void ImageOnOff(bool _newState)
    {
        //Debug.Log("Image " + _newState);
        myImage.enabled = _newState;
        buttonImage.enabled = _newState;        
    }

    public void BeChosed()
    {       
        //Debug.Log("be chosed");
        //Debug.Log("myFacePartNumber= " + myFacePartNumber);
        if(ChooseFacePartEvent != null)
        {
            ChooseFacePartEvent(myFacePartNumber, mySprites);
        }
        //Debug.Log("partCount= " + gameManager.partCount);        
        gameManager.SetFacePartIndex(gameManager.GetFacePartIndex() + 1);
        ImageOnOff(false);        
    }

    void OnEnable()
    {
        Assignments();        
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;        
        myImage = gameObject.GetComponent<Image>(); // here because at start throw error
    }

}
