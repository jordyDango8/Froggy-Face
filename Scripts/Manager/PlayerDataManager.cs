using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    #region variables
    internal static PlayerDataManager playerDataManager;
    
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    internal ImageInfo[] gottenFaceParts; // on pinball scene

    [SerializeField]
    internal ImageInfo[] playerFacePartChosen; // on face builder scene

    [SerializeField]
    internal int[] playerFacePartNumbers; // to check if match with frog indications
    #endregion

    void Awake()
    {
        if(playerDataManager == null)
        {
            playerDataManager = this;
        }
        else
        {
            Destroy(gameObject);            
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameManager = GameManager.gameManager;
    }

    internal void GotFacePart(Sprite _originalSprite, Sprite _editedSprite)
    {
        //Debug.Log("got face part");
        gottenFaceParts[gameManager.GetFacePartIndex()].originalSprite = _originalSprite;
        gottenFaceParts[gameManager.GetFacePartIndex()].editedSprite = _editedSprite;
    }   

    void SetChoiceOfPlayer(int _newFacePartNumber, ImageInfo _newImage)
    {
        SetPlayerFacePartNumbers(_newFacePartNumber);
        SetPlayerFacePartChosen(_newImage);
    }

    void SetPlayerFacePartNumbers(int _newValue)
    {
        playerFacePartNumbers[gameManager.GetFacePartIndex() - 2] = _newValue;
    }

    internal void SetPlayerFacePartChosen(ImageInfo _newSprites)
    {
        //Debug.Log("set player face part chosen");
        playerFacePartChosen[gameManager.GetFacePartIndex() - 2] = _newSprites;
    }

    void OnEnable()
    {
        FacePartButtonHelper.ChooseFacePartEvent += SetChoiceOfPlayer;        
    }

    void OnDisable()
    {
        FacePartButtonHelper.ChooseFacePartEvent -= SetChoiceOfPlayer;        
    }
}
