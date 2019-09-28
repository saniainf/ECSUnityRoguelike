using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class WallComponent : IEcsAutoResetComponent
    {
        public bool Solid = false;
        public int HealthPoint = 3;
        public Sprite DamageSprite;
        public bool Damage = false;

        void IEcsAutoResetComponent.Reset()
        {
            DamageSprite = null;
        }
    }
}