using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    static class ObjectData
    {
        public static Transform GameObjectsOther = new GameObject("GameObjectsOther").transform;

        public static WallsObject WallsPresets = Resources.Load<WallsObject>("Presets/WallsPreset");

        public static ResourcesObjects ResourcesPresets = Resources.Load<ResourcesObjects>("Presets/ResourcesPresets");
        public static SpritesObject ChopEffect = Resources.Load<SpritesObject>("Presets/ChopEffect");
        public static SpritesObject ObstaclePresets;
        public static SpritesObject FloorPresets;
        public static SpritesObject ExitPointPreset;
        public static PlayerObject PlayerPreset;
        public static EnemyObject Enemy01Preset;
        public static EnemyObject Enemy02Preset;
        public static CollectItemObject HealItemPreset;
        public static CollectItemObject BoostHPItemPreset;
        public static SpritesObject ArrowPreset;
    }
}
