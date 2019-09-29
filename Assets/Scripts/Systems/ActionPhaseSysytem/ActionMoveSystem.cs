using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionMoveSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionMoveComponent>.Exclude<GameObjectRemoveEvent> _moveEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _moveEntities)
            {
                var rb = _moveEntities.Components1[i].Rigidbody;
                var endPosition = _moveEntities.Components1[i].EndPosition;
                var speed = _moveEntities.Components1[i].Speed;

                Vector2 newPostion = Vector2.MoveTowards(rb.position, endPosition, speed * Time.deltaTime);
                rb.MovePosition(newPostion);

                float sqrRemainingDistance = (rb.position - endPosition).sqrMagnitude;
                if (sqrRemainingDistance < float.Epsilon)
                {
                    rb.position = endPosition;
                    _world.RemoveComponent<ActionMoveComponent>(_moveEntities.Entities[i]);
                }
            }
        }
    }
}