using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ActionAtackComponent : IEcsAutoResetComponent
    {
        public bool Run = false;
        public Vector2Int TargetPosition = Vector2Int.zero;
        public EcsEntity Target;

        void IEcsAutoResetComponent.Reset()
        {
            Run = false;
            TargetPosition = Vector2Int.zero;
        }
    }
}