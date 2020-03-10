using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Spell/StatusEffect", fileName = "StatusEffectPreset")]
    public class StatusEffectPreset : ScriptableObject
    {
        public StatusEffectType StatusType;
        public float Value;
        public int Turn;
    }
}
