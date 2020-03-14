using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    static class RLEcsEntityExtension
    {
        public static void RLDestoryGO(this EcsEntity entity, float time = 0)
        {
            var c = entity.Get<GameObjectComponent>();
            if (c != null)
                UnityEngine.Object.Destroy(c.Transform.gameObject, time);
            entity.Destroy();
        }

        public static void RLSetHealth(this EcsEntity entity, int value)
        {
            var c = entity.Get<NPCDataSheetComponent>();
            if (c != null)
                c.Stats.HealthPoint = Mathf.Min(value, c.Stats.MaxHealthPoint);
        }

        public static void RLSetMaxHealth(this EcsEntity entity, int value)
        {
            var c = entity.Get<NPCDataSheetComponent>();
            if (c != null)
                c.Stats.MaxHealthPoint = value;
        }

        public static int RLGetHealth(this EcsEntity entity)
        {
            int value = 0;
            var c = entity.Get<NPCDataSheetComponent>();
            if (c != null)
                value = c.Stats.HealthPoint;
            return value;
        }

        public static int RLGetMaxHealth(this EcsEntity entity)
        {
            int value = 0;
            var c = entity.Get<NPCDataSheetComponent>();
            if (c != null)
                value = c.Stats.MaxHealthPoint;
            return value;
        }

        public static void RLApplyDamage(this EcsEntity entity, int value)
        {
            var c = entity.Get<NPCDataSheetComponent>();
            if (c != null)
                c.Stats.HealthPoint -= value;
        }

        public static void RLApplySpell(this EcsEntity entity, SpellPreset preset)
        {
            if (preset.StatusEffect != null)
                entity.RLApplyStatusEffect(preset.StatusEffect);
        }

        public static void RLApplyStatusEffect(this EcsEntity entity, StatusEffectPreset preset)
        {
            var data = entity.Get<NPCDataSheetComponent>();
            data.StatusEffects.Add(new StatusEffect { EffectType = preset.StatusType, Value = preset.Value, Time = preset.Time });
        }
    }
}
