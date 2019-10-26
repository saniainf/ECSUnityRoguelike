using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/SpritesPreset", fileName = "SpritesPreset")]
    class SpritesObject : ScriptableObject
    {
        public Sprite[] spritesArray;
        public Sprite spriteSingle;
    }
}
