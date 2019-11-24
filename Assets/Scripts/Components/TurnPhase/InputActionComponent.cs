using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class InputActionComponent:IEcsAutoResetComponent
    {
        public ActionType InputActionType = ActionType.None;
        public Vector2 GoalPosition = Vector2.zero;
        public bool Skip = false;

        void IEcsAutoResetComponent.Reset()
        {
            InputActionType = ActionType.None;
            GoalPosition = Vector2.zero;
            Skip = false;
        }
    }
}