using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsOneFrame]
    sealed class GameObjectCreateEvent : IEcsAutoResetComponent
    {
        public Transform Transform = null;
        public Rigidbody2D Rigidbody = null;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
            Rigidbody = null;
        }
    }
}