using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    [EcsInject]
    sealed class UISystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerDataEntities = null;
        readonly WorldStatus _worldStatus = null;

        readonly GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UI/UICanvas");
        readonly GameObject healthPointTextPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointText");
        readonly GameObject healthPointSliderPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointSlider");
        readonly GameObject levelLoadTextPrefab = Resources.Load<GameObject>("Prefabs/UI/LevelLoadText");
        readonly GameObject gameOverTextPrefab = Resources.Load<GameObject>("Prefabs/UI/GameOverText");

        private GameObject levelRunCanvas;
        private GameObject levelLoadCanvas;
        private GameObject gameOverCanvas;
        private Text UIHPText;
        private Slider UIHPSlider;
        private Text UILoadLevelText;
        private Text UIGameOverText;

        void IEcsInitSystem.Initialize()
        {
            levelRunCanvas = Object.Instantiate(canvasPrefab);
            UIHPSlider = Object.Instantiate(healthPointSliderPrefab, levelRunCanvas.transform).GetComponent<Slider>();
            UIHPText = Object.Instantiate(healthPointTextPrefab, levelRunCanvas.transform).GetComponent<Text>();
            levelRunCanvas.SetActive(false);

            levelLoadCanvas = Object.Instantiate(canvasPrefab);
            UILoadLevelText = Object.Instantiate(levelLoadTextPrefab, levelLoadCanvas.transform).GetComponent<Text>();
            levelLoadCanvas.SetActive(false);

            gameOverCanvas = Object.Instantiate(canvasPrefab);
            UIGameOverText = Object.Instantiate(gameOverTextPrefab, gameOverCanvas.transform).GetComponent<Text>();
            gameOverCanvas.SetActive(false);
        }

        void IEcsRunSystem.Run()
        {
            switch (_worldStatus.GameStatus)
            {
                case GameStatus.None:
                    break;
                case GameStatus.Start:
                    break;
                case GameStatus.LevelRun:
                    levelLoadCanvas.SetActive(false);
                    levelRunCanvas.SetActive(true);
                    break;
                case GameStatus.LevelLoad:
                    levelRunCanvas.SetActive(false);
                    levelLoadCanvas.SetActive(true);
                    UILoadLevelText.text = ($"Level {_worldStatus.LevelNum}");
                    break;
                case GameStatus.LevelEnd:
                    break;
                case GameStatus.GameOver:
                    levelRunCanvas.SetActive(false);
                    gameOverCanvas.SetActive(true);
                    break;
                default:
                    break;
            }

            foreach (var i in _playerDataEntities)
            {
                var pc1 = _playerDataEntities.Components1[i];
                var cur = pc1.CurrentHealthPoint;
                var hp = pc1.HealthPoint;

                UIHPText.text = $"HP: {cur} | {hp}";
                UIHPSlider.value = (((float)pc1.CurrentHealthPoint / (float)pc1.HealthPoint) * 100f);
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}