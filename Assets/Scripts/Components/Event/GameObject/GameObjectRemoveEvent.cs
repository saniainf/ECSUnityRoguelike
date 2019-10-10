using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectRemoveEvent : IEcsAutoResetComponent
    {
        public float RemoveTime = 0f;

        void IEcsAutoResetComponent.Reset()
        {
            RemoveTime = 0f;
        }
    }
}