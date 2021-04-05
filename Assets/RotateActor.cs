using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActor : MonoBehaviour
{
            #region Rotation
        
        private void RotateCharacterFowardToCamera()
        {
            // // TODO: Add switch for different rotation sizes
            var distance = 0f;
            var direction = 0;
            var rotation = transform.rotation;
            var cameraRotation = mainCamera.rotation;


            if (!myApproximation(rotation.y, cameraRotation.y, 0.1f))
            {
                if (rotation.y > cameraRotation.y)
                {
                    distance = rotation.y - cameraRotation.y;
                    direction = 0;
            
                }
                else if ( rotation.y < cameraRotation.y)
                {
            
                    distance = cameraRotation.y - rotation.y;
                    direction = 2;
                }
            }
            else
            {
            
                direction = 1;
            }
            

            // If current camera distane is less than deadzone exit method
            // if (!(distance > characterFolowRotationDeadzone)) return;
            
            var rotationTarget =
                Quaternion.Lerp(rotation, cameraRotation, Time.deltaTime * rotationSpeed);
                
            // Remove x and z rotations so character would only rotate in one axes
            rotationTarget.x = 0;
            rotationTarget.z = 0;
                
            // Apply modified rotation
            transform.rotation = rotationTarget;
            
            // Set animation state
            playerContainer.AnimationController.SetCharacterRotationY(direction);
        }
        
        #endregion
        
        private bool myApproximation(float a, float b, float tolerance)
        {
            return (Mathf.Abs(a - b) < tolerance);
        }
}
