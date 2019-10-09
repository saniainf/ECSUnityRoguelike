using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UIEventsSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerDataEntities = null;
        readonly EcsFilter<UIComponent> _uiLevelRunEntities = null;

        readonly EcsFilter<UIEnableEvent> _uiEnableEvent = null;
        readonly EcsFilter<UIDisableEvent> _uiDisableEvent = null;

        readonly GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UI/UICanvas");
        readonly GameObject healthPointTextPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointText");
        readonly GameObject healthPointSliderPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointSlider");
        readonly GameObject levelLoadTextPrefab = Resources.Load<GameObject>("Prefabs/UI/LevelLoadText");
        readonly GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");

        private GameObject levelRunCanvas;
        private GameObject levelLoadCanvas;

        void IEcsInitSystem.Initialize()
        {
            levelRunCanvas = Object.Instantiate(canvasPrefab);
            var goSlider = Object.Instantiate(healthPointSliderPrefab, levelRunCanvas.transform);
            var goText = Object.Instantiate(healthPointTextPrefab, levelRunCanvas.transform);

            var goCamera = Object.Instantiate(cameraPrefab).transform;

            _world.CreateEntityWith(out UIComponent uiComponent);
            uiComponent.UIHPSlider = goSlider.GetComponent<UnityEngine.UI.Slider>();
            uiComponent.UIHPText = goText.GetComponent<UnityEngine.UI.Text>();

            _world.CreateEntityWith(out CameraComponent cameraComponent);
            cameraComponent.Transform = goCamera.transform;

            levelRunCanvas.SetActive(false);

            
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _uiEnableEvent)
            {
                var c1 = _uiEnableEvent.Components1[i];
                if (c1.UIType == UIType.LevelRun)
                {
                    levelRunCanvas.SetActive(true);
                }
            }

            foreach (var i in _uiDisableEvent)
            {
                var c1 = _uiDisableEvent.Components1[i];
                if (c1.UIType == UIType.LevelRun)
                {
                    levelRunCanvas.SetActive(false);
                }
            }

            foreach (var i in _playerDataEntities)
            {
                var pc1 = _playerDataEntities.Components1[i];

                foreach (var j in _uiLevelRunEntities)
                {
                    var uic1 = _uiLevelRunEntities.Components1[i];
                    var cur = pc1.CurrentHealthPoint;
                    var hp = pc1.HealthPoint;

                    uic1.UIHPText.text = $"HP: {cur} | {hp}";
                    uic1.UIHPSlider.value = (((float)pc1.CurrentHealthPoint / (float)pc1.HealthPoint) * 100f);
                }
            }
        }
        void IEcsInitSystem.Destroy()
        {

        }
    }
}