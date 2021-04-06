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
        
        private Coroutine startRouteCoroutine;
        
        #endregion

        #region Awake

        private void Awake()
        {
            _transform = transform;

            foreach (var point in _targetPoints)
            {
                // if (_moveTargets.Contains(point.position)) break;
                
                _moveTargets.Add(point.position);
            }

            if (Vector3.Distance(_transform.position, _moveTargets[0]) <= 0.01f
                && _moveTargets.Count > 0)
            {
                _targetIndex = 1;
            }

            if (_moveOnStart) StartRoute();
        }

        #endregion

        private void StartRoute()
        {
            SetTargetPosition(_targetIndex);
            startRouteCoroutine = StartCoroutine(MovePed(_currentTargetPoint, _settings.movementSpeed));
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

        private IEnumerator MovePed(Vector3 endPos, float time)
        {
            if (endPos.Equals(GLOBALS._ZeroVector)) yield break;
            if ( !_hasTarget) yield break;

            _isMoving = true;
            var i = 0.0f;
            var rate = 1.0f / time;
            while (i < 1.0f && _hasTarget)
            {
                i += Time.deltaTime * rate;
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(endPos.x, _transform.position.y, endPos.z), i);
                yield return null;
            }

            Reset();
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

        private void LookAtTarget(Vector3 target) =>_transform.LookAt(target);
        

        private void OnTriggerEnter(Collider other)
        {
            if (ReferenceEquals(other.GetComponent<IActor>(), null)) return;
            
            StopCoroutine(startRouteCoroutine);
            LookAtTarget(other.transform.position);
            // onEnterPedTrigger.Raise();


        }

        private void OnTriggerExit(Collider other)
        {
            if (ReferenceEquals(other.GetComponent<IActor>(), null)) return;
            
            StartRoute();
            LookAtTarget(_currentTargetPoint);
            // onExitPedTrigger.Raise();
        }


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