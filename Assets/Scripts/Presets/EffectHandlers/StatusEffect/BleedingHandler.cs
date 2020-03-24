using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/EffectHandler/BleedingHandler", fileName = "BleedingHandler")]
    class BleedingHandler : StatusEffectHandler
    {
        public override void OnModificatonPhase(EcsWorld world, EcsEntity entity, StatusEffect effect)
        {
            world.RLApplyDamage(entity, entity, effect.Value);
        }
    }
}
