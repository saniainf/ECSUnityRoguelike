using Leopotam.Ecs;

namespace Client
{
    interface IWeaponBehaviour
    {
        void OnAtack(EcsEntity caster, EcsEntity target);
    }

    struct WeaponEmpty : IWeaponBehaviour
    {
        void IWeaponBehaviour.OnAtack(EcsEntity caster, EcsEntity target)
        {
            
        }
    }

    struct WeaponItemChopper : IWeaponBehaviour
    {
        private int damage;

        public WeaponItemChopper(int damage)
        {
            this.damage = damage;
        }

        void IWeaponBehaviour.OnAtack(EcsEntity caster, EcsEntity target)
        {
            target.RLApplyDamage(damage);
        }
    }

    struct WeaponItemStone : IWeaponBehaviour
    {
        private int damage;

        public WeaponItemStone(int damage)
        {
            this.damage = damage;
        }

        void IWeaponBehaviour.OnAtack(EcsEntity caster, EcsEntity target)
        {
            
        }
    }
}