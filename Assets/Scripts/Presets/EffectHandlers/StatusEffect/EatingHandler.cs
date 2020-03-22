using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/EffectHandler/EatingHandler", fileName = "EatingHandler")]
    class EatingHandler : StatusEffectHandler
    {
        public override void OnModificatonPhase(EcsEntity entity, StatusEffect effect)
        {
            entity.RLSetHealth(entity.RLGetHealth() + effect.Value);
        }
    }
}