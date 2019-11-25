using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление камерой
    /// </summary>
    
    sealed class CameraSystem : IEcsRunSystem, IEcsInitSystem
    {
        //UNDONE camera
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;

        readonly GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");

        private Transform cameraTransform;

        void IEcsInitSystem.Init()
        {
            cameraTransform = Object.Instantiate(cameraPrefab).transform;
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                var playerPosition = _playerEntities.Get1[i].Transform.position;
                playerPosition.z = -100f;
                cameraTransform.position = playerPosition;
            }
        }
    }
}