using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class GameWorldSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectCreateEvent> _createEvents = null;
        readonly EcsFilter<GameObjectRemoveEvent, PositionComponent> _removeEvents = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _createEvents)
            {
                Transform newGO = _createEvents.Components1[i].Transform;
                Rigidbody2D newRB = _createEvents.Components1[i].Rigidbody;
                ref var newEntity = ref _createEvents.Entities[i];

                var c1 = _world.AddComponent<PositionComponent>(in newEntity);
                c1.Transform = newGO;
                c1.Rigidbody = newRB;
                c1.Coords.x = (int)newGO.transform.localPosition.x;
                c1.Coords.y = (int)newGO.transform.localPosition.y;
            }

            foreach (var i in _removeEvents)
            {
                Transform destroyGO = _removeEvents.Components2[i].Transform;
                ref var removeEntity = ref _removeEvents.Entities[i];
                destroyGO.gameObject.SetActive(false);
                _world.RemoveEntity(in removeEntity);
            }
        }
    }
}