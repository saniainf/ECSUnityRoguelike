using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/Presets/Entities/CollectingItem", fileName = "CollectingItemPreset")]
    class CollectingItemPreset : ScriptableObject
    {
        public GameObjectPreset GameObject;
        public BuffPreset Buff;
    }
}
