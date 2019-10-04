using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsOneFrame]
    sealed class ImpactEvent : IEcsAutoResetComponent
    {
        public int HitValue = 0;

        void IEcsAutoResetComponent.Reset()
        {
            HitValue = 0;
        }
    }
}