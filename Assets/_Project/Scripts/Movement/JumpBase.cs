using System;
using UnityEngine;

namespace Muramasa.Movement
{
    public abstract class JumpBase : MonoBehaviour, IGroundable
    {
        [SerializeField] protected float jumpHeight;
        [SerializeField] protected KeyCode jumpKey = KeyCode.Space;

        #region Properties

        public bool IsGrounded { get; private set; }

        #endregion

        #region Private fields

        protected Collider _collider;
        protected GroundCheck _groundCheck;
        protected readonly Vector3 _gravity = Physics.gravity;

        #endregion

        protected void Awake()
        {
            _collider = GetComponent<Collider>();

            if (ReferenceEquals(_collider, null)) return;
            _groundCheck = new GroundCheck(_collider);
        }

        private void Update()
        {
            if (!IsGrounded)
            {
                IsGrounded = _groundCheck.CheckGrounded();
            }
            
            // TODO: Add different type input
            if (Input.GetKeyDown(jumpKey) && IsGrounded)
            {
                Jump();
            }
        }

        protected abstract void Jump();

    }
}