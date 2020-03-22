using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    class StatusEffectHandler : ScriptableObject
    {
        public StatusEffectType EffectType;

        public virtual void OnModificatonPhase(EcsEntity entity, StatusEffect effect)
        {

        }

        public virtual void OnTick(EcsEntity entity, StatusEffect effect)
        {
            effect.Time -= 1;
        }
    }
}