using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ProjectileComponent : IEcsAutoReset
    {
        public bool Run = false;
        public Vector2 StartPosition = Vector2.zero;
        public Vector2 GoalPosition = Vector2.zero;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            StartPosition = Vector2.zero;
            GoalPosition = Vector2.zero;
        }
    }
}