using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/CollectItemPreset", fileName = "CollectItemPreset")]
    class CollectItemObject : ScriptableObject
    {
        public int Value;
        public Sprite Sprite;
    }
}
