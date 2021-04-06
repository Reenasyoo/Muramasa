using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    public class GroundCheck 
    {
        private readonly Collider _collider;
        private readonly float _distanceToGround;
        private const float _RADIUS = 0.2f;
        private Vector3 _origin => _collider.transform.position - (GLOBALS._UpDirection * _distanceToGround);

        public GroundCheck(Collider collider)
        {
            _collider = collider;
            // _distanceToGround = _collider.bounds.extents.y;
            _distanceToGround = 0.5f;
            // Debug.Log(_distanceToGround);
            
        }

        public bool IsGrounded()
        {
            return Physics.SphereCast(_origin, _RADIUS, GLOBALS._DownDirection, out var raycastHit, _distanceToGround);   
        }

        public void DebugGrounded()
        {
            Debug.DrawRay(_origin, GLOBALS._DownDirection, Color.red);
        }
    }
}