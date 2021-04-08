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

        [SerializeField] private EntitySettings _settings;
        [SerializeField] private ActorAnimationController _animationController;

        [SerializeField] private Transform[] _targetPoints;

        [SerializeField] private bool _moveOnStart = false;
        [SerializeField] private bool _doLoop;

        [SerializeField] private GameEvent onEnterPedTrigger;
        [SerializeField] private GameEvent onExitPedTrigger;

        private Transform _transform;

        private List<Vector3> _moveTargets = new List<Vector3>();
        private Vector3 _currentTargetPoint;

        private int _targetIndex = 0;

        private bool _isMoving;
        private bool _hasTarget;

        #endregion

        #region Awake

        private void Awake()
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

        private void Update()
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

        private void StartRoute()
        {
            SetTargetPosition(_targetIndex);
            LookAtTarget(_currentTargetPoint);
            _hasTarget = true;
        }

        private void SetTargetPosition(Transform targetPos)
        {
            _currentTargetPoint = targetPos.position;
            _hasTarget = true;
        }

        private void SetTargetPosition(int index)
        {
            if (_moveTargets.Count < index) return;

            _currentTargetPoint = _moveTargets[index];
            _hasTarget = true;
        }

        private void MovePed(Vector3 endPoint, float moveSpeed)
        {
            if (endPoint.Equals(GLOBALS._ZeroVector)) return;
            if (!_hasTarget) return;
            
            _isMoving = true;
            
            if (!ReferenceEquals(_animationController, null))
            {
                _animationController.SetForwardVelocity(1);    
            }
            
            var slerp = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPoint, slerp);
        }

        private void NextDestination()
        {
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

        private void LookAtTarget(Vector3 target) => _transform.LookAt(target);

        private bool CheckEndDistance(Vector3 endPoint) => Vector3.Distance(_transform.position, endPoint) <= 0.01f;

        private void OnTriggerStay(Collider other)
        {
            if (ReferenceEquals(other.GetComponent<IActor>(), null)) return;

            _hasTarget = false;
            LookAtTarget(other.transform.position);
            onEnterPedTrigger.Raise();
        }

        private void OnTriggerExit(Collider other)
        {
            if (ReferenceEquals(other.GetComponent<IActor>(), null)) return;

            StartRoute();
            onExitPedTrigger.Raise();
        }


        #region Reset

        private void Reset()
        {
            Debug.Log("Reset");
            _hasTarget = false;
            _isMoving = false;

            if (!_doLoop) return;

            NextDestination();
        }

        #endregion
    }
}