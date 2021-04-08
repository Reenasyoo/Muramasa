using System;
using Systems;
using Muramasa.Movement;
using UnityEngine;

namespace Muramasa.Player
{
    public class PlayerFacade : MonoBehaviour, IActor
    {
        [SerializeField] private Transform _headPivotPoint;
        [SerializeField] private Transform _swordPivotPoint;
        [SerializeField] private ActorAnimationController _animationController;


        private RigidbodyMovement _rigidbodyMovement; // TODO: Should make interface form velocity change detection
        private RotateActor _rotateActor;
        
        [Header("Keys")]
        [SerializeField] private KeyCode _interactionKey = KeyCode.E;
        [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;

        [Header("Events")] 
        [SerializeField] private GameEvent onDialogActivated = null;
        
        private bool _canInteract = false;
        

        private void Awake()
        {
            CameraManager.Instance.CreateFirstPersonCamera(_headPivotPoint);
            
            _rigidbodyMovement = GetComponent<RigidbodyMovement>();
            _rotateActor = new RotateActor(transform);
        }


        private void Update()
        {
            _rotateActor.RotateCharacterForwardToCamera();
            _animationController.SetForwardVelocity(_rigidbodyMovement.ForwardVelocity);
            
            
            if (_canInteract && Input.GetKeyDown(_interactionKey))
            {
                onDialogActivated.Raise();
            }

            if (Input.GetKeyDown(_attackKey))
            {
                _rigidbodyMovement.CanMove = false;
                _animationController.PlayAttack(_rigidbodyMovement);
            }
        }

        private void PickupSword(GameObject sword)
        {
            if(ReferenceEquals(_swordPivotPoint, null)) return;
            
            // Add sword to hand
            sword.transform.parent = _swordPivotPoint;
        }
        
        public void CanInteractWithPed() =>_canInteract = true;
        public void StopInteractionWithPed() => _canInteract = false;

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sword"))
            {
                PickupSword(other.gameObject);
            }
        }
    }
}