using Leopotam.Ecs;

namespace Client
{
    sealed class DataSheetComponent : IEcsAutoReset
    {
        public NPCStats Stats;

        public NPCWeapon PrimaryWeapon;
        public NPCWeapon SecondaryWeapon;

        void IEcsAutoReset.Reset()
        {
            PrimaryWeapon = null;
            SecondaryWeapon = null;
        }
    }
}