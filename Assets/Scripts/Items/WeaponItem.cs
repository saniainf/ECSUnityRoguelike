using Leopotam.Ecs;

namespace Client
{
    interface IWeaponItem
    {
        void OnAtack(EcsEntity caster, EcsEntity target);
    }

    struct WeaponItemChopper : IWeaponItem
    {
        private int damage;

        public WeaponItemChopper(int damage)
        {
            this.damage = damage;
        }

        void IWeaponItem.OnAtack(EcsEntity caster, EcsEntity target)
        {
            target.RLApplyDamage(damage);
        }
    }
}