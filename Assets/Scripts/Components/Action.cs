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
    }
}