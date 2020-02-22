
using System.Collections.Generic;
using UnityEngine;

namespace Client.Objects
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Spell", fileName = "SpellPreset")]
    class SpellPreset : ScriptableObject
    {
        public string Name;

        public List<BuffPreset> Buffs;
        public List<EffectPreset> Effects;
    }
}
