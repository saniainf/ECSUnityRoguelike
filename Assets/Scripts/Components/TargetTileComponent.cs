using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TargetTileComponent : IEcsAutoReset
    {
        public EcsEntity Target = EcsEntity.Null;
        public Vector2 TargetPos = Vector2.zero;
        public AtackType AtackType = AtackType.None;

        void IEcsAutoReset.Reset()
        {
            Target = EcsEntity.Null;
            TargetPos = Vector2.zero;
            AtackType = AtackType.None;
        }
    }
}
