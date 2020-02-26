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
        public static GameObject r_PrefabGameObject = Resources.Load<GameObject>("Prefabs/PrefabGameObject");

        //преднастройки сущностей
        public static WallsObject p_WallsPresets = Resources.Load<WallsObject>("Presets/Other/WallsPreset");
        public static SpritesObject p_ChopEffect = Resources.Load<SpritesObject>("Presets/Other/ChopEffect");
        public static SpritesObject p_ObstaclePresets = Resources.Load<SpritesObject>("Presets/Other/ObstaclePresets");
        public static SpritesObject p_FloorPresets = Resources.Load<SpritesObject>("Presets/Other/FloorPresets");
        public static SpritesObject p_ExitPointPreset = Resources.Load<SpritesObject>("Presets/Other/ExitPointPresets");
        public static PlayerObject p_PlayerPreset = Resources.Load<PlayerObject>("Presets/Other/PlayerPreset");
        public static EnemyPreset p_Enemy01Preset = Resources.Load<EnemyPreset>("Presets/Other/Enemy/Enemy01");
        public static EnemyPreset p_Enemy02Preset = Resources.Load<EnemyPreset>("Presets/Other/Enemy/Enemy02");
        public static CollectItemObject p_HealItemPreset = Resources.Load<CollectItemObject>("Presets/Other/HealItemPreset");
        public static CollectItemObject p_BoostHPItemPreset = Resources.Load<CollectItemObject>("Presets/Other/BoostHPItemPreset");
        public static WeaponItemObject p_WeaponChopperPreset = Resources.Load<WeaponItemObject>("Presets/Other/WeaponChopperPreset");
        public static WeaponItemObject p_WeaponClawsPreset = Resources.Load<WeaponItemObject>("Presets/Other/WeaponClawsPreset");
        public static WeaponItemObject p_WeaponClawsMK2Preset = Resources.Load<WeaponItemObject>("Presets/Other/WeaponClawsMK2Preset");
        public static WeaponItemObject p_WeaponStonePreset = Resources.Load<WeaponItemObject>("Presets/Other/WeaponStonePreset");

        public static SpritesObject p_Overlay = Resources.Load<SpritesObject>("Presets/Other/Overlay");

        public static GameObjectPreset p_AcidPuddle = Resources.Load<GameObjectPreset>("Presets/GameObjects/AcidPuddle");
    }
}

