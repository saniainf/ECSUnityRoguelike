using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    class NPCWeapon
    {
        public int Damage;
        public Sprite ProjectileSprite;
        public EffectPreset HitEffect;
        public WeaponBehaviour Behaviour;

        public NPCWeapon(WeaponItemObject weaponItem, WeaponBehaviour weaponBehaviour)
        {
            this.Behaviour = weaponBehaviour;
            this.Behaviour.Weapon = this;
            HitEffect = weaponItem.HitEffect;
            Damage = weaponItem.Damage;
            ProjectileSprite = weaponItem.ProjectileSprite;
        }
    }
}
