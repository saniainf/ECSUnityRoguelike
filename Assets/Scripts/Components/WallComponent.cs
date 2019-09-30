using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class WallComponent : IEcsAutoResetComponent
    {
        public bool Solid = false;
        public int HealthPoint = 3;
        public bool Damage = false;
        public Sprite DamageSprite;

        void IEcsAutoResetComponent.Reset()
        {
            DamageSprite = null;
        }
    }
}