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
                ref var e = ref _moveEntities.Entities[i];
                var c1 = _moveEntities.Components1[i];
                var c2 = _moveEntities.Components2[i];

                if (!c1.Run)
                {
                    c1.StartPosition = c2.Rigidbody.position;
                    c1.Run = true;
                }

                if (c1.Run)
                {
                    if (c1.SqrDistance > (c1.DestroyDistance * c1.DestroyDistance))
                    {
                        Debug.Log($"{c1.SqrDistance}    {c1.DestroyDistance * c1.DestroyDistance}");
                        _world.RemoveComponent<ActionMoveComponent>(e);
                    }
                }

                if (c1.GoalInt != Vector2Int.zero)
                {
                    var nextPosition = Vector2.MoveTowards(c2.Rigidbody.position, c1.GoalInt, c1.Speed * Time.deltaTime);
                    c2.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.Rigidbody.position - c1.StartPosition).sqrMagnitude;

                    float sqrDistanceToGoal = (c2.Rigidbody.position - c1.GoalInt).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c2.Rigidbody.position = c1.GoalInt;
                        c2.Coords = c1.GoalInt;

                        _world.RemoveComponent<ActionMoveComponent>(e);
                    }
                }

                else if (c1.GoalFloat != Vector2.zero)
                {
                    var nextPosition = Vector2.MoveTowards(c2.Rigidbody.position, c1.GoalFloat, c1.Speed * Time.deltaTime);
                    c2.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.Rigidbody.position - c1.StartPosition).sqrMagnitude;

                    float sqrDistanceToGoal = (c2.Rigidbody.position - c1.GoalFloat).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c2.Rigidbody.position = c1.GoalFloat;
                        c2.Coords = c1.GoalFloat;

                        _world.RemoveComponent<ActionMoveComponent>(e);
                    }
                }

                else if (c1.GoalDirection != Vector2.zero)
                {
                    var nextPosition = c2.Rigidbody.position + (c1.GoalDirection * (c1.Speed * Time.deltaTime));
                    c2.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.Rigidbody.position - c1.StartPosition).sqrMagnitude;
                }
            }
        }
    }
}