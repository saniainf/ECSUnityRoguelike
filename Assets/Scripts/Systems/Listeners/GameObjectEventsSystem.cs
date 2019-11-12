using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class GameObjectEventsSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectCreateEvent> _createEvents = null;
        readonly EcsFilter<GameObjectRemoveEvent, PositionComponent> _removeEvents = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _createEvents)
            {
                var newGO = _createEvents.Components1[i].Transform;
                var newRB = _createEvents.Components1[i].Rigidbody;
                var newCollider = _createEvents.Components1[i].Collider;
                ref var entity = ref _createEvents.Entities[i];

                var c = _world.AddComponent<PositionComponent>(entity);
                c.Transform = newGO;
                c.Rigidbody = newRB;
                c.Collider = newCollider;
                c.Coords.x = (int)newGO.transform.localPosition.x;
                c.Coords.y = (int)newGO.transform.localPosition.y;
            }

            foreach (var i in _removeEvents)
            {
                ref var entity = ref _removeEvents.Entities[i];
                var c1 = _removeEvents.Components1[i];
                var c2 = _removeEvents.Components2[i];

                if (c1.RemoveTime <= 0)
                {
                    var destroyGO = c2.Transform;
                    Object.Destroy(destroyGO.gameObject);
                    _world.RemoveComponent<GameObjectRemoveEvent>(entity);
                    _world.RemoveEntity(entity);
                }
                else
                {
                    c1.RemoveTime -= Time.deltaTime;
                }
            }
        }
    }
}