using Leopotam.Ecs;

namespace Client
{
    sealed class SpellSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<ApplySpellComponent> _spellEnitites = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _spellEnitites)
            {
                var c1 = _spellEnitites.Get1[i];

                if (c1.Spell.StatusEffect != null)
                {
                    ApplyStatusEffect(c1.Target, c1.Spell.StatusEffect);
                }

                _spellEnitites.Entities[i].Destroy();
            }
        }

        void ApplyStatusEffect(EcsEntity entity, StatusEffectPreset preset)
        {
            var data = entity.Get<NPCDataSheetComponent>();

            if (data != null)
            {
                if (data.StatusEffects != null)
                    data.StatusEffects.Add(new StatusEffect { EffectType = preset.StatusType, Value = preset.Value, Time = preset.Time });
            }
        }
    }
}