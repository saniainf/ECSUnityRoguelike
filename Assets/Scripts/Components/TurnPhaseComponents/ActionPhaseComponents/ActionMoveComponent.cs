using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class ActionMoveComponent : IEcsAutoResetComponent
    {
        public Rigidbody2D Rigidbody = null;
        public Vector2Int EndPosition = Vector2Int.zero;
        public float Speed = 0f;

        void IEcsAutoResetComponent.Reset()
        {
            Rigidbody = null;
        }
    }


}