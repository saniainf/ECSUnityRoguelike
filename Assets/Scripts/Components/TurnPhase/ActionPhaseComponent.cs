using Leopotam.Ecs;

namespace Client
{
    sealed class ActionPhaseComponent : IEcsAutoReset
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