using Leopotam.Ecs;

namespace Client
{
    sealed class ApplySpellComponent : IEcsAutoReset
    {
        public EcsEntity Target = EcsEntity.Null;
        public EcsEntity Caster = EcsEntity.Null;
        public SpellPreset Spell;

        public void Reset()
        {
            Target = EcsEntity.Null;
            Caster = EcsEntity.Null;
            Spell = null;
        }
    }
}