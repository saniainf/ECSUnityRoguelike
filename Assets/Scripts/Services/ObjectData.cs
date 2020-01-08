using UnityEngine;

namespace Client
{
    /// <summary>
    /// все ресурсы
    /// </summary>
    static class ObjData
    {
        //трансформы
        public static Transform t_GameBoardRoot;
        public static Transform t_GameObjectsRoot;
        public static Transform t_GameObjectsOther;

        //ресурсы
        public static Sprite[] r_SpriteSheet = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet");
        public static GameObject r_PrefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");
        public static GameObject r_PrefabAnimation = Resources.Load<GameObject>("Prefabs/PrefabAnimation");
        public static GameObject r_PrefabPhysicsSprite = Resources.Load<GameObject>("Prefabs/PrefabPhysicsSprite");
        public static GameObject r_PrefabPhysicsAnimation = Resources.Load<GameObject>("Prefabs/PrefabPhysicsAnimation");

        //преднастройки сущностей
        public static WallsObject p_WallsPresets = Resources.Load<WallsObject>("Presets/WallsPreset");
        public static SpritesObject p_ChopEffect = Resources.Load<SpritesObject>("Presets/ChopEffect");
        public static SpritesObject p_ObstaclePresets = Resources.Load<SpritesObject>("Presets/ObstaclePresets");
        public static SpritesObject p_FloorPresets = Resources.Load<SpritesObject>("Presets/FloorPresets");
        public static SpritesObject p_ExitPointPreset = Resources.Load<SpritesObject>("Presets/ExitPointPresets");
        public static PlayerObject p_PlayerPreset = Resources.Load<PlayerObject>("Presets/PlayerPreset");
        public static EnemyPreset p_Enemy01Preset = Resources.Load<EnemyPreset>("Presets/Enemy/Enemy01");
        public static EnemyPreset p_Enemy02Preset = Resources.Load<EnemyPreset>("Presets/Enemy/Enemy02");
        public static CollectItemObject p_HealItemPreset = Resources.Load<CollectItemObject>("Presets/HealItemPreset");
        public static CollectItemObject p_BoostHPItemPreset = Resources.Load<CollectItemObject>("Presets/BoostHPItemPreset");
        public static WeaponItemObject p_WeaponChopperPreset = Resources.Load<WeaponItemObject>("Presets/WeaponChopperPreset");
        public static WeaponItemObject p_WeaponClawsPreset = Resources.Load<WeaponItemObject>("Presets/WeaponClawsPreset");
        public static WeaponItemObject p_WeaponClawsMK2Preset = Resources.Load<WeaponItemObject>("Presets/WeaponClawsMK2Preset");
        public static WeaponItemObject p_WeaponStonePreset = Resources.Load<WeaponItemObject>("Presets/WeaponStonePreset");
        
        public static SpritesObject p_Overlay = Resources.Load<SpritesObject>("Presets/Overlay");
    }
}

