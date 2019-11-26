using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TurnComponent : IEcsAutoReset
    {
        public int Queue = 0;
        public bool ReturnInput = false;

        public ActionType ActionType = ActionType.None;
        public Vector2 GoalPosition = Vector2.zero;

        public bool SkipTurn = false;

        void IEcsAutoReset.Reset()
        {
            ReturnInput = false;

            ActionType = ActionType.None;
            GoalPosition = Vector2.zero;

            SkipTurn = false;
        }
    }
}