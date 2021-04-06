using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump: JumpBase
    {
        #region Properties
        // private float CalculateVerticalSpeed() =>  Mathf.Sqrt(-1 * jumpHeight * _gravity.y / _rigidbody.mass);

        #endregion

        #region Fields

        [SerializeField] private float jumpSpeed = 10f;
        [SerializeField] private bool enableMassGravity = true;
        
        private Rigidbody _rigidbody;

        #endregion
        
        protected void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        protected void FixedUpdate()
        {
            base.FixedUpdate();

            // if (enableMassGravity)
            // {
            //     // Apply gravity based on mass
            //     _rigidbody.AddForce(new Vector3(0, _gravity.y * _rigidbody.mass, 0));    
            // }
        }

        protected override void Jump()
        {
            // _rigidbody.velocity = new Vector3( _rigidbody.velocity.x, jumpSpeed, _rigidbody.velocity.z);
            _rigidbody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            // _rigidbody.AddForce(GLOBALS._UpDirection * CalculateVerticalSpeed());
        }
    }
}