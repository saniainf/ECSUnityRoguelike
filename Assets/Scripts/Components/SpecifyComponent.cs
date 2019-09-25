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

    enum Status
    {
        STANDBY,
        INPUT,
        ACTION,
        TURNEND
    }

    sealed class SpecifyComponent
    {
        public MoveDirection MoveDirection = MoveDirection.NONE;
        public Status Status = Status.STANDBY;
        public Vector2 StartPosition = Vector2.zero;
        public Vector2 EndPosition = Vector2.zero;
        public float Speed = 0f;
        public Vector2 Direction = Vector2.zero;
        public int Initiative = 0;
    }
}