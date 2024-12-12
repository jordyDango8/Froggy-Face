using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class Indications : MonoBehaviour
{
    /*
    #region variables
    internal delegate void FinishIndicationsDelegate(int partsChosen);
    internal static event FinishIndicationsDelegate FinishIndicationsEvent;

    [SerializeField]
    IdiomsManager idiomsManager;

    [SerializeField]
    TextAsset textExcell;

    [SerializeField]
    string[] text;  

    [SerializeField]
    GameObject indications;    

    [SerializeField]
    TextMeshProUGUI indicationsTMP;

    [SerializeField]
    int indicationIndex = 0;
    [SerializeField]
    int indicationIndexIncremet = 6;
    [SerializeField]
    int indicationsMax = 3;
    [SerializeField]
    int newRandomIndex;
    [SerializeField]
    int min;
    [SerializeField]
    int max;
    [SerializeField]
    int porcentaje1 = 45;
    [SerializeField]
    int porcentaje2 = 30;
    [SerializeField]
    int porcentaje3 = 12;
    [SerializeField]
    int porcentaje4 = 8;
    [SerializeField]
    int porcentaje5 = 5;
    [SerializeField]
    int indicationsNumber = 0;
    [SerializeField]
    int newIndicationsNumber = 0;
    [SerializeField]
    string newIndication;
    [SerializeField]
    int partsChosen = 0;
    [SerializeField]
    int FacePartsEnglishCount;
    [SerializeField]
    int newFrogIndicationIndex;
    [SerializeField]
    internal int[] frogIndications = new int[5] {-1, -1, -1, -1, -1};    
    [SerializeField]
    internal int frogIndicationsIndex = 0;  
    [SerializeField]
    #endregion
    
    void Start()
    {
        Assignments();
        //SetIndicationIndex();
        //GetText();             
        //SetIndication();           
        //SetIndication2();
        FacePartsEnglishCount = 
            EnumManager.FacePartsEnglish.GetNames(typeof(EnumManager.FacePartsEnglish)).Length;               
        EraseFrogIndications();
        GenerateFrogIndications();
        SetIndication3();
    }

    void Assignments()
    {
        idiomsManager = IdiomsManager.idiomsManager;
    }   

    internal void SetFrogIndications(int newValue)
    {
        frogIndications[frogIndicationsIndex] = newValue;
    }

    internal void GenerateFrogIndications()
    {
        for(int i = 0; i < FacePartsEnglishCount; i++)
        {
            //Debug.Log("i= " + i);
            newFrogIndicationIndex = Random.Range(0, FacePartsEnglishCount);        
            CheckRepeated();
            frogIndications[i] = newFrogIndicationIndex;
        } 
    }

    void CheckRepeated()
    {
        for(int j = 0; j < FacePartsEnglishCount; j++)
        {                
            //Debug.Log("j= " + j);
            if(newFrogIndicationIndex == frogIndications[j])
            {
                newFrogIndicationIndex = Random.Range(0, FacePartsEnglishCount);        
                CheckRepeated();
                break;
            }
        }
    }

    internal void EraseFrogIndications()
    {
        for(int i = 0; i < FacePartsEnglishCount; i++)
        {
            frogIndications[i] = -1;
        }
    }

    void SetIndicationIndex()
    {
        if(idiomsManager.currentLanguaje == EnumManager.languajes.english.ToString())
        {
            indicationIndex = 8;            
        }
        else
        {
            indicationIndex = 11;            
        }
    }

    void ChangeIndications(string newIndication)
    {
        indicationsTMP.text = newIndication;
    }

    void GetText()
    {        
        text = textExcell.text.Split(new string[] {";"}, System.StringSplitOptions.None);   
        int i = 0;
        foreach(string indication in text)
        {
            //Debug.Log("text= " + i + " " + indication);
            i++;
        }
        //0,1,2,3
        //english 8, 14, 20, 26
        //español 11, 17, 23, 29
    }

    void SetIndication()
    {
        //indicationIndex+(x*6)
        int x = Random.Range(0,indicationsMax);
        Debug.Log("x= " + x);
        ChangeIndications(text[indicationIndex+(x*6)]);        
    }

     void SetIndication2()
    {        
        EraseFrogIndications();
        GenerateFrogIndications();
               
        if(idiomsManager.currentLanguaje == EnumManager.languajes.english.ToString())
        {
            ChangeIndications((EnumManager.FacePartsEnglish) frogIndications[0] + ", " +
            (EnumManager.FacePartsEnglish) frogIndications[1] + ", " +
            (EnumManager.FacePartsEnglish) frogIndications[2] + ", " +
            (EnumManager.FacePartsEnglish) frogIndications[3] + ", " +
            (EnumManager.FacePartsEnglish) frogIndications[4]);        
        }
        else
        {
            ChangeIndications((EnumManager.FacePartsSpanish) frogIndications[0] + ", " +
            (EnumManager.FacePartsSpanish) frogIndications[1] + ", " +
            (EnumManager.FacePartsSpanish) frogIndications[2] + ", " +
            (EnumManager.FacePartsSpanish) frogIndications[3] + ", " +
            (EnumManager.FacePartsSpanish) frogIndications[4]);
        }
    }

    void SetIndication3()
    {
        int randomNumber = Random.Range(1, 101);

        min = 1;
        max = porcentaje1;
        if(randomNumber >= min && randomNumber <= max) //45
        {
            newIndicationsNumber = 1;
        }

        min = porcentaje1 + 1;
        max = porcentaje1 + porcentaje2;
        if(randomNumber >= min && randomNumber <= max) //30 75
        {
            newIndicationsNumber = 2;
        }

        min = porcentaje1 + porcentaje2 + 1;
        max = porcentaje1 + porcentaje2 + porcentaje3;
        if(randomNumber >= min && randomNumber <= max) //12 87
        {
            newIndicationsNumber = 3;
        }

        min = porcentaje1 + porcentaje2 + porcentaje3 + 1;
        max = porcentaje1 + porcentaje2 + porcentaje3 + porcentaje4;
        if(randomNumber >= min && randomNumber <= max + 7) //8 95
        {
            newIndicationsNumber = 4;
        }

        min = porcentaje1 + porcentaje2 + porcentaje3 + porcentaje4 + 1;
        max = porcentaje1 + porcentaje2 + porcentaje3 + porcentaje4 + porcentaje5;
        if(randomNumber >= min && randomNumber <= max) //5 100
        {
            newIndicationsNumber = 5;
        }
        indicationsNumber += newIndicationsNumber;
        
        newIndication = "";
        int lastIndicationIndex = indicationIndex;
        if(idiomsManager.currentLanguaje == EnumManager.languajes.english.ToString())
        {
            //while(indicationIndex < (lastIndicationIndex + indicationsNumber) && indicationIndex < 5) //5 numero magico
            while(indicationIndex < (indicationsNumber) && indicationIndex < 5) //5 numero magico
            {
                if(lastIndicationIndex == indicationIndex)
                {
                    newIndication += (EnumManager.FacePartsEnglish) frogIndications[indicationIndex];
                }
                else
                {
                    newIndication += ", " + (EnumManager.FacePartsEnglish) frogIndications[indicationIndex];
                }
                indicationIndex += 1;
            }
        }
        else
        {
            //español
        }
        ChangeIndications(newIndication);  
        IndicationsOnOff(false);
    }

    void IndicationsOnOff(bool on)
    {   
        if(on)
        {
            indications.SetActive(true);    
        }
        else
        {
            StartCoroutine(QuitIndications());
        }
    }

    IEnumerator QuitIndications()
    {
        yield return new WaitForSeconds(newIndicationsNumber);
        indications.SetActive(false);
    }    

    public void ChoosePart()
    {
        partsChosen += 1;
        if(partsChosen >= 5)
        {
            if(FinishIndicationsEvent != null)
            {
                FinishIndicationsEvent(partsChosen);
                return;
            }
        }
        //Debug.Log("parts chosen= " + partsChosen);
        //Debug.Log("indications number= " + indicationsNumber);
        if(partsChosen == indicationsNumber)
        {
            IndicationsOnOff(true);
            SetIndication3();
            if(FinishIndicationsEvent != null)
            {
                FinishIndicationsEvent(partsChosen);                
            }
            
        }
    }
    */

}
