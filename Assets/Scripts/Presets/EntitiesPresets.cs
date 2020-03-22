using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/EntitiesPresets", fileName = "EntitiesPresets")]
    class EntitiesPresets : ScriptableObject
    {
        public List<LevelTilePreset> LevelTiles;
        public PlayerPreset Player;
        public List<CollectingItemPreset> CollectingItems;
        public List<StatusEffectHandler> StatusEffectHandlers;
    }
}
