using System;
using Muramasa.Player;
using UnityEngine;

namespace Muramasa.NonPlayerCharacter
{
    public class FirstEnemy : NonPlayerCharacterBase, IActor
    {
        public int Health { get; private set; }

        [SerializeField] private WeaponMono _sword;
        
        private Transform _enemyTransform;
        private bool _hasEnemy;
        private bool _isAttacking;

        protected void Awake()
        {
            base.Awake();
            _sword = GetComponentInChildren<WeaponMono>();
        }
        
        protected void Update()
        {
            if (!ReferenceEquals(_enemyTransform, null))
            {
                if (CheckEndDistance(_enemyTransform.position, 1) && !_isAttacking)
                {
                    Attack();
                }
                else if(_isMoving)
                {
                    _isAttacking = false;
                    _sword.CanDoDamage = false;
                    var position = _enemyTransform.position;
                    LookAtTarget(position);
                    MovePed(position, _settings.movementSpeed);
                }
            }
            if(!_hasEnemy) base.Update();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IActor>() == null) return;
            
            _enemyTransform = other.gameObject.transform;
            SetTargetPosition(_enemyTransform);
            _isMoving = true;
            _hasEnemy = true;
        }

        private void Attack()
        {
            _animationController.PlayAttack(out var attackEnded);
            _isAttacking = true;
            _sword.CanDoDamage = true;
            _isMoving = attackEnded;
            _isAttacking = attackEnded;
        }

        public void TakeDamage(int damage) => Health -= damage;
    }
}