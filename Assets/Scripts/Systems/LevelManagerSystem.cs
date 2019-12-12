using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// ��������, �������� ������. ���������� ������ ����� ��������
    /// </summary>

    sealed class LevelManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent>.Exclude<PlayerComponent> _transformEntities = null;

        private int levelNum = 1;
        private float loadLevelTime = 2f;
        private float loadLevelCurrentTime = 0f;
        private float gameOverTime = 2f;
        private float gameOvetCurrentTime = 0f;

        private GameLevel gameLevel = null;

        private NPCDataSheet playerData;

        void IEcsInitSystem.Init()
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
            playerData = new NPCDataSheet(
                new NPCStats(ObjData.p_PlayerPreset.HealthPoint,
                             ObjData.p_PlayerPreset.HealthPoint,
                             ObjData.p_PlayerPreset.Initiative),
                new NPCWeapon(ObjData.p_PlayerPreset.PrimaryWeaponItem, new WB_DamageOnContact()),
                new NPCWeapon(ObjData.p_PlayerPreset.SecondaryWeaponItem, new WB_DamageOnContact()));

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
                gameLevel = new GameLevel(_world, playerData);
                gameLevel.LevelCreate();
                gameLevel.SetActive(true);
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
                e.RLDestoryGO();
            }

            foreach (var i in _playerEntities)
            {
                ref var e = ref _playerEntities.Entities[i];
                var c1 = _playerEntities.Get1[i];
                playerData.NPCStats = c1.Stats;
                playerData.PriamaryWeapon = c1.PrimaryWeapon;
                playerData.SecondaryWeapon = c1.SecondaryWeapon;
                e.RLDestoryGO();
            }
        }
    }
}