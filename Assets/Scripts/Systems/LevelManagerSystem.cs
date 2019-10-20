using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class LevelManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly WorldStatus _worldStatus = null;

        private int levelNum = 1;
        private float loadLevelTime = 2f;
        private float loadLevelCurrentTime = 0f;

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
            _worldStatus.GameStatus = GameStatus.LevelLoad;
            _worldStatus.LevelNum = levelNum++;
            loadLevelCurrentTime = loadLevelTime;
        }

        void LevelLoad()
        {
            loadLevelCurrentTime -= Time.deltaTime;
            if (loadLevelCurrentTime <= 0f)
            {
                _worldStatus.GameStatus = GameStatus.LevelRun;
                _world.CreateEntityWith<WorldCreateEvent>(out _);
            }
        }

        void LevelEnd()
        {
            _worldStatus.GameStatus = GameStatus.LevelLoad;
            _worldStatus.LevelNum = levelNum++;
            loadLevelCurrentTime = loadLevelTime;
            _world.CreateEntityWith<WorldDestroyEvent>(out _);
        }

        void GameOver()
        {
            _world.CreateEntityWith<WorldDestroyEvent>(out _);
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}