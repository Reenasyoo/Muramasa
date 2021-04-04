using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Movement
{
    public class GroundCheck 
    {
        private readonly Collider _collider;
        private readonly float _distanceToGround;
        private const float _RADIUS = 0.2f;

        public GroundCheck(Collider collider)
        {
            _collider = collider;
            _distanceToGround = _collider.bounds.extents.y;
        }

        public bool CheckGrounded()
        {
            var origin = _collider.transform.position - (GLOBALS._UpDirection * _distanceToGround);
            // Debug.DrawRay(_center, GLOBALS._DownDirection * _distanceToGround, Color.red);
            return Physics.SphereCast(origin, _RADIUS, GLOBALS._DownDirection, out var raycastHit, _distanceToGround);
        }
    }
}