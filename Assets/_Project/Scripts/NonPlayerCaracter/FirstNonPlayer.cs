using Systems;
using Muramasa.Player;
using UnityEngine;

namespace Muramasa.NonPlayerCharacter
{
    public class FirstNonPlayer : NonPlayerCharacterBase
    {
        [SerializeField] private GameEvent onEnterPedTrigger;
        [SerializeField] private GameEvent onExitPedTrigger;
        
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
    }
}