﻿using UnityEngine;

namespace Client
{
    static class ObjData
    {
        public static Transform t_GameBoardRoot;
        public static Transform t_GameObjectsRoot;
        public static Transform t_GameObjectsOther;

        public static Sprite[] r_SpriteSheet = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet");
        public static GameObject r_PrefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");
        public static GameObject r_PrefabAnimation = Resources.Load<GameObject>("Prefabs/PrefabAnimation");
        public static GameObject r_PrefabPhysicsSprite = Resources.Load<GameObject>("Prefabs/PrefabPhysicsSprite");
        public static GameObject r_PrefabPhysicsAnimation = Resources.Load<GameObject>("Prefabs/PrefabPhysicsAnimation");

        public static WallsObject p_WallsPresets = Resources.Load<WallsObject>("Presets/WallsPreset");
        public static SpritesObject p_ChopEffect = Resources.Load<SpritesObject>("Presets/ChopEffect");
        public static SpritesObject p_ObstaclePresets = Resources.Load<SpritesObject>("Presets/ObstaclePresets");
        public static SpritesObject p_FloorPresets = Resources.Load<SpritesObject>("Presets/FloorPresets");
        public static SpritesObject p_ExitPointPreset = Resources.Load<SpritesObject>("Presets/ExitPointPresets");
        public static PlayerObject p_PlayerPreset = Resources.Load<PlayerObject>("Presets/PlayerPreset");
        public static EnemyObject p_Enemy01Preset = Resources.Load<EnemyObject>("Presets/Enemy01Preset");
        public static EnemyObject p_Enemy02Preset = Resources.Load<EnemyObject>("Presets/Enemy02Preset");
        public static CollectItemObject p_HealItemPreset = Resources.Load<CollectItemObject>("Presets/HealItemPreset");
        public static CollectItemObject p_BoostHPItemPreset = Resources.Load<CollectItemObject>("Presets/BoostHPItemPreset");
    }
}

