using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditInfoHelper : MonoBehaviour
{
    #region variable
    [SerializeField]
    TextMeshProUGUI nameTMP;

    [SerializeField]
    TextMeshProUGUI linkTMP;
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
    //    nameTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    //    linkTMP = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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

    internal void SetInfo(CreditsInfo _newCredit)
    {
        //Debug.Log("setInfo");        
        nameTMP.text = _newCredit.name;
        linkTMP.text = _newCredit.link;
    }

    public void GoToLink()
    {        
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string link = button.GetComponent<TextMeshProUGUI>().text;
        //Debug.Log("link= " + link);
        Application.OpenURL(link);
    }
}
