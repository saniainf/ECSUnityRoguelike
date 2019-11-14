using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/ResourcesPresets", fileName = "ResourcesPresets")]
    class ResourcesObjects : ScriptableObject
    {
        public Sprite[] SpriteSheet;
        public GameObject PrefabSprite;
        public GameObject PrefabAnimation;
        public GameObject PrefabPhysicsSprite;
        public GameObject PrefabPhysicsAnimation;
    }
}
