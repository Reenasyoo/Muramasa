using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump: JumpBase
    {
        #region Properties
        private float CalculateVerticalSpeed() =>  Mathf.Sqrt(-1 * jumpHeight * _gravity.y / _rigidbody.mass);

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
        
        protected void FixedUpdate()
        {
            base.FixedUpdate();
            // TODO: Could add different types of gravity's
            // TODO: Could make module for applying gravity
            
            if (enableMassGravity)
            {
                // Apply gravity based on mass
                _rigidbody.AddForce(new Vector3(0, _gravity.y * _rigidbody.mass, 0));    
            }
        }

        protected override void Jump()
        {
            Debug.Log(CalculateVerticalSpeed());
            Debug.Log("VAR");
            var velocity = _rigidbody.velocity;
            
            _rigidbody.velocity = new Vector3( 0, CalculateVerticalSpeed(), 0);
            // _rigidbody.AddForce(GLOBALS._UpDirection * CalculateVerticalSpeed());
        }
    }
}