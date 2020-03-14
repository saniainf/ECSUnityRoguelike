using Leopotam.Ecs;
using System.Collections.Generic;

namespace Client
{
    sealed class NPCDataSheetComponent : IEcsAutoReset
    {
        public NPCStats Stats;

        public NPCWeapon PrimaryWeapon;
        public NPCWeapon SecondaryWeapon;
        public List<StatusEffect> StatusEffects;

        void IEcsAutoReset.Reset()
        {
            PrimaryWeapon = null;
            SecondaryWeapon = null;
            StatusEffects = null;
        }
    }
}