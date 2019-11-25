using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ActionAtackComponent : IEcsAutoReset
    {
        public bool Run = false;
        public bool OnAtack = false;
        public Vector2 TargetPosition = Vector2.zero;
        public EcsEntity Target;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            OnAtack = false;
            TargetPosition = Vector2.zero;
        }
    }
}