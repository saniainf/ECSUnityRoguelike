using Leopotam.Ecs;

namespace Client
{
    sealed class EnvironmentPhaseComponent : IEcsAutoReset
    {
        public bool Run = false;
        public bool PhaseEnd = false;

        void IEcsAutoReset.Reset()
        {
            Run = false;
            PhaseEnd = false;
        }
    }
}
