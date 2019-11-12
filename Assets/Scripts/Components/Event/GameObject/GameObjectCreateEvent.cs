using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsOneFrame]
    sealed class GameObjectCreateEvent : IEcsAutoResetComponent
    {
        public Transform Transform = null;
        public Rigidbody2D Rigidbody = null;
        public BoxCollider2D Collider = null;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
            Rigidbody = null;
            Collider = null;
        }
    }
}