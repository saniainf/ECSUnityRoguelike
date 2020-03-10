using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Spell/Spell", fileName = "SpellPreset")]
    public class SpellPreset : ScriptableObject
    {
        public DirectEffectPreset DirectEffect;
        public ActiveEffectPreset ActiveEffect;
        public StatusEffectPreset StatusEffect;
    }
}
