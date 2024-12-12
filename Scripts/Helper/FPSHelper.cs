using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSHelper : MonoBehaviour
{   
    [SerializeField]  
    TextMeshProUGUI fpsTMP;
    
    float deltaTime;

    int targetFrameRate = 60;

     void Start()
     {
        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = targetFrameRate;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;
        fpsTMP.enabled = false;
     }
 
     void Update () 
     {
        //if(Application.targetFrameRate != targetFrameRate)
        if(Application.targetFrameRate < targetFrameRate)
        {
           Application.targetFrameRate = targetFrameRate;
        }      

        if(Application.targetFrameRate < targetFrameRate / 2f)
        {
           Debug.Log("muy lento");
        }

         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         fpsTMP.text = "FPS= " + Mathf.Ceil (fps).ToString ();
     }

}
