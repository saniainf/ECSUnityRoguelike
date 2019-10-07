using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsOneFrame]
    sealed class CollectEvent : IEcsAutoResetComponent
    {
        public int HealValue = 0;

        void IEcsAutoResetComponent.Reset()
        {
            HealValue = 0;
        }
    }
}