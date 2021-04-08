using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActor
{
    #region Fields
    
    private readonly Transform _actorTransform;
    private readonly Transform _mainCameraTransform;
    private readonly float _rotationSpeed;

    #endregion


    public RotateActor(Transform actor, float rotationSpeed = 5f)
    {
        _actorTransform = actor;
        
        _rotationSpeed = rotationSpeed;
        
        if(ReferenceEquals(CameraManager.Instance.BrainTransform, null)) return;
        
        _mainCameraTransform = CameraManager.Instance.BrainTransform;
    }


    #region Rotation

    public void RotateCharacterForwardToCamera()
    {
        var rotation = _actorTransform.rotation;
        var cameraRotation = _mainCameraTransform.rotation;

        var rotationTarget = Quaternion.Lerp(rotation, cameraRotation, Time.deltaTime * _rotationSpeed);

        // Remove x and z rotations so character would only rotate in one axes
        rotationTarget.x = 0;
        rotationTarget.z = 0;

        // Apply modified rotation
        _actorTransform.rotation = rotationTarget;
    }

    #endregion
}