using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump: JumpBase
    {
        #region Properties

        #endregion

        #region Fields

        [SerializeField] private float jumpSpeed = 10f;

        private Rigidbody _rigidbody;

        #endregion
        
        protected void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected override void Jump()
        {
            _rigidbody.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
    }
}