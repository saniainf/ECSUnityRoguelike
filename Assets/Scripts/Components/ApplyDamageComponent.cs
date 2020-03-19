using Leopotam.Ecs;

namespace Client
{
    sealed class ApplyDamageComponent : IEcsAutoReset
    {
        public int DamageValue = 0;
        public EcsEntity Target = EcsEntity.Null;
        public EcsEntity Caster = EcsEntity.Null;

        void IEcsAutoReset.Reset()
        {
            DamageValue = 0;
            Target = EcsEntity.Null;
            Caster = EcsEntity.Null;
        }
    }
}