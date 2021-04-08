using System;
using Muramasa.Utilities;
using UnityEngine;

namespace Muramasa.Utilities
{
    public class GameManager : Singleton<GameManager>
    {
        public int FirstPedInteractionCount { get; set; } = 0;

        public bool EnemyKilled { get; set; }
    }
}