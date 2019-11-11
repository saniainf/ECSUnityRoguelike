using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class ActionMoveComponent : IEcsAutoResetComponent
    {
        public bool Run = false;
        public Vector2 StartPosition = Vector2.zero;
        public Vector2Int GoalInt = Vector2Int.zero;
        public Vector2 GoalFloat = Vector2.zero;
        public Vector2 GoalDirection = Vector2.zero;
        public float SqrDistance = 0f;
        public float DestroyDistance = 3f;
        public float Speed = 0f;

        void IEcsAutoResetComponent.Reset()
        {
            Run = false;
            StartPosition = Vector2.zero;
            GoalInt = Vector2Int.zero;
            GoalFloat = Vector2.zero;
            GoalDirection = Vector2.zero;
            SqrDistance = 0f;
            DestroyDistance = 3f;
            Speed = 0f;
        }
    }


}