using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IInputVector
    {
        #region Properties

        public float ForwardVelocity => _velocityVector.z;
        public bool CanMove { get; set; } = true;

        #endregion
        
        #region Fields

        [SerializeField] private float movementSpeed = 100f;
        
        private Rigidbody _rigidbody;
        private Vector3 _velocityVector;
        private Vector3 _targetVelocity;

        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (CanMove)
            {
                // Converts transform from local to world space
                _targetVelocity = transform.TransformDirection(_velocityVector);
                // Add movement speed to target velocity + delta time
                _targetVelocity *= (movementSpeed * Time.fixedDeltaTime);
                // Remove y velocity so we dont disrupt any other applied gravitys
                _targetVelocity.y = _rigidbody.velocity.y;
                // Apply our target velocity to RigidBody velocity
                _rigidbody.velocity = _targetVelocity;
            }
        }

        #region IInputVector

        // Get Horizontal and Vertical inputs
        public void GetInputVector(Vector2 inputVector) => _velocityVector = new Vector3(inputVector.x, 0, inputVector.y);

        #endregion
    }
}