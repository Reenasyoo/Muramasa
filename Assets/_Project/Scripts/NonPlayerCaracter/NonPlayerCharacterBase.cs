using System;
using System.Collections;
using System.Collections.Generic;
using Systems;
using Muramasa.Player;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.NonPlayerCharacter
{
    public class NonPlayerCharacterBase : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected EntitySettings _settings;
        [SerializeField] protected ActorAnimationController _animationController;

        [SerializeField] private Transform[] _targetPoints;

        [SerializeField] private bool _moveOnStart = false;
        [SerializeField] private bool _doLoop;



        private Transform _transform;

        private List<Vector3> _moveTargets = new List<Vector3>();
        private Vector3 _currentTargetPoint;

        private int _targetIndex = 0;

        protected bool _isMoving;
        protected bool _hasTarget;

        #endregion

        #region Awake

        protected void Awake()
        {
            _transform = transform;

            if (_targetPoints.Length > 1 )
            {
                foreach (var point in _targetPoints)
                {
                    if (point != null)
                    {
                        _moveTargets.Add(point.position);    
                    }
                }

                if (_moveTargets.Count <= 0) return;
                
                if (CheckEndDistance(_moveTargets[0]) && _moveTargets.Count > 0) _targetIndex = 1;
                if (_moveOnStart) StartRoute();
            }
        }

        protected void Update()
        {
            if (CheckEndDistance(_currentTargetPoint))
            {
                Reset();
            }
            else
            {
                MovePed(_currentTargetPoint, _settings.movementSpeed);
            }
        }

        #endregion

        protected void StartRoute()
        {
            SetTargetPosition(_targetIndex);
            LookAtTarget(_currentTargetPoint);
            _hasTarget = true;
        }

        protected void SetTargetPosition(Transform targetPos)
        {
            _currentTargetPoint = targetPos.position;
            _hasTarget = true;
        }

        protected void SetTargetPosition(int index)
        {
            if (_moveTargets.Count <= index) return;

            _currentTargetPoint = _moveTargets[index];
            _hasTarget = true;
        }

        protected void MovePed(Vector3 endPoint, float moveSpeed)
        {
            if (endPoint.Equals(GLOBALS._ZeroVector) || ReferenceEquals(endPoint, null)) return;
            if (!_hasTarget) return;
            
            _isMoving = true;
            
            if (!ReferenceEquals(_animationController, null))
            {
                _animationController.SetForwardVelocity(0.5f);    
            }
            
            var slerp = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPoint, slerp);
        }

        private void NextDestination()
        {
            if (_moveTargets.Count <= 0) return;
            if (_moveTargets.Count > _targetIndex + 1)
            {
                _targetIndex++;
            }
            else
            {
                _targetIndex = 0;
            }

            StartRoute();
        }

        protected void LookAtTarget(Vector3 target) => _transform.LookAt(target);

        protected bool CheckEndDistance(Vector3 endPoint, float distance = 0.01f) => Vector3.Distance(_transform.position, endPoint) <= distance;

        #region Reset

        private void Reset()
        {
            _hasTarget = false;
            _isMoving = false;

            if (!_doLoop) return;

            NextDestination();
        }

        #endregion
    }
}