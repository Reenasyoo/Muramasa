using System;
using UnityEngine;

namespace Muramasa.Movement
{
    public abstract class JumpBase : MonoBehaviour, IGroundable
    {
        [SerializeField] protected KeyCode jumpKey = KeyCode.Space;

        #region Properties

        public bool IsGrounded { get; private set; }

        #endregion

        #region Private fields

        private Collider _collider;
        private GroundCheck _groundCheck;

        #endregion

        protected void Awake()
        {
            _collider = GetComponent<Collider>();

            if (ReferenceEquals(_collider, null)) return;
            _groundCheck = new GroundCheck(_collider);
        }

        protected void FixedUpdate()
        {
            _groundCheck.DebugGrounded();
            IsGrounded = _groundCheck.IsGrounded();
            
            if (Input.GetKeyDown(jumpKey) && IsGrounded)
            {
                Jump();
            }
        }

        protected abstract void Jump();
    }
}