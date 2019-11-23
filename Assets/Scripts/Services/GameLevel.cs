using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// построение уровня
    /// </summary>
    class GameLevel
    {
        private EcsWorld _world;

        #region Settings
        int healCount = 3;
        int boostHPCount = 1;
        int wallCount = 5;
        int enemy01Count = 2;
        int enemy02Count = 1;

        int boostHPValue = 3;
        int healValue = 2;

        int minWallHP = 2;
        int maxWallHP = 4;
        #endregion

        NPCDataSheet playerData;

        public GameLevel(EcsWorld world, NPCDataSheet playerData)
        {
            _world = world;

            this.playerData = playerData;

            ObjData.t_GameBoardRoot = new GameObject("GameBoardRoot").transform;
            ObjData.t_GameObjectsRoot = new GameObject("GameObjectsRoot").transform;
            ObjData.t_GameObjectsOther = new GameObject("GameObjectsOther").transform;

            SetActive(false);
        }

        public void LevelCreate()
        {
            var rooms = new Rooms();

            var roomsArray = VExt.NextFromArray(rooms.RoomsArray);
            Array.Reverse(roomsArray);

            int width = roomsArray[0].Length;
            int height = roomsArray.Length; ;

            List<Vector2Int> emptyCells = new List<Vector2Int>();

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    switch (roomsArray[i][j])
                    {
                        case '.':
                            LayoutFloorObject(j, i);
                            emptyCells.Add(new Vector2Int(j, i));
                            break;
                        case '#':
                            LayoutObstacleObject(j, i);
                            break;
                        case 'X':
                            LayoutFloorObject(j, i);
                            LayoutExitObject(j, i);
                            break;
                        case '@':
                            LayoutFloorObject(j, i);
                            LayoutPlayerObject(j, i, playerData);
                            break;
                        default:
                            break;
                    }
                }

            for (int i = 0; i < boostHPCount; i++)
            {
                LayoutBoostHPObject(ref emptyCells);
            }

            for (int i = 0; i < healCount; i++)
            {
                LayoutHealObject(ref emptyCells);
            }

            for (int i = 0; i < wallCount; i++)
            {
                LayoutWallObject(ref emptyCells);
            }

            for (int i = 0; i < enemy01Count; i++)
            {
                LayoutEnemyObject(
                    ref emptyCells,
                    "enemy01",
                    ObjData.p_Enemy01Preset.Animation,
                    new NPCDataSheet(
                        new NPCStats(ObjData.p_Enemy01Preset.HealthPoint,
                                     ObjData.p_Enemy01Preset.HealthPoint,
                                     ObjData.p_Enemy01Preset.Initiative),
                        new WeaponItemChopper(ObjData.p_Enemy01Preset.WeaponItem.Damage)));
            }

            for (int i = 0; i < enemy02Count; i++)
            {
                LayoutEnemyObject(
                    ref emptyCells,
                    "enemy02",
                    ObjData.p_Enemy02Preset.Animation,
                    new NPCDataSheet(
                        new NPCStats(ObjData.p_Enemy02Preset.HealthPoint,
                                     ObjData.p_Enemy02Preset.HealthPoint,
                                     ObjData.p_Enemy02Preset.Initiative),
                        new WeaponItemChopper(ObjData.p_Enemy02Preset.WeaponItem.Damage)));
            }
        }

        public void LevelDestroy()
        {
            UnityEngine.Object.Destroy(ObjData.t_GameBoardRoot.gameObject);
            UnityEngine.Object.Destroy(ObjData.t_GameObjectsRoot.gameObject);
            UnityEngine.Object.Destroy(ObjData.t_GameObjectsOther.gameObject);

            _world = null;
        }

        public void SetActive(bool value)
        {
            ObjData.t_GameBoardRoot.gameObject.SetActive(value);
            ObjData.t_GameObjectsRoot.gameObject.SetActive(value);
            ObjData.t_GameObjectsOther.gameObject.SetActive(value);
        }

        #region Layout
        void LayoutFloorObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(
                ObjData.r_PrefabSprite,
                x, y,
                "floor",
                ObjData.t_GameObjectsRoot,
                LayersName.Floor.ToString(),
                VExt.NextFromArray(ObjData.p_FloorPresets.spritesArray));

            _world.CreateEntityWith(out GameObjectComponent goComponent);
            goComponent.Transform = go.transform;
        }

        void LayoutObstacleObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(
                ObjData.r_PrefabPhysicsSprite,
                x, y,
                "obstacle",
                ObjData.t_GameBoardRoot,
                LayersName.Wall.ToString(),
                VExt.NextFromArray(ObjData.p_ObstaclePresets.spritesArray));

            _world.CreateEntityWith(out GameObjectComponent goComponent, out ObstacleComponent _);
            goComponent.Transform = go.transform;
            goComponent.Link = go.GetComponent<PrefabComponentsShortcut>();
        }

        void LayoutExitObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(
                ObjData.r_PrefabSprite,
                x, y,
                "exit",
                ObjData.t_GameObjectsRoot,
                LayersName.Object.ToString(),
                ObjData.p_ExitPointPreset.spriteSingle);

            _world.CreateEntityWith(out GameObjectComponent goComponent, out ZoneExitComponent _);
            goComponent.Transform = go.transform;
        }

        void LayoutPlayerObject(int x, int y, NPCDataSheet data)
        {

            var go = VExt.LayoutAnimationObjects(
                ObjData.r_PrefabPhysicsAnimation,
                x, y,
                "player",
                ObjData.t_GameObjectsRoot,
                LayersName.Character.ToString(),
                ObjData.p_PlayerPreset.Animation);

            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out PlayerComponent _);

            goComponent.Transform = go.transform;
            goComponent.Link = go.GetComponent<PrefabComponentsShortcut>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);
            dataComponent.Stats = playerData.NPCStats;
            dataComponent.WeaponItem = playerData.WeaponItem;
        }

        void LayoutBoostHPObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(
                ObjData.r_PrefabSprite,
                cell.x, cell.y,
                "boostHP",
                ObjData.t_GameObjectsRoot,
                LayersName.Object.ToString(), ObjData.p_BoostHPItemPreset.Sprite);

            _world.CreateEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);
            goComponent.Transform = go.transform;
            collectItemComponent.CollectItem = new CollectItemBoostHP(ObjData.p_BoostHPItemPreset.Value);

            emptyCells.Remove(cell);
        }

        void LayoutHealObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(
                ObjData.r_PrefabSprite,
                cell.x, cell.y,
                "heal",
                ObjData.t_GameObjectsRoot,
                LayersName.Object.ToString(),
                ObjData.p_HealItemPreset.Sprite);

            _world.CreateEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);
            goComponent.Transform = go.transform;
            collectItemComponent.CollectItem = new CollectItemHeal(ObjData.p_HealItemPreset.Value);

            emptyCells.Remove(cell);
        }

        void LayoutWallObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(
                ObjData.r_PrefabPhysicsAnimation,
                cell.x, cell.y,
                "wall",
                ObjData.t_GameObjectsRoot,
                LayersName.Object.ToString(),
                VExt.NextFromArray(ObjData.p_WallsPresets.Animation));

            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out WallComponent _);

            goComponent.Transform = go.transform;
            goComponent.Link = go.GetComponent<PrefabComponentsShortcut>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);
            dataComponent.Stats.MaxHealthPoint = UnityEngine.Random.Range(minWallHP, maxWallHP + 1);
            dataComponent.Stats.HealthPoint = dataComponent.Stats.MaxHealthPoint;

            emptyCells.Remove(cell);
        }

        void LayoutEnemyObject(ref List<Vector2Int> emptyCells, string goName, RuntimeAnimatorController animation, NPCDataSheet data)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(ObjData.r_PrefabPhysicsAnimation, cell.x, cell.y, goName, ObjData.t_GameObjectsRoot, LayersName.Character.ToString(), animation);
            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out EnemyComponent _);

            goComponent.Transform = go.transform;
            goComponent.Link = go.GetComponent<PrefabComponentsShortcut>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);
            dataComponent.Stats = data.NPCStats;
            dataComponent.WeaponItem = data.WeaponItem;

            emptyCells.Remove(cell);
        }
        #endregion
    }
}
