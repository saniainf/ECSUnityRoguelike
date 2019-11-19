using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent : IEcsAutoResetComponent
    {
        public NPCStats Stats;

        public IWeaponItem WeaponItem;

        void IEcsAutoResetComponent.Reset()
        {
            WeaponItem = null;
        }
    }
}