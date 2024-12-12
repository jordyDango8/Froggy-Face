using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsInfoHelper : MonoBehaviour
{
    string[] info;
    
    //int columns = 0;

    //int infoIndex = 0;



    internal string[] GetInfoFromExcel(TextAsset _excelDoc)
    {     
        //info = _excelDoc.text.Split(new string[] {";"}, System.StringSplitOptions.None);   
        info = _excelDoc.text.Split(new string[] {","}, System.StringSplitOptions.None);   

        //columns = int.Parse(info[0]);
        //Debug.Log("columns= " + columns);
        //Debug.Log("language= " + info[1]);
        //Debug.Log("first Credit is in " + (columns * 2) + 1);

        //infoIndex = 0;
        //foreach(string text in info)
        //{            
        //    Debug.Log(infoIndex + " = " + text);
        //    infoIndex += 1; 
        //}
        return info;   
    }

}
