namespace Client
{
    /// <summary>
    /// свойства чара (статы, оружие...)
    /// </summary>
    struct NPCDataSheet
    {
        public NPCStats NPCStats;
        public WeaponItemObject PriamryWeapon;
        public WeaponItemObject SecondaryWeapon;

        public NPCDataSheet(NPCStats stats, WeaponItemObject primaryWeapon, WeaponItemObject secondWeapon)
        {
            NPCStats = stats;
            PriamryWeapon = primaryWeapon;
            SecondaryWeapon = secondWeapon;
        }
    }
}
