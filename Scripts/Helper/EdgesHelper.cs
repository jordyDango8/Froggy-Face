//por revisar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgesHelper : MonoBehaviour
{
    [SerializeField]
    CameraManager cameraManager;

    void Start()
    {
        cameraManager = CameraManager.cameraManager;
    }

    void LateUpdate()
    {
        transform.position = 
         new Vector3(transform.position.x, 
         cameraManager.activeCamera.transform.position.y,
         transform.position.z);
    }
}
