using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IInputVector
    {
        #region Properties

        public float ForwardVelocity => _velocityVector.z;
        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        } 

        #endregion
        
        #region Fields

        [SerializeField] private float movementSpeed = 100f;
        
        private Rigidbody _rigidbody;
        private Vector3 _velocityVector;
        private Vector3 _targetVelocity;

        private bool _canMove = true;
        
        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Converts transform from local to world space
            
            if (_canMove)
            {
                _targetVelocity = transform.TransformDirection(_velocityVector);
                _targetVelocity = _targetVelocity * (movementSpeed * Time.fixedDeltaTime);
                _targetVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = _targetVelocity;
            }
            
        }

        #region IInputVector

        // Get Horizontal and Vertical inputs
        public void GetInputVector(Vector2 inputVector)
        {
            _velocityVector = new Vector3(inputVector.x, 0, inputVector.y);
            // _velocityVector = new Vector3(inputVector.x, _rigidbody.velocity.y , inputVector.y);
        }

        #endregion
    }
}