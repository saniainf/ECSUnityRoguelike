using Leopotam.Ecs;

namespace Client
{
    sealed class DamageComponent : IEcsAutoReset
    {
        public int damageValue = 0;
        public EcsEntity target = EcsEntity.Null;

        void IEcsAutoReset.Reset()
        {
            damageValue = 0;
            target = EcsEntity.Null;
        }
    }
}