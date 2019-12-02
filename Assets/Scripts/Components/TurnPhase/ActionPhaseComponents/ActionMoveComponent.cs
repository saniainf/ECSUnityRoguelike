using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class ActionMoveComponent : IEcsAutoReset
    {
        public bool Run = false;
        public Vector2 StartPosition = Vector2.zero;
        public Vector2 GoalPosition = Vector2.zero;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            StartPosition = Vector2.zero;
            GoalPosition = Vector2.zero;
        }
    }


}