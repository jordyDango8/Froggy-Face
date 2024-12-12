using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateIndicationsHelper : MonoBehaviour
{
    #region variables      

    [SerializeField]
    TextManager textManager;

    [SerializeField]
    int indicationIndex = 0;
   
    [SerializeField]
    int percentMin;
    
    [SerializeField]
    int percentMax;
    
    [SerializeField]
    int percent1 = 45;
    
    [SerializeField]
    int percent2 = 30;
    
    [SerializeField]
    int percent3 = 12;
    
    [SerializeField]
    int percent4 = 8;
    
    [SerializeField]
    int percent5 = 5;
    
    [SerializeField]
    internal int indicationsQuantity = 0;
    
    [SerializeField]    
    internal int newIndicationsQuantity = 0;
    
    [SerializeField]
    string newIndication;    
    
    [SerializeField]
    int facePartsEnglishQuantity;
    
    [SerializeField]    
    int newRandomIndex;
    
    [SerializeField]
    internal int[] frogIndications = new int[5] {-1, -1, -1, -1, -1}; // to chech if match with player choices     
    
    [SerializeField]
    int lastIndicationIndex = 0;
    #endregion

    void Awake()
    {
        Assignments();                       
    }

    void Assignments()
    {
        textManager = TextManager.textManager;
        
        facePartsEnglishQuantity = 
            textManager.GetTextArray(EnumManager.text.faceIndications).Length;
        //Debug.Log("face parts english quantity= " + facePartsEnglishQuantity);            
    }   

    internal void GenerateRandomIndex() // called from FaceBuilderSceneController
    {
        EraseRandomIndex();
        for(int i = 0; i < facePartsEnglishQuantity; i++)
        {
            //Debug.Log("i= " + i);
            newRandomIndex = Random.Range(0, facePartsEnglishQuantity);        
            CheckRepeated();
            frogIndications[i] = newRandomIndex;
        } 
    }

    void CheckRepeated()
    {
        for(int j = 0; j < facePartsEnglishQuantity; j++)
        {                
            //Debug.Log("j= " + j);
            if(newRandomIndex == frogIndications[j])
            {
                newRandomIndex = Random.Range(0, facePartsEnglishQuantity);        
                CheckRepeated();
                break;
            }
        }
    }

    internal void EraseRandomIndex()
    {
        for(int i = 0; i < facePartsEnglishQuantity; i++)
        {
            frogIndications[i] = -1;
        }
    }

    internal string GenerateIndications()
    {
        GenerateIndicationsQuantity();
        
        newIndication = "";
        AssignIndications(textManager.GetTextArray(EnumManager.text.faceIndications));
        /*
        if(textManager.GetCurrentLanguage() == EnumManager.idioms.English)
        {
            //AssignIndications(EnumManager.facePartsEnglish.GetNames(typeof(EnumManager.facePartsEnglish)));
            AssignIndications(textManager.GetTextArray(EnumManager.text.faceIndications));
        }
        else
        {
            //AssignIndications(EnumManager.facePartsSpanish.GetNames(typeof(EnumManager.facePartsEnglish)));
        }
        */
        //Debug.Log("newIndication= " + newIndication);
        return newIndication;
    }

    void AssignIndications(string[] _faecParts)
    {
        //Debug.Log("indicationIndex= " + indicationIndex);
        //Debug.Log("indicationsQuantity= " + indicationsQuantity);
        //Debug.Log("facePartsEnglisgQuantity= " + facePartsEnglishQuantity);
        while(indicationIndex < (indicationsQuantity) && indicationIndex < facePartsEnglishQuantity)
        {
            if(lastIndicationIndex == indicationIndex)
            {
                newIndication += _faecParts[frogIndications[indicationIndex]];
            }
            else
            {
                newIndication += ", " + _faecParts[frogIndications[indicationIndex]];
            }
            indicationIndex += 1;
        }        
    }

    void GenerateIndicationsQuantity()
    {
        int randomNumber = Random.Range(1, 101);

        percentMin = 1;
        percentMax = percent1;
        if(randomNumber >= percentMin && randomNumber <= percentMax) //45
        {
            newIndicationsQuantity = 1;
        }

        percentMin = percent1 + 1;
        percentMax = percent1 + percent2;
        if(randomNumber >= percentMin && randomNumber <= percentMax) //30 75
        {
            newIndicationsQuantity = 2;
        }

        percentMin = percent1 + percent2 + 1;
        percentMax = percent1 + percent2 + percent3;
        if(randomNumber >= percentMin && randomNumber <= percentMax) //12 87
        {
            newIndicationsQuantity = 3;
        }

        percentMin = percent1 + percent2 + percent3 + 1;
        percentMax = percent1 + percent2 + percent3 + percent4;
        if(randomNumber >= percentMin && randomNumber <= percentMax + 7) //8 95
        {
            newIndicationsQuantity = 4;
        }

        percentMin = percent1 + percent2 + percent3 + percent4 + 1;
        percentMax = percent1 + percent2 + percent3 + percent4 + percent5;
        if(randomNumber >= percentMin && randomNumber <= percentMax) //5 100
        {
            newIndicationsQuantity = 5;
        }
        indicationsQuantity += newIndicationsQuantity;        
        lastIndicationIndex = indicationIndex;        
    }    

}
