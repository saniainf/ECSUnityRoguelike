namespace Client
{
    /// <summary>
    /// свойства чара (статы, оружие, etc...)
    /// </summary>
    class NPCDataSheet
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
