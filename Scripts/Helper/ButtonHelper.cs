using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHelper : MonoBehaviour
{
    //[SerializeField]
    AudioManager audioManager;       

    //[SerializeField]
    Button button; 

    //[SerializeField]
    float waitBeforeEnable = 1.2f;

    [SerializeField]
    bool cancel = false;

    void Awake()
    {
        button = GetComponent<Button>();    
    }

    void Start()
    {
        Assignments();
        button.onClick.AddListener(TaskOnClick);
    }

    void Assignments()
    {
        audioManager = AudioManager.audioManager;           
    }

    void TaskOnClick()
    {
        //Debug.Log("buttonPressed");
        if(cancel)
        {
            audioManager.Play(EnumManager.audios.clicCancel);        
        }
        else
        {
            audioManager.Play(EnumManager.audios.clicConfirm);        
        }
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine(DisableCR());
        }
    }

    IEnumerator DisableCR()
    {
            button.interactable = false;
            yield return new WaitForSecondsRealtime(waitBeforeEnable);

            if(gameObject.activeInHierarchy)
            {
                button.interactable = true;
            }
    }

    void OnEnable()
    {
        button.interactable = true;
    }
}

