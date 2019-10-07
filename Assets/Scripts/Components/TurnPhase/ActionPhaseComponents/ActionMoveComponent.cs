using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class ActionMoveComponent
    {
        public Vector2Int EndPosition = Vector2Int.zero;
        public float Speed = 0f;
    }


}