using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiltFaceController : MonoBehaviour
{   
    internal delegate void FinishShowBuiltFaceDelegate();
    internal static event FinishShowBuiltFaceDelegate FinishShowBuiltFaceEvent;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    PlayerDataManager playerDataManager;

    [SerializeField]
    SpritesManager spritesManager;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    GameObject[] builtFacePartsGO; 

    [SerializeField]
    SpriteRenderer[] builtFacePartsSR; 

    [SerializeField]
    GenerateIndicationsHelper generateIndicationsHelper;

    [SerializeField]
    Sprite rightHair;
    
    [SerializeField]
    Sprite backHair;
    
    [SerializeField]
    Sprite frontHair;

    [SerializeField]
    Sprite transparent; 
    
    [SerializeField]
    float waitBetweenParts = 0.5f;

    void Start()
    {
        Assignments();
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;
        playerDataManager = PlayerDataManager.playerDataManager;
        spritesManager = SpritesManager.spritesManager;
        audioManager = AudioManager.audioManager;

        for(int i = 0; i < builtFacePartsGO.Length; i++)
        {
            builtFacePartsSR[i] = builtFacePartsGO[i].GetComponent<SpriteRenderer>();
        }
    }

    void CheckIfGameOver()
    {
        FillBuiltFacePartsSR();    
        if(gameManager.GetGameOver())    
        {
            return;
        }
        CheckIfPlayerChoiceMatchRightFaceParts();
    }

    void FillBuiltFacePartsSR()
    {
        EraseBuiltFaceParts();
         if(playerDataManager.gottenFaceParts[1].originalSprite == rightHair)
        {
            //Debug.Log("right hair");
            builtFacePartsSR[0].sprite = backHair;
            builtFacePartsSR[7].sprite = frontHair;
        }
        else
        {
            //Debug.Log("wrong hair");
            builtFacePartsSR[7].sprite = playerDataManager.gottenFaceParts[1].originalSprite;
        }
        
        builtFacePartsSR[1].sprite = playerDataManager.gottenFaceParts[0].originalSprite;
        for(int i = 2; i <= 6; i++)
        {                        
            //Debug.Log("frogIndication= " + generateIndicationsHelper.frogIndications[i-2]);
            //Debug.Log("playerFacePartNumber= " + playerDataManager.playerFacePartNumbers[i-2]);            
            if(generateIndicationsHelper.frogIndications[i-2] == playerDataManager.playerFacePartNumbers[i-2])
            {
                int index = playerDataManager.playerFacePartNumbers[i-2]+2;
                builtFacePartsSR[index].sprite =
                 playerDataManager.playerFacePartChosen[i-2].originalSprite;                
            }
            else
            {
                gameManager.SetGameOver(true); //comment to test right parts
                //Debug.Log("frog said " + generateIndicationsHelper.frogIndications[i-2]);
                //Debug.Log("I chose " + playerDataManager.playerFacePartNumbers[i-2]);
                //Debug.Log("the part " + builtFacePartsGO[generateIndicationsHelper.frogIndications[i-2]+2].transform.GetChild(0));
                //Debug.Log("must have " + playerDataManager.playerFacePartChosen[i-2].editedSprite);
                builtFacePartsGO[generateIndicationsHelper.frogIndications[i-2]+2].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                 playerDataManager.playerFacePartChosen[i-2].editedSprite;
            }
        }       
    }

    void EraseBuiltFaceParts()
    {
        foreach(SpriteRenderer part in builtFacePartsSR)
        {
            part.sprite = transparent;
        }
    }

    void CheckIfPlayerChoiceMatchRightFaceParts()
    {
        for(int i = 0; i < builtFacePartsGO.Length; i++)
        {
            //Debug.Log("builtFacePartSR= " + builtFacePartsSR[i].sprite);
            //Debug.Log("rightFacePart= " + spritesManager.rightFaceParts[i]);
            if(builtFacePartsSR[i].sprite != spritesManager.rightFaceParts[i])
            {
                //Debug.Log("part " + i + " no match");
                gameManager.SetGameOver(true); //comment to test right parts
                return;
            }
        }
    }

    internal void ShowBuiltFace()
    {
        StartCoroutine(ShowBuiltFaceCR());
    }

    IEnumerator ShowBuiltFaceCR()
    {
        if(!gameManager.GetGameOver())
        {
            waitBetweenParts *= 2f;
        }
        for(int i = 0; i < builtFacePartsGO.Length; i++)
        {
            if(gameManager.GetGameOver() && Random.Range(0, 100) < 8)
            {
                audioManager.Play(EnumManager.audios.noYay);
            }
            if(i == 0)
            {
                builtFacePartsGO[builtFacePartsGO.Length - 1].SetActive(true);
            }
            builtFacePartsGO[i].SetActive(true);

            yield return new WaitForSeconds(waitBetweenParts);
        }     
        if(FinishShowBuiltFaceEvent != null)
        {
            FinishShowBuiltFaceEvent();
        }           
    }

    void OnEnable()
    {
        FaceBuilderSceneController.FinishAllIndicationsEvent += CheckIfGameOver;
    }

    void OnDisable()
    {
        FaceBuilderSceneController.FinishAllIndicationsEvent -= CheckIfGameOver;
    }
}
