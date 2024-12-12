using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHelper : MonoBehaviour
{
    //[SerializeField]
    TextManager textManager;

    [SerializeField]
    [Tooltip("english, spanish")]
    string[] textToShow;

    //[SerializeField]
    TextMeshProUGUI myTmp;

    void Awake()
    {
        Assignments();
    }

    void Start()
    {
        InitializeText();
    }

    void Assignments()
    {
        textManager = TextManager.textManager;

        myTmp = GetComponent<TextMeshProUGUI>();
    }

    void InitializeText()
    {
        myTmp.enableAutoSizing = true;
        myTmp.fontSizeMin = myTmp.fontSize - 5;
        myTmp.fontSizeMax = myTmp.fontSize + 5; 
    }

    internal void ChangeMyText()
    {
        //Debug.Log("soy " + myTmp.text + " change my text");
        if(textToShow.Length == 0)
        {
            return;
        }
        switch(textManager.GetLanguage())
        {
            case EnumManager.idioms.English:
                myTmp.text = textToShow[0];
                break;
            case EnumManager.idioms.Spanish:
                myTmp.text = textToShow[1];
                break;
            default:
                Debug.Log("no idiom");
                break;
        }
    }

    internal void ChangeMyFont()
    {
        //Debug.Log("soy " + myTmp.text + " change my font");        
        myTmp.font = textManager.GetCurrentFont();
    }

    void OnEnable()
    {
        //if(textToShow.Length > 0)
        //{
            ChangeMyText();
        //}
        ChangeMyFont();
    }
}
