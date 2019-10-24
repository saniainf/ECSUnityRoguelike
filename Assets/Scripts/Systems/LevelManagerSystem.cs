using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class LevelManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;
        readonly WorldObjects _worldObjects = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<PositionComponent>.Exclude<PlayerComponent> _transformEntities = null;

        private int levelNum = 1;
        private float loadLevelTime = 2f;
        private float loadLevelCurrentTime = 0f;
        private float gameOverTime = 2f;
        private float gameOvetCurrentTime = 0f;
        private GameLevel gameLevel = null;

        (int HP, int currentHP, int hitDamage, int initiative) startPlayerSet = (3, 3, 1, 10);
        (int HP, int currentHP, int hitDamage, int initiative) playerSet;

        void IEcsInitSystem.Initialize()
        {
            _worldStatus.GameStatus = GameStatus.Start;
        }

        void IEcsRunSystem.Run()
        {
            switch (_worldStatus.GameStatus)
            {
                case GameStatus.None:
                    break;
                case GameStatus.Start:
                    StartGame();
                    break;
                case GameStatus.LevelRun:
                    break;
                case GameStatus.LevelLoad:
                    LevelLoad();
                    break;
                case GameStatus.LevelEnd:
                    LevelEnd();
                    break;
                case GameStatus.GameOver:
                    GameOver();
                    break;
                default:
                    break;
            }
        }

        void StartGame()
        {
            levelNum = 1;
            playerSet = startPlayerSet;
            _worldStatus.GameStatus = GameStatus.LevelLoad;
            _worldStatus.LevelNum = levelNum++;
            loadLevelCurrentTime = loadLevelTime;
            gameOvetCurrentTime = gameOverTime;
        }

        void LevelLoad()
        {
            loadLevelCurrentTime -= Time.deltaTime;

            if (loadLevelCurrentTime <= 0f)
            {
                gameLevel = new GameLevel(_world, _worldObjects, playerSet);
                gameLevel.LevelCreate();
                gameLevel.SetActive(true);
                _worldStatus.ParentOtherObject = gameLevel.GameObjectsOther;
                _worldStatus.GameStatus = GameStatus.LevelRun;
            }
        }

        void LevelEnd()
        {
            _worldStatus.GameStatus = GameStatus.LevelLoad;
            _worldStatus.LevelNum = levelNum++;
            loadLevelCurrentTime = loadLevelTime;

            if (gameLevel != null)
            {
                ClearWorld();
                gameLevel.SetActive(false);
                gameLevel.LevelDestroy();
                gameLevel = null;
            }
        }

        void GameOver()
        {
            if (gameLevel != null)
            {
                ClearWorld();
                gameLevel.SetActive(false);
                gameLevel.LevelDestroy();
                gameLevel = null;
            }

            gameOvetCurrentTime -= Time.deltaTime;

            if (gameOvetCurrentTime <= 0f)
            {
                _worldStatus.GameStatus = GameStatus.Start;
            }
        }

        void ClearWorld()
        {
            foreach (var i in _transformEntities)
            {
                ref var e = ref _transformEntities.Entities[i];
                _world.RemoveEntity(e);
            }

            foreach (var i in _playerEntities)
            {
                ref var e = ref _playerEntities.Entities[i];
                var c1 = _playerEntities.Components1[i];
                playerSet.HP = c1.HealthPoint;
                playerSet.currentHP = c1.CurrentHealthPoint;
                playerSet.hitDamage = c1.HitDamage;
                _world.RemoveEntity(e);
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}