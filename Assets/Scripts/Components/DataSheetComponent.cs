using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent : IEcsAutoReset
    {
        public NPCStats Stats;

        public WeaponItemObject PrimaryWeaponItem;
        public WeaponItemObject SecondaryWeaponItem;

        void IEcsAutoReset.Reset()
        {
            PrimaryWeaponItem = null;
            SecondaryWeaponItem = null;
        }
    }
}