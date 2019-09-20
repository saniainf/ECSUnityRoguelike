using UnityEngine;

namespace Client
{
    enum MoveDirection
    {
        TOP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }

    sealed class Action
    {
        public MoveDirection MoveDirection = MoveDirection.NONE;
        public Vector2 StartPosition = Vector2.zero;
        public Vector2 EndPosition = Vector2.zero;
        public float Speed = 0f;
        public Vector2 Direction = Vector2.zero;
        public bool ActionRun = false;
    }
}