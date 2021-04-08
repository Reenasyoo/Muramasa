using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Muramasa.Utilities;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    #region Properties

    protected CameraManager() {}

    public Transform BrainTransform => _brain.transform;

        #endregion

    #region Fields
    
    [SerializeField] private CinemachineBrain _brain = null;
    
    [SerializeField] private CinemachineVirtualCameraBase _firstPersonCamera = null;
    [SerializeField] private CinemachineVirtualCameraBase _thirdPersonCamera = null;

    #endregion
    
    // Singleton Awake method
    protected override void OnAwake()
    {
        base.OnAwake();
    }

    public void CreateFirstPersonCamera(Transform target)
    {
        if (ReferenceEquals(_firstPersonCamera, null)) return;
        
        // Set FPS cameras follow value to target, in this case set it to player
        _firstPersonCamera.Follow = target;
    }
    
    public void CreateThirdPersonCamera(Transform target)
    {
        if (ReferenceEquals(_thirdPersonCamera, null)) return;
        
        // Set Third Person Camera Follow and LookAt to target
        _thirdPersonCamera.Follow = target;
        _thirdPersonCamera.LookAt = target;
    }
    
}
