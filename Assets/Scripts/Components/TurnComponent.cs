using Leopotam.Ecs;

namespace Client
{
    sealed class TurnComponent : IEcsAutoResetComponent
    {
        public int Queue = 0;
        public bool ReturnInput = false;

        void IEcsAutoResetComponent.Reset()
        {
            ReturnInput = false;
        }
    }
}