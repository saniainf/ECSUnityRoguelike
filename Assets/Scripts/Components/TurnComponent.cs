using Leopotam.Ecs;

namespace Client
{
    sealed class TurnComponent : IEcsAutoReset
    {
        public int Queue = 0;
        public bool ReturnInput = false;

        void IEcsAutoReset.Reset()
        {
            ReturnInput = false;
        }
    }
}