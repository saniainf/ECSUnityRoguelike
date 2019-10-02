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
                var newGO = _createEvents.Components1[i].Transform;
                var newRB = _createEvents.Components1[i].Rigidbody;
                ref var entity = ref _createEvents.Entities[i];

                var c = _world.AddComponent<PositionComponent>(entity);
                c.Transform = newGO;
                c.Rigidbody = newRB;
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
                    destroyGO.gameObject.SetActive(false);
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