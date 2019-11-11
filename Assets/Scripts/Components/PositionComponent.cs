using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class PositionComponent : IEcsAutoResetComponent
    {
        public Vector2 Coords;
        public Transform Transform = null;
        public Rigidbody2D Rigidbody = null;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
            Rigidbody = null;
        }
    }
}