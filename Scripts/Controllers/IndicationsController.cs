using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class IndicationsController : MonoBehaviour
{   
    internal delegate void QuitIndicationsDelegate();
    internal static event QuitIndicationsDelegate QuitIndicationsEvent;

    [SerializeField]
    TextManager textManager;

    [SerializeField]
    GenerateIndicationsHelper generateIndicationsHelper;

    [SerializeField]
    TextMeshProUGUI indicationsTMP;

    [SerializeField]
    GameObject indicationsGO;  

    void Awake()
    {
        textManager = TextManager.textManager;
    }    

    void Start()
    {
        Assignments();
    }

    void Assignments()
    {
        generateIndicationsHelper = GetComponent<GenerateIndicationsHelper>();
    }

    internal void SetFirstIndications()
    {
        IndicationOnOff(true);        
        SetIndications(textManager.GetText(EnumManager.text.firstIndications));        
    }

    void ShowIndications()
    {
        //Debug.Log("setIndications");       
        IndicationOnOff(true);
        SetIndications(generateIndicationsHelper.GenerateIndications());    
        StartCoroutine(QuitIndications());  
    }

    void SetIndications(string _newIndications)
    {
        //Debug.Log("set indications= " + _newIndications);
        indicationsTMP.text = _newIndications;    
    }   

    IEnumerator QuitIndications()
    {
        yield return new WaitForSeconds(generateIndicationsHelper.newIndicationsQuantity);
        if(QuitIndicationsEvent != null)
        {
            QuitIndicationsEvent();
        }
        IndicationOnOff(false);
    }  

    void IndicationOnOff(bool _newState)
    {
        indicationsGO.SetActive(_newState);
    }   

    void ShowLoseMessage()
    {
        IndicationOnOff(true);
        SetIndications(textManager.GetText(EnumManager.text.loseMessage));        
    }

    void ShowFinishMessage()
    {
        //Debug.Log("show finish message");
        IndicationOnOff(true);
        SetIndications(textManager.GetText(EnumManager.text.finishMessage));             
    }

    void OnEnable()
    {
        FaceBuilderSceneController.SetIndicationsEvent += ShowIndications;
        FaceBuilderSceneController.FinishAllIndicationsEvent += ShowFinishMessage;
        FaceBuilderSceneController.LoseEvent += ShowLoseMessage;
    }

    void OnDisable()
    {
        FaceBuilderSceneController.SetIndicationsEvent -= ShowIndications;
        FaceBuilderSceneController.FinishAllIndicationsEvent -= ShowFinishMessage;
        FaceBuilderSceneController.LoseEvent -= ShowLoseMessage;
    }
}
