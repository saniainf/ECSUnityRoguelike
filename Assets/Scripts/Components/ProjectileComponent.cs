using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ProjectileComponent : IEcsAutoReset
    {
        public bool Run = false;

        public Vector2 StartPosition = Vector2.zero;
        public Vector2 GoalPosition = Vector2.zero;

        public NPCWeapon Weapon = null;
        public EcsEntity Caster = EcsEntity.Null;
        public EcsEntity Target = EcsEntity.Null;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            StartPosition = Vector2.zero;
            GoalPosition = Vector2.zero;

            Weapon = null;
            Caster = EcsEntity.Null;
            Target = EcsEntity.Null;
        }
    }
}