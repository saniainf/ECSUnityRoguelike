using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "EcsRoguelike/WorldObjectsPresets", fileName = "WorldObjectsPresets")]
    class WorldObjects : ScriptableObject
    {
        public ResourcesObjects ResourcesPresets;
        [Space(10)]
        public WallsObject WallsPresets;
        public SpritesObject ObstaclePresets;
        public SpritesObject FloorPresets;
        public SpritesObject ExitPointPreset;
        [Space(10)]
        public PlayerObject PlayerPreset;
        public EnemyObject Enemy01Preset;
        public EnemyObject Enemy02Preset;
        [Space(10)]
        public CollectItemObject HealItemPreset;
        public CollectItemObject BoostHPItemPreset;
        [Space(10)]
        public SpritesObject ArrowPreset;
    }
}
