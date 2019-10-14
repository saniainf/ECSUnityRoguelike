using Leopotam.Ecs;

namespace Client
{
    sealed class ActionPhaseComponent : IEcsAutoResetComponent
    {
        public bool PhaseEnd = false;

        void IEcsAutoResetComponent.Reset()
        {
            PhaseEnd = false;
        }
    }
}