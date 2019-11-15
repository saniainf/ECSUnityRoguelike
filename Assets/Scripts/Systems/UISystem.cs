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

            levelLoadCanvas = Object.Instantiate(canvasPrefab);
            UILoadLevelText = Object.Instantiate(levelLoadTextPrefab, levelLoadCanvas.transform).GetComponent<Text>();

            gameOverCanvas = Object.Instantiate(canvasPrefab);
            UIGameOverText = Object.Instantiate(gameOverTextPrefab, gameOverCanvas.transform).GetComponent<Text>();

            SetActive(false);
        }

        void IEcsRunSystem.Run()
        {
            switch (_worldStatus.GameStatus)
            {
                case GameStatus.LevelRun:
                    SetActive(false);
                    levelRunCanvas.SetActive(true);
                    break;
                case GameStatus.LevelLoad:
                    SetActive(false);
                    levelLoadCanvas.SetActive(true);
                    UILoadLevelText.text = ($"Level {_worldStatus.LevelNum}");
                    break;
                case GameStatus.GameOver:
                    SetActive(false);
                    gameOverCanvas.SetActive(true);
                    break;
                default:
                    break;
            }

            foreach (var i in _playerDataEntities)
            {
                var pc1 = _playerDataEntities.Components1[i];
                var cur = Mathf.Max(0, pc1.HealthPoint);
                var hp = pc1.MaxHealthPoint;

                UIHPText.text = $"HP: {cur} | {hp}";
                UIHPSlider.value = (((float)pc1.HealthPoint / (float)pc1.MaxHealthPoint) * 100f);
            }
        }

        void SetActive(bool value)
        {
            levelLoadCanvas.SetActive(value);
            levelRunCanvas.SetActive(value);
            gameOverCanvas.SetActive(value);
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}