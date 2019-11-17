using Leopotam.Ecs;

namespace Client
{
    interface IWeaponItem
    {
        void OnAtack(EcsWorld world, EcsEntity caster, EcsEntity target);
    }

    class WeaponItemChopper : IWeaponItem
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

    sealed class WeaponItemComponent : IEcsAutoResetComponent
    {
        public IWeaponItem WeaponItem;

        void IEcsAutoResetComponent.Reset()
        {
            WeaponItem = null;
        }
    }
}