using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    AudioManager audioManager;
    TextManager textManager;   
    ScenesManager scenesManager;    
    GameManager gameManager;

    [SerializeField]
    TMP_Dropdown languageDropdown;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    TMP_Dropdown qualityDropdown;    
    
    [SerializeField]
    TMP_Dropdown resolutionDropdown;

    //[SerializeField]
    Resolution[] resolutions;  

    [SerializeField]
    TMP_Dropdown fontDropdown; 

    [SerializeField]
    TMP_FontAsset[] fonts;
    
    int qualityIndex;
    int resolutionIndex;
    int defaultResolution;

    void Start()
    {
        Assignments();
        Initialize();                            
    }   

    internal void LoadPreferences() //called from LogosSceneController
    {
        //Debug.Log("load preferences");
        LoadQuality();    
        LoadResolution(); 
        LoadVolume();                   
    }

    void LoadQuality()
    {
        LoadQualityIndex();
        QualitySettings.SetQualityLevel(qualityIndex);             
    }

    void LoadQualityIndex()
    {
        qualityIndex = PlayerPrefs.GetInt(EnumManager.intType.qualityIndex.ToString(), 1); // medium default
        //Debug.Log("loaded quality index= " + qualityIndex);
    }

    void LoadResolution()
    {
        resolutions = Screen.resolutions;
        LoadResolutionIndex();
        if(resolutionIndex < 0)
        {
            return;
        }
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);  
        //Debug.Log("loaded resolution= " + resolutions[resolutionIndex]);
        //Debug.Log("loaded resolution= " + Screen.currentResolution);
    }

    void LoadResolutionIndex()
    {
        resolutionIndex = PlayerPrefs.GetInt(EnumManager.intType.resolutionIndex.ToString(), -1);
        //resolutionIndex = -1; // for test
        //Debug.Log("loaded resolution index= " + resolutionIndex);
    }

    void LoadVolume()
    {
        audioManager = AudioManager.audioManager;             
        audioManager.ChangeMasterVolume(audioManager.GetVolume());
    }  

    void Assignments()
    {
        textManager = TextManager.textManager;                
        scenesManager = ScenesManager.scenesManager;
        gameManager = GameManager.gameManager;
        fonts = textManager.GetFonts();
        GetDefaultResolution();
    }

    void GetDefaultResolution()
    {
        for(int i = 0; i < resolutions.Length; i++)
        {            
            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                defaultResolution = i;
            }
        }
    }

    void Initialize()
    {                
        InitializeQuality();
        InitializeResolutions();  
        InitializeVolume();    
        InitializeFont();
        InitializeLanguage();                  
    }    

    void InitializeQuality()
    {        
        //Debug.Log("initialize quality");
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();
        //Debug.Log("qualityIndex= " + qualityIndex);
    }    
    
    void InitializeResolutions()
    {
        FillResolutions();
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }    

    void FillResolutions()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        //resolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height
               && resolutionIndex < 0)
            {
                resolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);              
    }

    void InitializeVolume()
    {
        volumeSlider.value = audioManager.GetVolume();
    }

    void InitializeFont()
    {
        fontDropdown.value = textManager.GetFontIndex();
        fontDropdown.RefreshShownValue(); 
    }

    void InitializeLanguage()
    {
        languageDropdown.value = textManager.GetLanguageIndex();
        languageDropdown.RefreshShownValue(); 
    }
   
   /*
    void FillFonts()
    {
        fontDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentFontIndex = 0;
        for(int i = 0; i < fonts.Length; i++)
        {
            string font = fonts[i].name;
            options.Add(font);

            if(i == textManager.GetFontIndex())               
            {
                currentFontIndex = i;
            }
        }

        fontDropdown.AddOptions(options);
        fontDropdown.value = currentFontIndex;
        fontDropdown.RefreshShownValue();
    }
    */        

    public void SetLanguage(int _languageIndex)
    {
        //Debug.Log("set languaje");        
        textManager.SetLanguageIndex(_languageIndex);        
    }

    public void SetVolume (float _volume)
    {
        //Debug.Log(volume);
        audioManager.SetVolume(_volume);        
    }    

    public void SetQuality(int _qualityIndex)
    {
        //Debug.Log(_qualityIndex);
        QualitySettings.SetQualityLevel(_qualityIndex);                
        SaveQualityIndex(_qualityIndex);
    }    

    void SaveQualityIndex(int _newValue)
    {
        PlayerPrefs.SetInt(EnumManager.intType.qualityIndex.ToString(), _newValue);
        //Debug.Log("saved qualityIndex= " + PlayerPrefs.GetInt(EnumManager.intType.qualityIndex.ToString()));
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        SaveCurrentResolutionIndex(_resolutionIndex);
    }

    void SaveCurrentResolutionIndex(int _newValue)
    {
        PlayerPrefs.SetInt(EnumManager.intType.resolutionIndex.ToString(), _newValue);
        //Debug.Log("saved resolution index= " + PlayerPrefs.GetInt(EnumManager.intType.resolutionIndex.ToString()));
    }

    public void SetFont(int _fontIndex)
    {
        textManager.SetFontIndex(_fontIndex);
    }    

    public void Reset()
    {
        SetLanguage(0);
        InitializeLanguage();
        SetVolume(-10);
        InitializeVolume();
        SetQuality(1);
        InitializeQuality();
        SetResolution(defaultResolution);
        InitializeResolutions();
        SetFont(2);
        InitializeFont();
    }

    public void Return()
    {
        if(scenesManager.GetInGameplay())
        {
            gameManager.Pause();            
        }
        //Debug.Log("return");
        //SaveInfo();
    }

}
