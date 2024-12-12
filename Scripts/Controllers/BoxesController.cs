using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesController : MonoBehaviour
{
    #region variables
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    SpritesManager spritesManager;

    [SerializeField]
    GameObject[] boxes;

    [SerializeField]
    int boxesQuantity;
    
    [SerializeField]
    int randomIndexOfFacePartMin = 0;
    
    [SerializeField]
    int randomIndexOfFacePartMax = 2;
    
    [SerializeField]
    int newRandomNumberOfFacePart;
    
    [SerializeField]
    int[] randomNumbersOfFaceParts;    
    #endregion

    void Start()
    {
        Assignments();
        SetRandomFacePartOnBoxes();
    }
    
    void Assignments()
    {
        gameManager = GameManager.gameManager;
        spritesManager = SpritesManager.spritesManager;

        boxesQuantity = boxes.Length;
    }

    void SetRandomFacePartOnBoxes()
    {
        GenerateRandorNumbers();        
        switch(gameManager.GetFacePartIndex())
        {
            case 0:
                CallAssignFacePartOnEachBox(spritesManager.faces);
                break;
            case 1:
                CallAssignFacePartOnEachBox(spritesManager.hair);
                break;
            case 2:
                CallAssignFacePartOnEachBox(spritesManager.eyeBrows);
                break;
            case 3:
                CallAssignFacePartOnEachBox(spritesManager.eyes);
                break;
            case 4:
                CallAssignFacePartOnEachBox(spritesManager.nose);
                break;
            case 5:
                CallAssignFacePartOnEachBox(spritesManager.nasolabialFold);
                break;
            case 6:
                CallAssignFacePartOnEachBox(spritesManager.mouths);
                break;                
            default:
                Debug.Log("no part");
                break;
        }
    }

     void GenerateRandorNumbers()
    {
        EraseRandomNumbers();
        for(int i = 0; i < boxesQuantity; i++)
        {
            //Debug.Log("i= " + i);
            newRandomNumberOfFacePart = Random.Range(randomIndexOfFacePartMin, randomIndexOfFacePartMax + 1);            
            CheckRepeated();      
            randomNumbersOfFaceParts[i] = newRandomNumberOfFacePart;      
        }
    }

    void EraseRandomNumbers()
    {
        for(int i = 0; i <= boxesQuantity - 1; i++)
        {
            //Debug.Log("i= " + i);
            randomNumbersOfFaceParts[i] = randomIndexOfFacePartMin - 1;
        }
    }

    void CheckRepeated()
    {
        //Debug.Log("Check repeated");
        for(int j = 0; j < boxesQuantity; j++)
            {
                if(newRandomNumberOfFacePart == randomNumbersOfFaceParts[j])
                {
                    //Debug.Log("j= " + j);
                    newRandomNumberOfFacePart = Random.Range(randomIndexOfFacePartMin, randomIndexOfFacePartMax + 1);            
                    CheckRepeated();
                }
            }
    }    

    void CallAssignFacePartOnEachBox(ImageInfo[] _sprites)
    {
        for(int i = 0; i < boxesQuantity; i++)
        {        
            //Debug.Log("original sprite= " + sprites[randomNumbersOfFaceParts[i]].originalSprite);
            //Debug.Log("edited sprite= " + sprites[randomNumbersOfFaceParts[i]].editedSprite);    
            boxes[i].GetComponent<BoxHelper>().AssignSprites(_sprites[randomNumbersOfFaceParts[i]].originalSprite, _sprites[randomNumbersOfFaceParts[i]].editedSprite);
            boxes[i].GetComponent<BoxHelper>().ChangeSprite(_sprites[randomNumbersOfFaceParts[i]].editedSprite);
            //boxes[i].GetComponent<BoxHelper>().AssignSprites(_sprites[0].originalSprite, _sprites[0].editedSprite); // for test on device
            //boxes[i].GetComponent<BoxHelper>().ChangeSprite(_sprites[0].editedSprite); // for test on device
        }
    }

}
