namespace Client
{
    sealed class TurnComponent
    {
        public int Initiative = 0;
        public Phase Phase = Phase.STANDBY;
        public bool ReturnInput = false;
    }
}