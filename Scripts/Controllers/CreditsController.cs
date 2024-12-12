using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.ComponentModel;

public class CreditsController : MonoBehaviour
{    
    #region variables   
    TextManager textManager; 
    [SerializeField]
    GameObject creditHeaderPrefab;
    
    [SerializeField]
    GameObject creditInfoPrefab;
    
    GameObject creditTemp;
    
    int areasCount = 0;
    int headersIndex = 0;

    [SerializeField]
    RectTransform scrollView;

    [SerializeField]
    RectTransform content;
    
    [Space(10)]
    [Header("---------- Credits Info ----------")]

    [SerializeField]
    TextAsset excelDoc;
    
    [SerializeField]
    string[] info; 

    [SerializeField]
    List<string> headers = new List<string>();    
    
    [SerializeField]
    List<CreditsInfo> credits = new List<CreditsInfo>();    

    [Space(10)]
    [Header("---------- Control Values ----------")]    
    [SerializeField]
    int columns = 0;

    [SerializeField]
    int languageOffset = 0;

    [SerializeField]
    int creditsStartIndex = 0;
    
    [SerializeField]
    int creditsEndIndex = 0;
    
    [SerializeField]
    int creditsCurrentIndex = 0;

    float positionY;

    float spaceBetweenAreasAndCredits = 30.0f;

    float spaceBetweenAreas = 100.0f;

    //[SerializeField]
    float creditHeight = 100.0f;

    //[SerializeField]
    float spaceBetweenCredits = 50.0f; 

    //[SerializeField]
    Vector2 creditsOffset;
    #endregion

    void Awake()
    {
        //creditsOffset = new Vector2(0, 0);
        creditsOffset = new Vector2(scrollView.sizeDelta.x / 2f, 20);
        //Debug.Log("creditsOffset= " + creditsOffset);

        Assignments();

        GetInfo();
        SetColumns(int.Parse(info[0]));
        //SetLanguajeOffset();
        //SetCreditsStartIndex();
        //SetCreditsEndIndex(); 
        //FillCredits();        
        //AdjustContentSize();
        //SetCredits();
    }       

    void Assignments()
    {
        textManager = TextManager.textManager;
    }

    void GetInfo()
    {
        info = GetComponent<CreditsInfoHelper>().GetInfoFromExcel(excelDoc);
    }

    void SetLanguajeOffset()
    {
        for(int i = 0; i < System.Enum.GetValues(typeof(EnumManager.idioms)).Length; i++)
        {
            if(textManager.GetLanguage() == (EnumManager.idioms) i)            
            {
                languageOffset = i;
                //Debug.Log("languajeOffset= " + languageOffset);
                return;
            }
        }
    }    

    void SetCreditsStartIndex()
    {
        creditsStartIndex = 0;
        string wordToSearch = textManager.GetLanguage() +
         EnumManager.textIndicators.CreditsStartsHere.ToString();
        
        foreach(string text in info)        
        {
            if(text.Contains(wordToSearch))//contains instead of equals because text has blankspace at right if is last row
            {
                //Debug.Log("found");
                //creditsStartIndex += languageOffset;                
                //Debug.Log("credits start index= " + creditsStartIndex);
                return;
            }            
            //Debug.Log("searching...");
            creditsStartIndex += 1;
        }        
    }

    void SetCreditsEndIndex()
    {
        creditsEndIndex = 0;
        string wordToSearch = textManager.GetLanguage() + 
            EnumManager.textIndicators.CreditsEndsHere.ToString();
        
        foreach(string text in info)
        {
            if(text.Contains(wordToSearch))
            {
                //Debug.Log("credits in English end index= " + creditsEndIndex);
                //Debug.Log("found");
                creditsEndIndex -= (1 - languageOffset);
                return;
            }            
            //Debug.Log("searching...");
            creditsEndIndex += 1;
        }        
    }

    void FillCredits()    
    { 
        //Debug.Log("fill credits");        
        for(int i = 0; i < creditsEndIndex - creditsStartIndex - 2 - columns;) //el 2 corresponde a que se hacen 2 movimientos hacia abajo por que es area name, person name y link, hacer variable
        {                 
            //Debug.Log("creditsStartIndex + columns +i= " + (creditsStartIndex + columns + i)); // 3 + 2 + 0 = 5 // 3 + 2 + 8 = 13 // 3 + 2 + 16 = 21
            if(info[creditsStartIndex + columns + i].Contains(
                textManager.GetLanguage() + EnumManager.textIndicators.NewAreaStartsHere.ToString()))
            {                
                i += columns;
                //Debug.Log("new area");                
                //Debug.Log("newcredit1= " + (creditsStartIndex + (columns * 1) + i));    // 3 + (2 * 1) + 2 = 7  // 3 + (2) + 10 = 15 
                //Debug.Log("newcredit2= " + (creditsStartIndex + (columns * 2) + i));    // 3 + (2 * 2) + 2 = 9  // 3 + (4) + 10 = 17
                //Debug.Log("newcredit3= " + (creditsStartIndex + (columns * 3) + i));    // 3 + (2 * 3) + 2 = 11 // 3 + (6) + 10 = 19
                AddNewCredit(
                    info[creditsStartIndex + (columns * 1) + i],    // 7, 15, 23
                    info[creditsStartIndex + (columns * 2) + i],    // 9, 17, 25 
                    info[creditsStartIndex + (columns * 3) + i]     // 11, 19, 27
                );
                AddNewHeader(info[creditsStartIndex + (columns * 1) + i]);
                i += columns * 3; // 2+= (2 * 3) = 8 // 10+= (6) = 16 
                //Debug.Log("i + columns * 3= " + i);                                             
                areasCount += 1;
            }  
            else
            {
                //Debug.Log("same area");
                AddNewCredit(                    
                    headers[areasCount-1],
                    info[creditsStartIndex + (columns * 1) + i],
                    info[creditsStartIndex + (columns * 2) + i]
                 );
                i += columns * 2;
            }
        }  
    }

    void SetColumns(int _newValue)
    {
        columns = _newValue;
    }

    void AddNewHeader(string _header)
    {
        headers.Add(new string(_header));
    }

    void AddNewCredit(string _area, string _name, string _link)
    {
        credits.Add(new CreditsInfo(_area, _name, _link));
    }

    void SetCredits()
    {
        //Debug.Log("set credits");            
        creditsCurrentIndex = 0;        
        for(int i = 0; i < areasCount; i++)
        {
            AddHeader();   
            headersIndex+= 1;
            while(string.Equals(headers[i], credits[creditsCurrentIndex].area))
            {
                AddCredit(credits[creditsCurrentIndex]);
                creditsCurrentIndex += 1; 
                if(creditsCurrentIndex >= credits.Count)
                return;
            }
            positionY += spaceBetweenAreas;
        }             
    }

    void AddHeader()
    {
        creditTemp = Instantiate(creditHeaderPrefab, transform.position, Quaternion.identity);
        creditTemp.transform.SetParent(content);        
        CreditHeaderHelper creditHeaderHelper = creditTemp.GetComponent<CreditHeaderHelper>();
        Vector3 position = new Vector3(creditsOffset.x, -positionY - creditsOffset.y, 0);
        //Debug.Log("headerpos= " + position);                    
        positionY += spaceBetweenAreasAndCredits;
        creditHeaderHelper.SetPosition(position);
        creditHeaderHelper.SetInfo(headers[headersIndex]);
        creditHeaderHelper.SetScale(new Vector3(1, 1, 1));  
    }

    void AddCredit(CreditsInfo _credit)
    {        
        creditTemp = Instantiate(creditInfoPrefab, transform.position, Quaternion.identity);    
        creditTemp.transform.SetParent(content);        
        CreditInfoHelper creditInfoHelper = creditTemp.GetComponent<CreditInfoHelper>();
        Vector3 position = new Vector3(creditsOffset.x, -positionY - creditsOffset.y, 0);
        positionY += creditHeight + spaceBetweenCredits;
        //Debug.Log("creditpos= " + position);                    
        creditInfoHelper.SetPosition(position);
        creditInfoHelper.SetInfo(credits[creditsCurrentIndex]);
        creditInfoHelper.SetScale(new Vector3(1, 1, 1));                           
    }

    void AdjustContentSize()
    {        
        //content.sizeDelta = new Vector2(content.sizeDelta.x, (credits.Count * (creditHeight + spaceBetweenCredits)) + (areasCount * spaceBetweenCredits)); 
        content.sizeDelta = new Vector2(content.sizeDelta.x, positionY);
    }

    void OnEnable()
    {
        credits.Clear();
        headers.Clear();
        areasCount = 0;
        headersIndex = 0;
        positionY = 0;
        foreach(Transform creditTemp in content)
        {
            Destroy(creditTemp.gameObject);
        }
        SetLanguajeOffset();
        SetCreditsStartIndex();
        SetCreditsEndIndex(); 
        FillCredits();        
        SetCredits();
        AdjustContentSize();
    }

}
