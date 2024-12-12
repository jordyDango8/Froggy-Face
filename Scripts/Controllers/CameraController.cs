using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region variables
    internal delegate void FinishTiltAction();
    internal static event FinishTiltAction FinishTiltEvent;
    
    [SerializeField]
    CameraManager cameraManager;
    
    [SerializeField]
    Transform target;
    
    [SerializeField]    
    float upperLimitPosY = 5;
    
    [SerializeField]
    float bottomLimitPosY = -5;
    
    Camera myCamera;
    
    [SerializeField]
    Animator animator;
    #endregion

    void Start()
    {        
        Assignments();        
        UpdateCameraSize();
    }

    void LateUpdate()
    {
        if(animator == null || animator.enabled)
        {
            return;
        }

        transform.position = 
        new Vector3(
            transform.position.x,
            Mathf.Clamp(target.position.y, bottomLimitPosY, upperLimitPosY),
            transform.position.z);
    }

    void Assignments()
    {
        cameraManager = CameraManager.cameraManager;
        myCamera = GetComponent<Camera>();

        if (TryGetComponent<Animator>(out Animator animatorT))
        {
            animator = animatorT;
            animator.SetTrigger(EnumManager.animParameters.tilt.ToString());
        }
    }

    void FinishTilt() //called as evet in the animation "cameraTiltUp"
    {
        animator.enabled = false;
        if(FinishTiltEvent != null)
        {
            FinishTiltEvent();
        }
    }

    internal void UpdateCameraSize()
    {
        myCamera.orthographicSize = cameraManager.cameraSize;
    }

}
