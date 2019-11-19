namespace Client
{
    struct NPCDataSheet
    {
        public NPCStats NPCStats;
        public IWeaponItem WeaponItem;

        public NPCDataSheet(NPCStats stats, IWeaponItem weaponItem)
        {
            NPCStats = stats;
            WeaponItem = weaponItem;
        }
    }
}
