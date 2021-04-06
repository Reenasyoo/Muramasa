using System;
using Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace Muramasa.Player
{
    public class PlayerFacade : MonoBehaviour, IActor
    {
        [SerializeField] private KeyCode _interactionKey = KeyCode.E;

        [FormerlySerializedAs("OnDialogActivated")]
        [Header("Events")] 
        [SerializeField] private GameEvent onDialogActivated = null;
        
        private bool _canInteract = false;
        
        public void CanInteractWithPed() =>_canInteract = true;
        public void StopInteractionWithPed() => _canInteract = false;
        
        
        private void Update()
        {
            if (_canInteract && Input.GetKeyDown(_interactionKey))
            {
                onDialogActivated.Raise();
            }
        }
    }
}