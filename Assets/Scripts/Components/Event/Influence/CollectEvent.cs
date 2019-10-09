using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsOneFrame]
    sealed class CollectEvent : IEcsAutoResetComponent
    {
        public int HealValue = 0;
        public int BoostHealthValue = 0;

        void IEcsAutoResetComponent.Reset()
        {
            HealValue = 0;
            BoostHealthValue = 0;
        }
    }
}