namespace Client
{
    enum Phase : int
    {
        STANDBY,
        INPUT,
        ACTION,
        TURNEND
    }

    sealed class TurnComponent
    {
        public Phase Phase = Phase.INPUT;
    }
}