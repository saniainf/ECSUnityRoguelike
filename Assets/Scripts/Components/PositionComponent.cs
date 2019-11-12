using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class PositionComponent : IEcsAutoResetComponent
    {
        public Vector2 Coords;
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