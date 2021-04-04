using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump: JumpBase
    {
        #region Properties
        private float CalculateVerticalSpeed =>  Mathf.Sqrt(-1 * jumpHeight * _gravity.y / _rigidbody.mass);

        #endregion

        #region Fields
        [SerializeField] private bool enableMassGravity = true;
        
        private Rigidbody _rigidbody;

        #endregion
        
        protected void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            // TODO: Could add different types of gravity's
            // TODO: Could make module for applying gravity
            
            if (enableMassGravity || !IsGrounded)
            {
                // Apply gravity based on mass
                _rigidbody.AddForce(new Vector3(0, _gravity.y * _rigidbody.mass, 0));    
            }
        }

        protected override void Jump()
        {
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3( velocity.x, CalculateVerticalSpeed, velocity.z);
        }
    }
}