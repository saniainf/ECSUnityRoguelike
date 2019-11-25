using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent : IEcsAutoReset
    {
        public NPCStats Stats;

        public IWeaponItem WeaponItem;

        void IEcsAutoReset.Reset()
        {
            WeaponItem = null;
        }
    }
}