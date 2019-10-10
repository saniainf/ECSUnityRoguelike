using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    [EcsInject]
    sealed class UIEventsSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerDataEntities = null;

        readonly EcsFilter<UIEnableEvent> _uiEnableEvent = null;
        readonly EcsFilter<UIDisableEvent> _uiDisableEvent = null;

        readonly GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UI/UICanvas");
        readonly GameObject healthPointTextPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointText");
        readonly GameObject healthPointSliderPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointSlider");
        readonly GameObject levelLoadTextPrefab = Resources.Load<GameObject>("Prefabs/UI/LevelLoadText");

        private GameObject levelRunCanvas;
        private GameObject levelLoadCanvas;
        private Text UIHPText;
        private Slider UIHPSlider;
        private Text UILoadLevelText;

        void IEcsInitSystem.Initialize()
        {
            levelRunCanvas = Object.Instantiate(canvasPrefab);
            UIHPSlider = Object.Instantiate(healthPointSliderPrefab, levelRunCanvas.transform).GetComponent<Slider>();
            UIHPText = Object.Instantiate(healthPointTextPrefab, levelRunCanvas.transform).GetComponent<Text>();
            levelRunCanvas.SetActive(false);

            levelLoadCanvas = Object.Instantiate(canvasPrefab);
            UILoadLevelText = Object.Instantiate(levelLoadTextPrefab, levelLoadCanvas.transform).GetComponent<Text>();
            levelLoadCanvas.SetActive(false);
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _uiEnableEvent)
            {
                var c1 = _uiEnableEvent.Components1[i];

                switch (c1.UIType)
                {
                    case UIType.None:
                        break;
                    case UIType.LevelRun:
                        levelRunCanvas.SetActive(true);
                        break;
                    case UIType.LevelLoad:
                        levelLoadCanvas.SetActive(true);
                        UILoadLevelText.text = ($"Level {c1.LevelNumber.ToString()}");
                        break;
                    default:
                        break;
                }
            }

            foreach (var i in _uiDisableEvent)
            {
                var c1 = _uiDisableEvent.Components1[i];

                switch (c1.UIType)
                {
                    case UIType.None:
                        break;
                    case UIType.LevelRun:
                        levelRunCanvas.SetActive(false);
                        break;
                    case UIType.LevelLoad:
                        levelLoadCanvas.SetActive(false);
                        break;
                    default:
                        break;
                }
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