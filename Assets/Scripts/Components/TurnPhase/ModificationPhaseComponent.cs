using Leopotam.Ecs;

namespace Client
{
    sealed class ModificationPhaseComponent : IEcsAutoReset
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
