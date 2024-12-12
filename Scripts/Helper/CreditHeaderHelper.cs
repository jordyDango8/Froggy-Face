using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditHeaderHelper : MonoBehaviour
{
    #region variable
    [SerializeField]
    TextMeshProUGUI headerTMP;
    #endregion

    //void Awake()
    //{
    //    Assignments(); //here because SetInfo is called from CreditsHelper at Start
    //}

    void Update()
    {
        //transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        //Debug.Log("localPos= " + transform.localPosition);
        //Debug.Log("globalPos= " + transform.position);
    }

    //void Assignments()
    //{
    //    headerTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //}

    internal void SetPosition(Vector3 _newPos)
    {
        //transform.position = _newPos;
        //Debug.Log("father= " + transform.parent);
        transform.localPosition = _newPos;
    }

    internal void SetScale(Vector3 _newScale)    
    {
        transform.localScale = _newScale;        
    }

    internal void SetInfo(string _newHeader)
    {
        //Debug.Log("setInfo");
        headerTMP.text = _newHeader;
    }

}
