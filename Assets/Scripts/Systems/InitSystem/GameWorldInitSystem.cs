using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class GameWorldInitSystem : IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        #region Resourses
        readonly Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet");
        readonly GameObject prefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");
        readonly GameObject prefabAnimation = Resources.Load<GameObject>("Prefabs/PrefabAnimation");
        readonly RuntimeAnimatorController playerAnimation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/PlayerAnimatorController");
        readonly RuntimeAnimatorController enemyAnimation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/EnemyAnimatorController");
        readonly RuntimeAnimatorController enemy2Animation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Enemy2AnimatorController");

        readonly Transform gameBoardRoot = new GameObject("GameBoardRoot").transform;
        readonly Transform gameObjectsRoot = new GameObject("GameObjectsRoot").transform;

        char[,] levelArray = new char[,]{
            { '8','8','8','8','8','8','8','8','8','8' },
            { '8','@','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','8','.','8','8','.','.','.','.','8' },
            { '8','.','.','.','8','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','8','.','.','.','.','8' },
            { '8','8','8','8','8','8','8','8','8','8' }};

        Sprite[] solidWallSprites;
        Sprite[] softWall;
        Sprite[] softWallDamage;
        Sprite[] floorSprites;
        Sprite sodaSprite;
        Sprite appleSprite;
        #endregion

        #region Settings
        int sodaCount = 3;
        int appleCount = 1;
        int wallCount = 5;
        int enemyCount = 1;
        int enemy2Count = 1;

        int appleFoodValue = 20;
        int sodaFoodValue = 10;

        int minWallHP = 2;
        int maxWallHP = 3;

        int initiative = 1;
        #endregion

        void IEcsInitSystem.Initialize()
        {
            solidWallSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 25, 26, 28, 29 });
            softWall = VExt.ExtractSubArray(spriteSheet, new int[] { 21, 22, 23, 24, 27, 30, 31 });
            softWallDamage = VExt.ExtractSubArray(spriteSheet, new int[] { 48, 49, 50, 51, 52, 53, 54 });
            floorSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 32, 33, 34, 35, 36, 37, 38, 39 });
            sodaSprite = spriteSheet[18];
            appleSprite = spriteSheet[19];

            List<Vector2Int> emptyCells = new List<Vector2Int>();

            GameObject go;
            WallComponent wallComponent;
            FoodComponent foodComponent;
            AnimationComponent animationComponent;
            TurnComponent turnComponent;
            GameObjectCreateEvent gameObjectCreateEvent;
            EnemyComponent enemyComponent;

            VExt.ReverseArray(ref levelArray);

            for (int i = 0; i < levelArray.GetLength(0); i++)
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    go = LayoutSpriteObjects(prefabSprite, j, i, "floor", gameBoardRoot, LayersName.Floor.ToString(), VExt.NextFromArray(floorSprites));
                    _world.CreateEntityWith(out gameObjectCreateEvent);

                    gameObjectCreateEvent.Transform = go.transform;
                    gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                    switch (levelArray[i, j])
                    {
                        case '.':
                            emptyCells.Add(new Vector2Int(j, i));
                            break;
                        case '8':
                            go = LayoutSpriteObjects(prefabSprite, j, i, "solidWall", gameBoardRoot, LayersName.Wall.ToString(), VExt.NextFromArray(solidWallSprites));
                            _world.CreateEntityWith(out gameObjectCreateEvent, out wallComponent);

                            gameObjectCreateEvent.Transform = go.transform;
                            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                            wallComponent.Solid = true;
                            break;
                        case '@':
                            go = LayoutAnimationObjects(prefabAnimation, j, i, "player", gameObjectsRoot, LayersName.Character.ToString(), playerAnimation);
                            var playerEntity = _world.CreateEntityWith(out gameObjectCreateEvent, out animationComponent, out PlayerComponent player);

                            gameObjectCreateEvent.Transform = go.transform;
                            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                            animationComponent.animator = go.GetComponent<Animator>();

                            turnComponent = _world.AddComponent<TurnComponent>(playerEntity);
                            turnComponent.Initiative = initiative++;
                            break;
                        default:
                            break;
                    }
                }
            for (int i = 0; i < appleCount; i++)
            {
                var cell = VExt.NextFromList(emptyCells);

                go = LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "apple", gameObjectsRoot, LayersName.Object.ToString(), appleSprite);
                _world.CreateEntityWith(out gameObjectCreateEvent, out foodComponent);

                gameObjectCreateEvent.Transform = go.transform;
                gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                foodComponent.foodValue = appleFoodValue;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < sodaCount; i++)
            {
                var cell = VExt.NextFromList(emptyCells);

                go = LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "soda", gameObjectsRoot, LayersName.Object.ToString(), sodaSprite);
                _world.CreateEntityWith(out gameObjectCreateEvent, out foodComponent);

                gameObjectCreateEvent.Transform = go.transform;
                gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                foodComponent.foodValue = sodaFoodValue;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < wallCount; i++)
            {
                var cell = VExt.NextFromList(emptyCells);
                int indxSprite = Random.Range(0, softWall.Length);

                go = LayoutSpriteObjects(prefabSprite, cell.x, cell.y, "softWall", gameObjectsRoot, LayersName.Object.ToString(), softWall[indxSprite]);
                _world.CreateEntityWith(out gameObjectCreateEvent, out wallComponent);

                gameObjectCreateEvent.Transform = go.transform;
                gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                wallComponent.Solid = false;
                wallComponent.HealthPoint = Random.Range(minWallHP, maxWallHP + 1);
                wallComponent.DamageSprite = softWallDamage[indxSprite];

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < enemyCount; i++)
            {
                var cell = VExt.NextFromList(emptyCells);

                go = LayoutAnimationObjects(prefabAnimation, cell.x, cell.y, "enemy", gameObjectsRoot, LayersName.Character.ToString(), enemyAnimation);
                var enemy1 = _world.CreateEntityWith(out gameObjectCreateEvent, out animationComponent, out enemyComponent);

                gameObjectCreateEvent.Transform = go.transform;
                gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                animationComponent.animator = go.GetComponent<Animator>();

                turnComponent = _world.AddComponent<TurnComponent>(enemy1);
                turnComponent.Initiative = initiative++;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < enemy2Count; i++)
            {
                var cell = VExt.NextFromList(emptyCells);

                go = LayoutAnimationObjects(prefabAnimation, cell.x, cell.y, "enemy2", gameObjectsRoot, LayersName.Character.ToString(), enemy2Animation);
                var enemy2 = _world.CreateEntityWith(out gameObjectCreateEvent, out animationComponent, out enemyComponent);

                gameObjectCreateEvent.Transform = go.transform;
                gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

                animationComponent.animator = go.GetComponent<Animator>();

                turnComponent = _world.AddComponent<TurnComponent>(enemy2);
                turnComponent.Initiative = initiative++;

                emptyCells.Remove(cell);
            }
        }

        private GameObject LayoutSpriteObjects(GameObject prefab, int x, int y, string name, Transform parent, string sortingLayer, Sprite sprite)
        {
            GameObject go = Object.Instantiate(prefab);
            go.transform.SetParent(parent);
            go.transform.localPosition = new Vector2(x, y);
            go.name = ($"{name}_(x{go.transform.localPosition.x}, y{go.transform.localPosition.y})");

            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = sortingLayer;
            sr.sprite = sprite;

            return go;
        }

        private GameObject LayoutAnimationObjects(GameObject prefab, int x, int y, string name, Transform parent, string sortingLayer, RuntimeAnimatorController controller)
        {
            GameObject go = LayoutSpriteObjects(prefab, x, y, name, parent, sortingLayer, null);

            Animator animator = go.GetComponent<Animator>();
            animator.runtimeAnimatorController = controller;

            return go;
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}