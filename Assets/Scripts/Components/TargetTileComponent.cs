using Leopotam.Ecs;

namespace Client
{
    sealed class TargetTileComponent : IEcsAutoReset
    {
        public EcsEntity Target = EcsEntity.Null;
        public AtackType AtackType = AtackType.None;

        void IEcsAutoReset.Reset()
        {
            Target = EcsEntity.Null;
            AtackType = AtackType.None;
        }
    }
}
