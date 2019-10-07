using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UISystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<DataSheetComponent, PlayerComponent> _playerDataEntities = null;
        readonly EcsFilter<UIComponent> _uiEntities = null;

        readonly GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UICanvas");
        readonly GameObject healtPointTextPrefab = Resources.Load<GameObject>("Prefabs/HealtPointText");
        readonly GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");

        void IEcsInitSystem.Initialize()
        {
            var goCanvas = Object.Instantiate(canvasPrefab);
            var goText = Object.Instantiate(healtPointTextPrefab, goCanvas.transform);
            var goCamera = Object.Instantiate(cameraPrefab).transform;

            _world.CreateEntityWith(out UIComponent uiComponent);
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

                    uic1.UIText.text = "LIVE: " + pc1.HealthPoint.ToString();
                }
            }
        }
        void IEcsInitSystem.Destroy()
        {

        }
    }
}