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
        private readonly GameObject prefabSprite;
        private readonly GameObject prefabAnimation;
        private readonly GameObject prefabPhysicsSprite;
        private readonly GameObject prefabPhysicsAnimation;

        private readonly EnemyObject enemy01Pres;
        private readonly EnemyObject enemy02Pres;
        private readonly PlayerObject playerPres;
        private readonly RuntimeAnimatorController[] wallsAnimation;

        readonly Transform gameBoardRoot = new GameObject("GameBoardRoot").transform;
        readonly Transform gameObjectsRoot = new GameObject("GameObjectsRoot").transform;
        public Transform GameObjectsOther = new GameObject("GameObjectsOther").transform;

        private Sprite[] obstacleSprites;
        private Sprite[] floorSprites;
        private Sprite healSprite;
        private Sprite boostHPSprite;
        private Sprite exitSprite;
        #endregion

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

            prefabSprite = worldObjects.ResourcesPresets.PrefabSprite;
            prefabAnimation = worldObjects.ResourcesPresets.PrefabAnimation;
            prefabPhysicsSprite = worldObjects.ResourcesPresets.PrefabPhysicsSprite;
            prefabPhysicsAnimation = worldObjects.ResourcesPresets.PrefabPhysicsAnimation;

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
            healSprite = _worldObjects.HealItemPreset.Sprite;
            boostHPSprite = _worldObjects.BoostHPItemPreset.Sprite;
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
            _world.CreateEntityWith(out GameObjectComponent goComponent);

            goComponent.Transform = go.transform;
        }

        void LayoutObstacleObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(prefabPhysicsSprite, x, y, "obstacle", gameBoardRoot, LayersName.Wall.ToString(), VExt.NextFromArray(obstacleSprites));
            _world.CreateEntityWith(out GameObjectComponent goComponent, out ObstacleComponent _);

            goComponent.Transform = go.transform;
            goComponent.Rigidbody = go.GetComponent<Rigidbody2D>();
            goComponent.Collider = go.GetComponent<BoxCollider2D>();
            goComponent.SpriteRenderer = go.GetComponent<SpriteRenderer>();
        }

        void LayoutExitObject(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(prefabSprite, x, y, "exit", gameObjectsRoot, LayersName.Object.ToString(), exitSprite);
            _world.CreateEntityWith(out GameObjectComponent goComponent, out ZoneExitComponent _);

            goComponent.Transform = go.transform;
        }

        void LayoutPlayerObject(int x, int y, (int HP, int currentHP, int hitDamage, int initiative) set)
        {

            var go = VExt.LayoutAnimationObjects(prefabPhysicsAnimation, x, y, "player", gameObjectsRoot, LayersName.Character.ToString(), playerPres.Animation);
            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out AnimationComponent animationComponent, out PlayerComponent player);

            goComponent.Transform = go.transform;
            goComponent.Rigidbody = go.GetComponent<Rigidbody2D>();
            goComponent.Collider = go.GetComponent<BoxCollider2D>();
            goComponent.SpriteRenderer = go.GetComponent<SpriteRenderer>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);

            dataComponent.HealthPoint = set.HP;
            dataComponent.CurrentHealthPoint = set.currentHP;
            dataComponent.HitDamage = set.hitDamage;
            dataComponent.Initiative = set.initiative;
        }

        void LayoutBoostHPObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "boostHP", gameObjectsRoot, LayersName.Object.ToString(), boostHPSprite);
            _world.CreateEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);

            goComponent.Transform = go.transform;

            CollectItemBoostHP itemBoostHP = new CollectItemBoostHP(_worldObjects.BoostHPItemPreset.Value);
            collectItemComponent.CollectItem = itemBoostHP;

            emptyCells.Remove(cell);
        }

        void LayoutHealObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "heal", gameObjectsRoot, LayersName.Object.ToString(), healSprite);
            _world.CreateEntityWith(out GameObjectComponent goComponent, out CollectItemComponent collectItemComponent);

            goComponent.Transform = go.transform;

            CollectItemHeal itemHeal = new CollectItemHeal(_worldObjects.HealItemPreset.Value);
            collectItemComponent.CollectItem = itemHeal;

            emptyCells.Remove(cell);
        }

        void LayoutWallObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(prefabPhysicsAnimation, cell.x, cell.y, "wall", gameObjectsRoot, LayersName.Object.ToString(), VExt.NextFromArray(wallsAnimation));
            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out AnimationComponent animationComponent, out WallComponent _);

            goComponent.Transform = go.transform;
            goComponent.Rigidbody = go.GetComponent<Rigidbody2D>();
            goComponent.Collider = go.GetComponent<BoxCollider2D>();
            goComponent.SpriteRenderer = go.GetComponent<SpriteRenderer>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);
            dataComponent.HealthPoint = UnityEngine.Random.Range(minWallHP, maxWallHP + 1);
            dataComponent.CurrentHealthPoint = dataComponent.HealthPoint;

            emptyCells.Remove(cell);
        }

        void LayoutEnemyObject(ref List<Vector2Int> emptyCells, string goName, RuntimeAnimatorController animation, (int HP, int currentHP, int hitDamage, int initiative) set)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutAnimationObjects(prefabPhysicsAnimation, cell.x, cell.y, goName, gameObjectsRoot, LayersName.Character.ToString(), animation);
            var e = _world.CreateEntityWith(out GameObjectComponent goComponent, out AnimationComponent animationComponent, out EnemyComponent _);

            goComponent.Transform = go.transform;
            goComponent.Rigidbody = go.GetComponent<Rigidbody2D>();
            goComponent.Collider = go.GetComponent<BoxCollider2D>();
            goComponent.SpriteRenderer = go.GetComponent<SpriteRenderer>();

            animationComponent.animator = go.GetComponent<Animator>();

            var dataComponent = _world.AddComponent<DataSheetComponent>(e);
            dataComponent.HealthPoint = set.HP;
            dataComponent.CurrentHealthPoint = set.currentHP;
            dataComponent.HitDamage = set.hitDamage;
            dataComponent.Initiative = set.initiative;

            emptyCells.Remove(cell);
        }
        #endregion
    }
}
