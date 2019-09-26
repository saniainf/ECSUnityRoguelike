namespace Client
{
    enum Phase : int
    {
        STANDBY,
        INPUT,
        ACTION,
    }

    sealed class TurnComponent
    {
        public Phase Phase = Phase.STANDBY;
    }
}