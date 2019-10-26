using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    class GameLevel
    {
        private EcsWorld _world;
        private readonly WorldObjects _worldObjects;

        #region Resourses
        //private readonly Sprite[] spriteSheet;
        private readonly GameObject prefabSprite;
        private readonly GameObject prefabAnimation;

        private readonly EnemyObject enemy01Pres;
        private readonly EnemyObject enemy02Pres;
        private readonly PlayerObject playerPres;
        private readonly RuntimeAnimatorController[] wallsAnimation;

        readonly Transform gameBoardRoot = new GameObject("GameBoardRoot").transform;
        readonly Transform gameObjectsRoot = new GameObject("GameObjectsRoot").transform;
        public Transform GameObjectsOther = new GameObject("GameObjectsOther").transform;

        private Sprite[] obstacleSprites;
        private Sprite[] floorSprites;
        private Sprite sodaSprite;
        private Sprite boostSprite;
        private Sprite exitSprite;
        #endregion

        #region Settings
        int sodaCount = 3;
        int boostCount = 1;
        int wallCount = 5;
        int enemy01Count = 2;
        int enemy02Count = 1;

        int boostHPValue = 3;
        int sodaFoodValue = 2;

        int minWallHP = 2;
        int maxWallHP = 4;

        (int HP, int currentHP, int hitDamage, int initiative) playerSet;

        #endregion

        public GameLevel(EcsWorld world, WorldObjects worldObjects, (int HP, int currentHP, int hitDamage, int initiative) playerSet)
        {
            _world = world;
            _worldObjects = worldObjects;
            this.playerSet = playerSet;

            wallsAnimation = worldObjects.WallsPresets.Animation;
            enemy01Pres = worldObjects.Enemy01Preset;
            enemy02Pres = worldObjects.Enemy02Preset;
            playerPres = worldObjects.PlayerPreset;

            //this.spriteSheet = worldObjects.ResourcesPresets.SpriteSheet;
            this.prefabSprite = worldObjects.ResourcesPresets.PrefabSprite;
            this.prefabAnimation = worldObjects.ResourcesPresets.PrefabAnimation;

            SetActive(false);
        }

        public void LevelCreate()
        {
            var rooms = new Rooms();

            var roomsArray = VExt.NextFromArray(rooms.RoomsArray);
            Array.Reverse(roomsArray);

            int width = roomsArray[0].Length;
            int height = roomsArray.Length; ;

            obstacleSprites = _worldObjects.ObstaclePresets.spritesArray;
            floorSprites = _worldObjects.FloorPresets.spritesArray;
            sodaSprite = _worldObjects.HealItemPreset.Sprite;
            boostSprite = _worldObjects.BoostHPItemPreset.Sprite;
            exitSprite = _worldObjects.ExitPointPreset.spriteSingle;

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
                            LayoutPlayerObject(j, i, playerSet);
                            break;
                        default:
                            break;
                    }
                }

            for (int i = 0; i < boostCount; i++)
            {
                LayoutBoostHPObject(ref emptyCells);
            }

            for (int i = 0; i < sodaCount; i++)
            {
                LayoutFoodObject(ref emptyCells);
            }

            for (int i = 0; i < wallCount; i++)
            {
                LayoutWallObject(ref emptyCells);
            }

            for (int i = 0; i < enemy01Count; i++)
            {
                LayoutEnemyObject(ref emptyCells, "enemy01", enemy01Pres.Animation, (enemy01Pres.HealthPoint, enemy01Pres.HealthPoint, enemy01Pres.HitDamage, enemy01Pres.Initiative));
            }

            for (int i = 0; i < enemy02Count; i++)
            {
                LayoutEnemyObject(ref emptyCells, "enemy02", enemy02Pres.Animation, (enemy02Pres.HealthPoint, enemy02Pres.HealthPoint, enemy02Pres.HitDamage, enemy02Pres.Initiative));
            }
        }

        public void LevelDestroy()
        {
            UnityEngine.Object.Destroy(gameBoardRoot.gameObject);
            UnityEngine.Object.Destroy(gameObjectsRoot.gameObject);
            UnityEngine.Object.Destroy(GameObjectsOther.gameObject);
            _world = null;
        }

        public void SetActive(bool value)
        {
            gameBoardRoot.gameObject.SetActive(value);
            gameObjectsRoot.gameObject.SetActive(value);
            GameObjectsOther.gameObject.SetActive(value);
        }

        #region Layout
        void LayoutFloorObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(prefabSprite, x, y, "floor", gameBoardRoot, LayersName.Floor.ToString(), VExt.NextFromArray(floorSprites));
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();
        }

        void LayoutObstacleObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(prefabSprite, x, y, "obstacle", gameBoardRoot, LayersName.Wall.ToString(), VExt.NextFromArray(obstacleSprites));
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out ObstacleComponent _);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();
        }

        void LayoutExitObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(prefabSprite, x, y, "exit", gameObjectsRoot, LayersName.Object.ToString(), exitSprite);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out ZoneExitComponent _);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();
        }

        void LayoutPlayerObject(int x, int y, (int HP, int currentHP, int hitDamage, int initiative) set)
        {

            var go = VExt.LayoutAnimationObjects(prefabAnimation, x, y, "player", gameObjectsRoot, LayersName.Character.ToString(), playerPres.Animation);
            var playerEntity = _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out AnimationComponent animationComponent, out PlayerComponent player);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(playerEntity);

            dataComponent.HealthPoint = set.HP;
            dataComponent.CurrentHealthPoint = set.currentHP;
            dataComponent.HitDamage = set.hitDamage;
            dataComponent.Initiative = set.initiative;
        }

        void LayoutBoostHPObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "boostHP", gameObjectsRoot, LayersName.Object.ToString(), boostSprite);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out CollectItemComponent collectItemComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            collectItemComponent.Type = CollectItemType.BoostHP;
            collectItemComponent.Value = _worldObjects.BoostHPItemPreset.Value;

            emptyCells.Remove(cell);
        }

        void LayoutFoodObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "soda", gameObjectsRoot, LayersName.Object.ToString(), sodaSprite);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out CollectItemComponent collectItemComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            collectItemComponent.Type = CollectItemType.Heal;
            collectItemComponent.Value = _worldObjects.HealItemPreset.Value;

            emptyCells.Remove(cell);
        }

        void LayoutWallObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(prefabAnimation, cell.x, cell.y, "wall", gameObjectsRoot, LayersName.Object.ToString(), VExt.NextFromArray(wallsAnimation));
            var wall = _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out AnimationComponent animationComponent, out WallComponent _);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(wall);
            dataComponent.HealthPoint = UnityEngine.Random.Range(minWallHP, maxWallHP + 1);
            dataComponent.CurrentHealthPoint = dataComponent.HealthPoint;

            emptyCells.Remove(cell);
        }

        void LayoutEnemyObject(ref List<Vector2Int> emptyCells, string goName, RuntimeAnimatorController animation, (int HP, int currentHP, int hitDamage, int initiative) set)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(prefabAnimation, cell.x, cell.y, goName, gameObjectsRoot, LayersName.Character.ToString(), animation);
            var enemy = _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out AnimationComponent animationComponent, out EnemyComponent _);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(enemy);
            dataComponent.HealthPoint = set.HP;
            dataComponent.CurrentHealthPoint = set.currentHP;
            dataComponent.HitDamage = set.hitDamage;
            dataComponent.Initiative = set.initiative;

            emptyCells.Remove(cell);
        }
        #endregion
    }
}
