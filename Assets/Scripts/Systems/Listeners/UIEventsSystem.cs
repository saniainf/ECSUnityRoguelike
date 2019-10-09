using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UIEventsSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerDataEntities = null;
        readonly EcsFilter<UIComponent> _uiEntities = null;

        readonly GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UI/UICanvas");
        readonly GameObject healthPointTextPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointText");
        readonly GameObject healthPointSliderPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthPointSlider");
        readonly GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");

        void IEcsInitSystem.Initialize()
        {
            var goCanvas = Object.Instantiate(canvasPrefab);
            var goSlider = Object.Instantiate(healthPointSliderPrefab, goCanvas.transform);
            var goText = Object.Instantiate(healthPointTextPrefab, goCanvas.transform);

            var goCamera = Object.Instantiate(cameraPrefab).transform;

            _world.CreateEntityWith(out UIComponent uiComponent);
            uiComponent.UISlider = goSlider.GetComponent<UnityEngine.UI.Slider>();
            uiComponent.UIText = goText.GetComponent<UnityEngine.UI.Text>();

            _world.CreateEntityWith(out CameraComponent cameraComponent);
            cameraComponent.Transform = goCamera.transform;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerDataEntities)
            {
                var pc1 = _playerDataEntities.Components1[i];

                foreach (var j in _uiEntities)
                {
                    var uic1 = _uiEntities.Components1[i];
                    var cur = pc1.CurrentHealthPoint;
                    var hp = pc1.HealthPoint;

                    uic1.UIText.text = $"HP: {cur} | {hp}";
                    uic1.UISlider.value = (((float)pc1.CurrentHealthPoint / (float)pc1.HealthPoint) * 100f);
                }
            }
        }
        void IEcsInitSystem.Destroy()
        {

        }
    }
}