using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/WorldObjectsPresets", fileName = "WorldObjectsPresets")]
    class WorldObjects : ScriptableObject
    {
        public PlayerObject PlayerPreset;
    }
}
