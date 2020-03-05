using UnityEngine;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class LevelManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;
        readonly EntitiesPresetsInject _presets = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent>.Exclude<PlayerComponent> _transformEntities = null;

        private float loadLevelTime = 2f;
        private float gameOverTime = 2f;

        private GameLevel gameLevel = null;

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
            if (!Service<GameProps>.Get(true).GamePlay)
            {
                Service<GameProps>.Get().GamePlay = true;
                Service<GameProps>.Get().LevelNum = 1;

                Service<NPCDataSheet>.Set(new NPCDataSheet(
                    new NPCStats(_presets.Player.HealthPoint,
                                 _presets.Player.HealthPoint,
                                 _presets.Player.Initiative),
                    new NPCWeapon(_presets.Player.PrimaryWeaponItem, new WB_DamageOnContact()),
                    new NPCWeapon(_presets.Player.SecondaryWeaponItem, new WB_DamageOnContact())));
            }

            _worldStatus.GameStatus = GameStatus.LevelLoad;
        }

        void LevelLoad()
        {
            loadLevelTime -= Time.deltaTime;

            if (loadLevelTime <= 0f)
            {
                gameLevel = new GameLevel(_world, _presets);
                gameLevel.LevelCreate();
                gameLevel.SetActive(true);
                _worldStatus.GameStatus = GameStatus.LevelRun;
            }
        }

        void LevelEnd()
        {
            _worldStatus.GameStatus = GameStatus.LevelLoad;

            Service<GameProps>.Get().LevelNum += 1;

            foreach (var i in _playerEntities)
            {
                ref var e = ref _playerEntities.Entities[i];
                var c1 = _playerEntities.Get1[i];
                Service<NPCDataSheet>.Get().NPCStats = c1.Stats;
                Service<NPCDataSheet>.Get().NPCStats = c1.Stats;
                Service<NPCDataSheet>.Get().PriamaryWeapon = c1.PrimaryWeapon;
                Service<NPCDataSheet>.Get().SecondaryWeapon = c1.SecondaryWeapon;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void GameOver()
        {
            gameLevel.SetActive(false);
            gameOverTime -= Time.deltaTime;

            if (gameOverTime <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}