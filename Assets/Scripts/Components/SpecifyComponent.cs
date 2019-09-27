using UnityEngine;

namespace Client
{
    enum MoveDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    enum ActionType
    {
        NONE,
        MOVE,
        ANIMATION
    }

    sealed class SpecifyComponent
    {
        public MoveDirection MoveDirection = MoveDirection.NONE;
        public ActionType ActionType = ActionType.NONE;

        public Vector2Int EndPosition = Vector2Int.zero;
        public float Speed = 0f;

        public int Initiative = 0;
    }
}