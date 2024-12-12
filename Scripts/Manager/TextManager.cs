using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextManager : MonoBehaviour
{
    internal static TextManager textManager;  

    EnumManager.idioms currentLanguage; 

    [SerializeField]
    TMP_FontAsset[] fonts;


    TMP_FontAsset currentFont;

    int fontIndex;
    int languageIndex;

    List<TextHelper> textHelpers = new List<TextHelper>();

    [HideInInspector]
    public string[] qualityOptionsEnglish = new string[]
    {
        "low",
        "medium",
        "high",        
    };

    [HideInInspector]
    public string[] qualityOptionsSpanish = new string[]
    {
        "baja",
        "media",
        "alta",        
    };

    [HideInInspector]
    public string[] faceIndicationsEnglish = new string[]
    {
        "eyebrows",
        "eyes",
        "nose",
        "nasolabial fold",
        "mouth",
    };

    [HideInInspector]
    public string[] faceIndicationsSpanish = new string[]
    {
        "cejas",
        "ojos",
        "nariz",
        "surco nasolabial",
        "boca",
    };

    [HideInInspector]
    public string firstIndicationsEnglish = "Please follow my indications, press 'ready' button";
    [HideInInspector]
    public string firstIndicationsSpanish = "Sigue mis indicaciones por favor, presiona el boton 'listo'";

    [HideInInspector]
    public string loseMessageEnglish = "Mmm... I swear the princess looked different";
    [HideInInspector]
    public string loseMessageSpanish = "Mmm... juraría que la princesa se veía diferente";    
    
    [HideInInspector]
    public string finishMessageEnglish = "Well, let's see the result";
    [HideInInspector]
    public string finishMessageSpanish = "Bien, veamos el resultado";        
    
    void Awake()
    {
        if(textManager == null)
        {
            textManager = this;
        }
        else
        {
            Destroy(gameObject);            
        }
        DontDestroyOnLoad(gameObject);
        LoadFontIndex();
        LoadLanguageIndex();
    }

    void Start()
    {
        //LoadFontIndex();
        //SetCurrentFont(2); 

        //currentLanguage = EnumManager.idioms.English; //for test
        //currentLanguage = EnumManager.idioms.Spanish; //for test
        //currentLanguage = EnumManager.idioms.Japanese; //for test                   
    }

    void Update()
    {
        //Debug.Log("current language= " + currentLanguage);
    }

    internal EnumManager.idioms GetLanguage()
    //internal string GetLanguage()
    {
        return currentLanguage;
    }

    internal string GetText(EnumManager.text variableToSearch)
    {
        string content = (string)this.GetType().GetField(variableToSearch.ToString() + GetLanguage()).GetValue(this);
        //Debug.Log("varContent= " + content);
        return content;
    }

    internal string[] GetTextArray(EnumManager.text variableToSearch)
    {
        string[] content = (string[])this.GetType().GetField(variableToSearch.ToString() + GetLanguage()).GetValue(this);
        return content;
        //Debug.Log("varContent= " + content);
    }

    internal TMP_FontAsset[] GetFonts()
    {
        return fonts;
    }

    internal int GetFontIndex()
    {
        return fontIndex;
    }

    internal void SetFontIndex(int newFont)
    {
        fontIndex = newFont;
        SaveFontIndex();
        SetCurrentFont();
        //UpdateTextsFont();
    }

    void SaveFontIndex()
    {
        PlayerPrefs.SetInt(EnumManager.intType.fontIndex.ToString(), fontIndex);
        //Debug.Log("save current font index= " + fontIndex);
    }

    internal TMP_FontAsset GetCurrentFont()
    {
        return currentFont;
    }

    void LoadFontIndex()
    {
        fontIndex = PlayerPrefs.GetInt(EnumManager.intType.fontIndex.ToString(), 2);
        //Debug.Log("load current font index= " + fontIndex);
        SetCurrentFont();
    }

    internal void SetCurrentFont()
    {
        currentFont = fonts[fontIndex];
        UpdateTextsFont();
    }
    
    internal void UpdateTextsFont()
    {     
        /*
        if(UpdateTextEvent != null)
        {
            UpdateTextEvent();
        }
        */
        EraseTextHelpers();
        textHelpers = FindObjectsOfType<TextHelper>().Select(TextHelper => TextHelper).ToList();       
        foreach(TextHelper textHelper in textHelpers)
        {
            textHelper.ChangeMyFont();
        }
    }

    void EraseTextHelpers()
    {
        textHelpers.Clear();
    }

    internal void SetLanguageIndex(int newValue)
    {
        languageIndex = newValue;
        //Debug.Log("languageIndex= " + languageIndex);
        SaveLanguageIndex();
        SetLanguage();
    }

    internal int GetLanguageIndex()
    {
        return languageIndex;
    }

    //internal void SetLanguage(string _newLanguage)
    internal void SetLanguage()
    {
        //switch(_newLanguage)
        //currentLanguage = _newLanguage;    
        //currentLanguage = (EnumManager.idioms)System.Enum.Parse( typeof(EnumManager.idioms), _newLanguage );
        //currentLanguage = languages[languageIndex];         
        currentLanguage = (EnumManager.idioms) languageIndex;
        UpdateTextsLanguage();        
    }  
    
    void UpdateTextsLanguage()
    {     
        //Debug.Log("update texts");
        //if(UpdateTextEvent != null)
        //{
        //    UpdateTextEvent();
        //}
        EraseTextHelpers();
        //textHelpers = FindObjectsOfType<TextHelper>();                  
        //textHelpers = FindObjectsOfType<TextHelper>(true); 
        textHelpers = FindObjectsOfType<TextHelper>().Select(TextHelper => TextHelper).ToList();       
        foreach(TextHelper textHelper in textHelpers)
        {
            textHelper.ChangeMyText();
        }
    }

    //internal void SaveCurrentLanguage()    
    void SaveLanguageIndex()    
    {
        PlayerPrefs.SetInt(EnumManager.intType.languageIndex.ToString(), languageIndex);
        //Debug.Log("save current language= " + currentLanguage);
    }

    internal void LoadLanguageIndex()    
    {
        languageIndex = PlayerPrefs.GetInt(EnumManager.intType.languageIndex.ToString());
        SetLanguage();
        
        //currentLanguage = 
        //(EnumManager.idioms)System.Enum.Parse( typeof(EnumManager.idioms), PlayerPrefs.GetString(EnumManager.stringType.currentLanguage.ToString()
        //,EnumManager.idioms.English.ToString()        
        //));

        //Debug.Log("load current language= " + currentLanguage);
    }

}