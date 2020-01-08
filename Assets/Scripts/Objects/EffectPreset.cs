using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Effect", fileName = "EffectPreset")]
    class EffectPreset : ScriptableObject
    {
        public Sprite[] spritesArray;
        public Sprite spriteSingle;
        public float duration;
    }
}
