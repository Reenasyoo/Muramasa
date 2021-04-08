using System;
using UnityEngine;

namespace Muramasa.Player
{
    public class WeaponMono : MonoBehaviour
    {
        public bool CanDoDamage { get; set; }

        [SerializeField] private int _damage = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IActor>() != null && CanDoDamage)
            {
                other.GetComponent<IActor>().TakeDamage(_damage);
            }
        }
    }
}