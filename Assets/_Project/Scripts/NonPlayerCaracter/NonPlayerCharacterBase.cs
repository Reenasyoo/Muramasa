using System.Collections.Generic;
using UnityEngine;

namespace Muramasa.Utilities.NonPlayerCaracter
{
    public class NonPlayerCharacterBase
    {
        [SerializeField] private List<GameObject> _moveTargets = new List<GameObject>();
        [SerializeField] private bool canMove = false;
    }
}