using Leopotam.Ecs;

namespace Client
{
    sealed class ZoneExitComponent : IEcsAutoResetComponent
    {
        public bool ZoneStepOn = false;

        void IEcsAutoResetComponent.Reset()
        {
            ZoneStepOn = false;
        }
    }
}