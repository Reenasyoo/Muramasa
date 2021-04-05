using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFacade : MonoBehaviour
{
    [SerializeField] private Transform _headPivotPoint;


    private RotateActor _rotateActor;
    
    private void Awake()
    {
        CameraManager.Instance.CreateFirstPersonCamera(_headPivotPoint);
        // CameraManager.Instance.CreateThirdPersonCamera(transform);

        _rotateActor = new RotateActor(transform);
    }

    private void Update()
    {
        _rotateActor.RotateCharacterFowardToCamera();
    }
}
