using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionMoveSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionMoveComponent, PositionComponent>.Exclude<GameObjectRemoveEvent> _moveEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _moveEntities)
            {
                ref var entity = ref _moveEntities.Entities[i];
                var c1 = _moveEntities.Components1[i];
                var c2 = _moveEntities.Components2[i];

                Vector2 newPosition = Vector2.MoveTowards(c2.Rigidbody.position, c1.EndPosition, c1.Speed * Time.deltaTime);
                c2.Rigidbody.MovePosition(newPosition);

                float sqrRemainingDistance = (c2.Rigidbody.position - c1.EndPosition).sqrMagnitude;
                if (sqrRemainingDistance < float.Epsilon)
                {
                    c2.Rigidbody.position = c1.EndPosition;
                    c2.Coords = c1.EndPosition;

                    _world.RemoveComponent<ActionMoveComponent>(entity);
                }
            }
        }
    }
}