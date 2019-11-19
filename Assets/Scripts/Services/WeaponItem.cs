using Leopotam.Ecs;

namespace Client
{
    interface IWeaponItem
    {
        void OnAtack(EcsWorld world, EcsEntity caster, EcsEntity target);
    }

    struct WeaponItemChopper : IWeaponItem
    {
        private int damage;

        public WeaponItemChopper(int damage)
        {
            this.damage = damage;
        }

        void IWeaponItem.OnAtack(EcsWorld world, EcsEntity caster, EcsEntity target)
        {
            world.RLApplyDamage(target, damage);
        }
    }
}