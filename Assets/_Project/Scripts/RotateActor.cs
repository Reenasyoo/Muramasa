using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActor
{
    [SerializeField] private float rotationSpeed;

    private Transform _actorTransform;
    private Transform _mainCameraTransform;

    public RotateActor(Transform actor)
    {
        _actorTransform = actor;
        _mainCameraTransform = CameraManager.Instance.BrainTransform;
    }


    #region Rotation

    public void RotateCharacterFowardToCamera()
    {
        var rotation = _actorTransform.rotation;
        var cameraRotation = _mainCameraTransform.rotation;

        // var distance = 0f;
        // var direction = 0;
        // if (!myApproximation(rotation.y, cameraRotation.y, 0.1f))
        // {
        //     if (rotation.y > cameraRotation.y)
        //     {
        //         distance = rotation.y - cameraRotation.y;
        //         direction = 0;
        //     }
        //     else if ( rotation.y < cameraRotation.y)
        //     {
        //         distance = cameraRotation.y - rotation.y;
        //         direction = 2;
        //     }
        // }
        // else
        // {
        //     direction = 1;
        // }
        // If current camera distane is less than deadzone exit method
        // if (!(distance > characterFolowRotationDeadzone)) return;

        var rotationTarget = Quaternion.Lerp(rotation, cameraRotation, Time.deltaTime * rotationSpeed);

        // Remove x and z rotations so character would only rotate in one axes
        rotationTarget.x = 0;
        rotationTarget.z = 0;

        // Apply modified rotation
        _actorTransform.rotation = rotationTarget;
    }

    #endregion

    private bool Approximation(float a, float b, float tolerance) => (Mathf.Abs(a - b) < tolerance);
}