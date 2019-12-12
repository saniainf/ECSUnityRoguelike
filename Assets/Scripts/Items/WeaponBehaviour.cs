using Leopotam.Ecs;

namespace Client
{
    abstract class WeaponBehaviour
    {
        public NPCWeapon Weapon;

        abstract public void OnAtack(EcsEntity caster, EcsEntity target);
    }

    class WeaponEmpty : WeaponBehaviour
    {
        public override void OnAtack(EcsEntity caster, EcsEntity target)
        {

        }
    }

    class WB_DamageOnContact : WeaponBehaviour
    {
        public override void OnAtack(EcsEntity caster, EcsEntity target)
        {
            target.RLApplyDamage(Weapon.Damage);
        }
    }
}