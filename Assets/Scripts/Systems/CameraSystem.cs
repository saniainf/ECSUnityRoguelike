using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class CameraSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;

        readonly GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");

        private Transform cameraTransform;

        void IEcsInitSystem.Initialize()
        {
            cameraTransform = Object.Instantiate(cameraPrefab).transform;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var playerPosition = _playerEntities.Components1[i].Transform.position;
                playerPosition.z = -100f;
                cameraTransform.position = playerPosition;
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}