//por corregir
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSizeSliderHelper : MonoBehaviour
{
    //internal delegate void ChangeCameraSizeDelegate(float newCameraSize);
    //internal static event ChangeCameraSizeDelegate ChangeCameraSizeEvent;

    [SerializeField]
    CameraManager cameraManager;
    [SerializeField]
    Slider cameraSizeSlider;

    void Start()
    {
        cameraManager = CameraManager.cameraManager;
        cameraSizeSlider = GetComponent<Slider>();
        UpdateSliderValue();
    }

    public void ChangeCameraSize()
    {
        cameraManager.ChangeCameraSize(cameraSizeSlider.value);
        /*
        if(ChangeCameraSizeEvent != null)
        {
            ChangeCameraSizeEvent();
        }
        */
    }
    
    void UpdateSliderValue()
    {
        Debug.Log("update slider value");
        cameraSizeSlider.value = cameraManager.cameraSize;
    }

}
