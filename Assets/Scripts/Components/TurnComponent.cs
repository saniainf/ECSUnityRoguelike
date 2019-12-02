using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    sealed class TurnComponent : IEcsAutoReset
    {
        public int Queue = 0;
        public bool ReturnInput = false;
        [Obsolete]
        public ActionType ActionType = ActionType.None;
        [Obsolete]
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