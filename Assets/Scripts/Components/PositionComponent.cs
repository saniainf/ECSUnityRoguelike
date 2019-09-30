using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class PositionComponent : IEcsAutoResetComponent
    {
        public Vector2Int Coords;
        public Transform Transform = null;
        public Rigidbody2D Rigidbody = null;

        public MoveDirection MoveDirection = MoveDirection.NONE;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
            Rigidbody = null;
        }
    }
}