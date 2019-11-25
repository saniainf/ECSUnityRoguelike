using Leopotam.Ecs;

namespace Client
{
    sealed class InputPhaseComponent:IEcsAutoReset
    {
        public bool PhaseEnd = false;

        void IEcsAutoReset.Reset()
        {
            PhaseEnd = false;
        }
    }
}