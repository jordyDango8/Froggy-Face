using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region variables
    internal static CameraManager cameraManager;
    
    [SerializeField]
    internal Camera activeCamera;

    [SerializeField]
    internal float cameraSize = 5f;
    #endregion

    void Awake()
    {
        if(cameraManager == null)
        {
            cameraManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    internal void InitializeCamera(Camera _newCamera)
    {
        AssignActiveCamera(_newCamera);
        //activeCamera.GetComponent<CameraController>().UpdateCameraSize();
    }

    internal void AssignActiveCamera(Camera _newCamera)
    {
        activeCamera = _newCamera;        
    }

    public void ChangeCameraSize(float _newChameraSize)
    {
        cameraSize = _newChameraSize;
        activeCamera.GetComponent<CameraController>().UpdateCameraSize();
    }

    /*  
    //ver si se ocupa
    void OnEnable()
    {
        CameraSizeSliderHelper.ChangeCameraSizeEvent += ChangeCameraSize;
    }

    void OnDisable()
    {
        CameraSizeSliderHelper.ChangeCameraSizeEvent += ChangeCameraSize;
    }
    */

    
}
