using Leopotam.Ecs;

namespace Client
{
    sealed class ActionPhaseComponent : IEcsAutoReset
    {
        public bool PhaseEnd = false;

        void IEcsAutoReset.Reset()
        {
            PhaseEnd = false;
        }
    }
}