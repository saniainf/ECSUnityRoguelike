using Leopotam.Ecs;
using UnityEngine;

namespace Client
{

    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _player = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        void IEcsRunSystem.Run()
        {
            var worldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        }

        void IEcsInitSystem.Init()
        {

        }

    }
}