using Leopotam.Ecs;

namespace Client
{
    sealed class InputPhaseComponent:IEcsAutoResetComponent
    {
        public bool PhaseEnd = false;

        void IEcsAutoResetComponent.Reset()
        {
            PhaseEnd = false;
        }
    }
}