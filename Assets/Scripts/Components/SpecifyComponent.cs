using UnityEngine;

namespace Client
{
    enum MoveDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }

    sealed class SpecifyComponent
    {
        public MoveDirection MoveDirection = MoveDirection.NONE;

        public Vector2 EndPosition = Vector2.zero;
        public float Speed = 0f;

        public int Initiative = 0;
    }
}