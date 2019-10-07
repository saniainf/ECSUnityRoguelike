using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class CameraSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<CameraComponent> _cameraEnities = null;
        readonly EcsFilter<PositionComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;

        float speed = 10f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _cameraEnities)
            {
                var cc1 = _cameraEnities.Components1[i];

                foreach (var j in _playerEntities)
                {
                    var playerPosition = _playerEntities.Components1[i].Transform.position;
                    playerPosition.z = -100f;
                    cc1.Transform.position = playerPosition;
                }

            }
        }
    }
}