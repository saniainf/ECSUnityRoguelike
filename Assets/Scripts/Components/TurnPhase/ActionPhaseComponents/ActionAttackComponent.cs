using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ActionAttackComponent : IEcsAutoReset
    {
        public bool Run = false;
        public bool OnAttack = false;

        public bool PrimaryOrSecondaryWeapon = true;
        public Vector2 TargetPosition = Vector2.zero;
        public EcsEntity Target = EcsEntity.Null;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            OnAttack = false;
            PrimaryOrSecondaryWeapon = true;

            TargetPosition = Vector2.zero;
            Target = EcsEntity.Null;
        }
    }
}