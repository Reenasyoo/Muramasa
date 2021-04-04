using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IInputVector
    {
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
            // Converts transform from local to world space
            _targetVelocity = transform.TransformDirection(_velocityVector);
            _rigidbody.velocity = _targetVelocity * (movementSpeed * Time.fixedDeltaTime);
        }

        #region IInputVector

        // Get Horizontal and Vertical inputs
        public void GetInputVector(Vector2 inputVector)
        {
            // _velocityVector = new Vector3(inputVector.x, _gravity.y, inputVector.y);
            _velocityVector = new Vector3(inputVector.x,_rigidbody.velocity.y, inputVector.y);
        }

        #endregion
    }
}