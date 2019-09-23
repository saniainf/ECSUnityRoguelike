using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class BuildLevel : IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly InjectFields _injectFields = null;

        EcsFilter<Position> _positions = null;
        EcsFilter<Wall> _wall = null;
        EcsFilter<Animation> _player = null;

        Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet");
        GameObject prefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");
        GameObject prefabAnimation = Resources.Load<GameObject>("Prefabs/PrefabAnimation");
        RuntimeAnimatorController playerAnimation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/PlayerAnimatorController");
        RuntimeAnimatorController enemyAnimation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/EnemyAnimatorController");
        RuntimeAnimatorController enemy2Animation = Resources.Load<RuntimeAnimatorController>("Animations/AnimatorControllers/Enemy2AnimatorController");

        Transform gameBoardRoot = new GameObject("GameBoardRoot").transform;
        Transform gameObjectsRoot = new GameObject("GameObjectsRoot").transform;

        char[,] levelArray = new char[,]{
            { '8','8','8','8','8','8','8','8','8','8' },
            { '8','@','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','8','8','8','8','.','.','.','.','8' },
            { '8','.','.','.','.','.','.','.','.','8' },
            { '8','.','.','.','8','.','.','.','.','8' },
            { '8','.','.','.','8','.','.','.','.','8' },
            { '8','8','8','8','8','8','8','8','8','8' }};

        int sodaCount = 3;
        int appleCount = 1;
        int wallCount = 3;
        int enemyCount = 2;
        int enemy2Count = 1;

        int appleFoodValue = 20;
        int sodaFoodValue = 10;

        int minWallHP = 1;
        int maxWallHP = 4;

        Sprite[] solidWallSprites;
        Sprite[] softWall;
        Sprite[] softWallDamage;
        Sprite[] floorSprites;
        Sprite sodaSprite;
        Sprite appleSprite;

        void IEcsInitSystem.Initialize()
        {
            solidWallSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 25, 26, 28, 29 });
            softWall = VExt.ExtractSubArray(spriteSheet, new int[] { 21, 22, 23, 24, 27, 30, 31 });
            softWallDamage = VExt.ExtractSubArray(spriteSheet, new int[] { 48, 49, 50, 51, 52, 53, 54 });
            floorSprites = VExt.ExtractSubArray(spriteSheet, new int[] { 32, 33, 34, 35, 36, 37, 38, 39 });
            sodaSprite = spriteSheet[18];
            appleSprite = spriteSheet[19];

            List<Coords> emptyCells = new List<Coords>();

            GameObject go;
            Coords coords = new Coords();
            Position positionComponent;
            Wall wallComponent;
            Food foodComponent;
            Animation animationComponent;
            Specify specifyComponent;

            VExt.ReverseArray(ref levelArray);

            for (int i = 0; i < levelArray.GetLength(0); i++)
                for (int j = 0; j < levelArray.GetLength(1); j++)
                {
                    go = LayoutSpriteObjects(prefabSprite, j, i, "floor", gameBoardRoot, LayersName.Floor.ToString(), VExt.NextFromArray(floorSprites));
                    _world.CreateEntityWith(out positionComponent);
                    positionComponent.Transform = go.transform;
                    positionComponent.Coords.X = (int)go.transform.localPosition.x;
                    positionComponent.Coords.Y = (int)go.transform.localPosition.y;

                    switch (levelArray[i, j])
                    {
                        case '.':
                            coords.X = j;
                            coords.Y = i;
                            emptyCells.Add(coords);
                            break;
                        case '8':
                            go = LayoutSpriteObjects(prefabSprite, j, i, "solidWall", gameBoardRoot, LayersName.Wall.ToString(), VExt.NextFromArray(solidWallSprites));
                            _world.CreateEntityWith(out positionComponent, out wallComponent);
                            positionComponent.Transform = go.transform;
                            positionComponent.Coords.X = (int)go.transform.localPosition.x;
                            positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                            wallComponent.Solid = true;
                            break;
                        case '@':
                            go = LayoutAnimationObjects(prefabAnimation, j, i, "player", gameObjectsRoot, LayersName.Object.ToString(), playerAnimation);
                            EcsEntity playerEntity = _world.CreateEntityWith(out positionComponent, out animationComponent, out specifyComponent);
                            _world.AddComponent<Player>(in playerEntity);
                            positionComponent.Transform = go.transform;
                            positionComponent.Coords.X = (int)go.transform.localPosition.x;
                            positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                            positionComponent.Rigidbody = go.GetComponent<Rigidbody2D>();
                            animationComponent.animator = go.GetComponent<Animator>();
                            _injectFields.thisTurnEntity = playerEntity;
                            break;
                        default:
                            break;
                    }
                }
            for (int i = 0; i < appleCount; i++)
            {
                Coords cell = VExt.NextFromList(emptyCells);

                go = LayoutSpriteObjects(prefabSprite, cell.X, cell.Y, "apple", gameObjectsRoot, LayersName.Object.ToString(), appleSprite);
                _world.CreateEntityWith(out positionComponent, out foodComponent);
                positionComponent.Transform = go.transform;
                positionComponent.Coords.X = (int)go.transform.localPosition.x;
                positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                foodComponent.foodValue = appleFoodValue;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < sodaCount; i++)
            {
                Coords cell = VExt.NextFromList(emptyCells);

                go = LayoutSpriteObjects(prefabSprite, cell.X, cell.Y, "soda", gameObjectsRoot, LayersName.Object.ToString(), sodaSprite);
                _world.CreateEntityWith(out positionComponent, out foodComponent);
                positionComponent.Transform = go.transform;
                positionComponent.Coords.X = (int)go.transform.localPosition.x;
                positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                foodComponent.foodValue = sodaFoodValue;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < wallCount; i++)
            {
                Coords cell = VExt.NextFromList(emptyCells);
                int indxSprite = Random.Range(0, softWall.Length);

                go = LayoutSpriteObjects(prefabSprite, cell.X, cell.Y, "softWall", gameObjectsRoot, LayersName.Object.ToString(), softWall[indxSprite]);
                _world.CreateEntityWith(out positionComponent, out wallComponent);
                positionComponent.Transform = go.transform;
                positionComponent.Coords.X = (int)go.transform.localPosition.x;
                positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                wallComponent.Solid = false;
                wallComponent.HealthPoint = Random.Range(minWallHP, maxWallHP + 1);
                wallComponent.damageSprite = softWallDamage[indxSprite];

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < enemyCount; i++)
            {
                Coords cell = VExt.NextFromList(emptyCells);

                go = LayoutAnimationObjects(prefabAnimation, cell.X, cell.Y, "enemy", gameObjectsRoot, LayersName.Object.ToString(), enemyAnimation);
                EcsEntity enemy1 = _world.CreateEntityWith(out positionComponent, out animationComponent, out specifyComponent);
                _world.AddComponent<Enemy>(in enemy1);
                positionComponent.Transform = go.transform;
                positionComponent.Coords.X = (int)go.transform.localPosition.x;
                positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                animationComponent.animator = go.GetComponent<Animator>();
                specifyComponent.MoveDirection = MoveDirection.NONE;

                emptyCells.Remove(cell);
            }

            for (int i = 0; i < enemy2Count; i++)
            {
                Coords cell = VExt.NextFromList(emptyCells);

                go = LayoutAnimationObjects(prefabAnimation, cell.X, cell.Y, "enemy2", gameObjectsRoot, LayersName.Object.ToString(), enemy2Animation);
                EcsEntity enemy2 = _world.CreateEntityWith(out positionComponent, out animationComponent, out specifyComponent);
                _world.AddComponent<Enemy>(in enemy2);
                positionComponent.Transform = go.transform;
                positionComponent.Coords.X = (int)go.transform.localPosition.x;
                positionComponent.Coords.Y = (int)go.transform.localPosition.y;
                animationComponent.animator = go.GetComponent<Animator>();
                specifyComponent.MoveDirection = MoveDirection.NONE;

                emptyCells.Remove(cell);
            }
        }

        private GameObject LayoutSpriteObjects(GameObject prefab, int x, int y, string name, Transform parent, string sortingLayer, Sprite sprite)
        {
            GameObject go = Object.Instantiate(prefab);
            go.transform.SetParent(parent);
            go.transform.localPosition = new Vector2(x, y);
            go.name = ($"{name}_{go.transform.localPosition.x}_{go.transform.localPosition.y}");

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
            foreach (int i in _positions)
            {
                _positions.Components1[i].Transform = null;
                _positions.Components1[i].Rigidbody = null;
            }

            foreach (int i in _wall)
            {
                _wall.Components1[i].damageSprite = null;
            }

            foreach (int i in _player)
            {
                _player.Components1[i].animator = null;
            }
        }
    }
}