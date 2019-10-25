using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/WorldObjectsPresets", fileName = "WorldObjectsPresets")]
    class WorldObjects : ScriptableObject
    {
        public ResourcesObjects ResourcesPresets;
        public WallsObject WallsPresets;
        public PlayerObject PlayerPreset;
        public EnemyObject Enemy01Preset;
        public EnemyObject Enemy02Preset;
    }
}
