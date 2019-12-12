using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    struct NPCWeapon
    {
        public WeaponItemObject PrimaryWeaponItem;
        public IWeaponBehaviour PrimaryWeaponBehaviour;
        public WeaponItemObject SecondaryWeaponItem;
        public IWeaponBehaviour SecondaryWeaponBehaviour;

        public NPCWeapon(WeaponItemObject primaryWeaponItem, IWeaponBehaviour primaryWeaponBeh, WeaponItemObject secondWeaponItemn, IWeaponBehaviour secondWeaponBeh)
        {
            PrimaryWeaponItem = primaryWeaponItem;
            PrimaryWeaponBehaviour = primaryWeaponBeh;
            SecondaryWeaponItem = secondWeaponItemn;
            SecondaryWeaponBehaviour = secondWeaponBeh;
        }
    }
}
