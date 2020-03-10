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
        private EntitiesPresetsInject _presets;

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

        public GameLevel(EcsWorld world, EntitiesPresetsInject presets)
        {
            _world = world;
            _presets = presets;
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
                    if (roomsArray[i][j] != ' ')
                    {
                        NewLevelTile(rooms.GetNameID('.'), new Vector2(j, i));
                    }
                    switch (roomsArray[i][j])
                    {
                        case '.':
                            emptyCells.Add(new Vector2Int(j, i));
                            break;
                        case '#':
                            NewLevelTile(rooms.GetNameID('#'), new Vector2(j, i));
                            break;
                        case 'X':
                            NewLevelTile(rooms.GetNameID('X'), new Vector2(j, i));
                            break;
                        case '@':
                            NewPlayer(j, i);
                            break;
                        case 'A':
                            NewLevelTile(rooms.GetNameID('A'), new Vector2(j, i));
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
                NewFruitHeal(ref emptyCells);
            }

            for (int i = 0; i < wallCount; i++)
            {
                LayoutWallObject(ref emptyCells);
            }

            for (int i = 0; i < enemy01Count; i++)
            {
                LayoutEnemyObject(ref emptyCells, ObjData.p_Enemy01Preset);
            }

            for (int i = 0; i < enemy02Count; i++)
            {
                LayoutEnemyObject(ref emptyCells, ObjData.p_Enemy02Preset);
            }
        }

        public void SetActive(bool value)
        {
            ObjData.t_GameBoardRoot.gameObject.SetActive(value);
            ObjData.t_GameObjectsRoot.gameObject.SetActive(value);
            ObjData.t_GameObjectsOther.gameObject.SetActive(value);
        }

        private void NewLevelTile(string id, Vector2 pos)
        {
            _presets.LevelTiles.TryGetValue(id, out LevelTilePreset preset);
            _world.RLNewLevelObject(preset, pos);
        }

        void NewPlayer(int x, int y)
        {
            _world.RLNewLevelObject(_presets.Player, new Vector2(x, y));
        }

        void NewFruitHeal(ref List<Vector2Int> emptyCells)
        {
            var pos = VExt.NextFromList(emptyCells);
            _presets.CollectingItems.TryGetValue("FruitHeal", out CollectingItemPreset preset);
            _world.RLNewLevelObject(preset, pos);
        }

        void LayoutBoostHPObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObject(
                ObjData.r_PrefabSprite,
                cell.x, cell.y,
                "boostHP",
                ObjData.t_GameObjectsRoot,
                SortingLayer.Object.ToString(), ObjData.p_BoostHPItemPreset.Sprite);

            _world.NewEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);
            goComponent.Transform = go.transform;
            goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();
            collectItemComponent.CollectItem = new CollectItemBoostHP(ObjData.p_BoostHPItemPreset.Value);

            emptyCells.Remove(cell);
        }

        void LayoutHealObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObject(
                ObjData.r_PrefabSprite,
                cell.x, cell.y,
                "heal",
                ObjData.t_GameObjectsRoot,
                SortingLayer.Object.ToString(),
                ObjData.p_HealItemPreset.Sprite);

            _world.NewEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);
            goComponent.Transform = go.transform;
            goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();
            collectItemComponent.CollectItem = new CollectItemHeal(ObjData.p_HealItemPreset.Value);

            emptyCells.Remove(cell);
        }

        void LayoutWallObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObject(
                ObjData.r_PrefabPhysicsAnimation,
                cell.x, cell.y,
                "wall",
                ObjData.t_GameObjectsRoot,
                SortingLayer.Object.ToString(),
                VExt.NextFromArray(ObjData.p_WallsPresets.Animation));

            var e = _world.NewEntityWith(out GameObjectComponent goComponent, out WallComponent _);

            goComponent.Transform = go.transform;
            goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();
            goComponent.GObj.NPCNameText.text = e.GetInternalId().ToString();

            var dataComponent = e.Set<DataSheetComponent>();
            dataComponent.Stats.MaxHealthPoint = UnityEngine.Random.Range(minWallHP, maxWallHP + 1);
            dataComponent.Stats.HealthPoint = dataComponent.Stats.MaxHealthPoint;

            emptyCells.Remove(cell);
        }

        void LayoutEnemyObject(ref List<Vector2Int> emptyCells, EnemyPreset enemyPreset)
        {
            var cell = VExt.NextFromList(emptyCells);

            _world.RLCreateEnemy(cell, enemyPreset);

            emptyCells.Remove(cell);
        }
    }
}
