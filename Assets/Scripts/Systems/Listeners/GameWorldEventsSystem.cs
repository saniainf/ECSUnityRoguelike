using System;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class GameWorldEventsSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<WorldCreateEvent> _worldCreateEvent = null;
        readonly EcsFilter<WorldDestroyEvent> _worldDestroyEvent = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;
        readonly EcsFilter<PositionComponent>.Exclude<PlayerComponent, GameObjectRemoveEvent> _transformEntities = null;

        #region Resourses
        readonly Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet");
        readonly GameObject prefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");
        readonly GameObject prefabAnimation = Resources.Load<GameObject>("Prefabs/PrefabAnimation");
        readonly RuntimeAnimatorController playerAnimation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/PlayerAnimatorController");
        readonly RuntimeAnimatorController enemy01Animation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Enemy1AnimatorController");
        readonly RuntimeAnimatorController enemy02Animation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Enemy2AnimatorController");

        readonly RuntimeAnimatorController[] wallsAnimation = {
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/01WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/02WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/03WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/04WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/05WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/06WallAnimationController"),
        Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Walls/07WallAnimationController")};

        readonly Transform gameBoardRoot = new GameObject("GameBoardRoot").transform;
        readonly Transform gameObjectsRoot = new GameObject("GameObjectsRoot").transform;

        Sprite[] obstacleSprites;
        Sprite[] floorSprites;
        Sprite sodaSprite;
        Sprite boostSprite;
        Sprite exitSprite;
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

        (int HP, int currentHP, int hitDamage, int initiative) playerSet = (10, 10, 1, 10);
        (int HP, int currentHP, int hitDamage, int initiative) enemy01Set = (2, 2, 1, 1);
        (int HP, int currentHP, int hitDamage, int initiative) enemy02Set = (3, 3, 2, 2);

        #endregion

        void IEcsRunSystem.Run()
        {
            if (_worldCreateEvent.GetEntitiesCount() > 0)
            {
                WorldCreate();
            }

            if (_worldDestroyEvent.GetEntitiesCount() > 0)
            {
                WorldDestroy();
            }
        }

        void WorldCreate()
        {
            var gameLevels = new GameLevels();

            var levelArray = VExt.NextFromArray(gameLevels.LevelsArray);
            Array.Reverse(levelArray);

            int width = levelArray[0].Length;
            int height = levelArray.Length; ;

            obstacleSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 25, 26, 28, 29 });
            floorSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 32, 33, 34, 35, 36, 37, 38, 39 });
            sodaSprite = spriteSheet[18];
            boostSprite = spriteSheet[19];
            exitSprite = spriteSheet[20];

            List<Vector2Int> emptyCells = new List<Vector2Int>();

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    switch (levelArray[i][j])
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
                LayoutEnemyObject(ref emptyCells, "enemy01", enemy01Animation, enemy01Set);
            }

            for (int i = 0; i < enemy02Count; i++)
            {
                LayoutEnemyObject(ref emptyCells, "enemy02", enemy02Animation, enemy02Set);
            }
        }

        void WorldDestroy()
        {
            foreach (var i in _transformEntities)
            {
                ref var e = ref _transformEntities.Entities[i];
                _world.AddComponent<GameObjectRemoveEvent>(e);
            }

            foreach (var i in _playerEntities)
            {
                ref var e = ref _playerEntities.Entities[i];
                var c1 = _playerEntities.Components1[i];
                playerSet.HP = c1.HealthPoint;
                playerSet.currentHP= c1.CurrentHealthPoint;
                playerSet.hitDamage = c1.HitDamage;
                _world.AddComponent<GameObjectRemoveEvent>(e);
            }
        }

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

            var go = VExt.LayoutAnimationObjects(prefabAnimation, x, y, "player", gameObjectsRoot, LayersName.Character.ToString(), playerAnimation);
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
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out BoostHPComponent boostHPComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            boostHPComponent.boostValue = boostHPValue;

            emptyCells.Remove(cell);
        }

        void LayoutFoodObject(ref List<Vector2Int> emptyCells)
        {
            var cell = VExt.NextFromList(emptyCells);

            var go = VExt.LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "soda", gameObjectsRoot, LayersName.Object.ToString(), sodaSprite);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out FoodComponent foodComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            foodComponent.foodValue = sodaFoodValue;

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
    }
}