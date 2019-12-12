namespace Client
{
    /// <summary>
    /// свойства чара (статы, оружие...)
    /// </summary>
    struct NPCDataSheet
    {
        public NPCStats NPCStats;
        public NPCWeapon PriamaryWeapon;
        public NPCWeapon SecondaryWeapon;

        public NPCDataSheet(NPCStats stats, NPCWeapon primaryWeapon, NPCWeapon secondWeapon)
        {
            NPCStats = stats;
            PriamaryWeapon = primaryWeapon;
            SecondaryWeapon = secondWeapon;
        }
    }
}
