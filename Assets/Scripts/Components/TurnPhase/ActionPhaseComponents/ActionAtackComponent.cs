using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ActionAtackComponent : IEcsAutoReset
    {
        public bool Run = false;
        public bool OnAtack = false;

        public bool PrimaryOrSecondaryWeapon = true;
        public Vector2 TargetPosition = Vector2.zero;
        public EcsEntity Target = EcsEntity.Null;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            OnAtack = false;
            PrimaryOrSecondaryWeapon = true;

            TargetPosition = Vector2.zero;
            Target = EcsEntity.Null;
        }
    }
}