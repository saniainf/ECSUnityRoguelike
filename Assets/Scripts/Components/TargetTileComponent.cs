using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TargetTileComponent : IEcsAutoReset
    {
        public EcsEntity Target = EcsEntity.Null;
        public Vector2 TargetPos = Vector2.zero;
        public AttackType AttackType = AttackType.None;

        void IEcsAutoReset.Reset()
        {
            Target = EcsEntity.Null;
            TargetPos = Vector2.zero;
            AttackType = AttackType.None;
        }
    }
}
