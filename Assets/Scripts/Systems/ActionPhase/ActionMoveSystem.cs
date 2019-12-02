using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление передвижением чара в фазу действия
    /// </summary>

    sealed class ActionMoveSystem : IEcsRunSystem
    {
        float SPEED = 7f;

        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionMoveComponent, TurnComponent, GameObjectComponent> _moveEntities = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _moveEntities)
            {
                ref var e = ref _moveEntities.Entities[i];
                var c1 = _moveEntities.Get1[i];
                var c2 = _moveEntities.Get2[i];
                var c3 = _moveEntities.Get3[i];

                if (!c1.Run && CheckObstacleCollision(c1.GoalPosition))
                {
                    //TODO проверка на возврат хода при столкновении с препядствием
                    if (true)
                        c2.ReturnInput = true;

                    e.Unset<ActionMoveComponent>();
                    continue;
                }

                if (!c1.Run && CheckCollision(c1.GoalPosition, out EcsEntity target))
                {
                    // TODO проверка на атаку при движении
                    if (true)
                    {
                        var c = e.Set<ActionAtackComponent>();
                        c.Target = target;
                        c.TargetPosition = c1.GoalPosition;
                    }

                    e.Unset<ActionMoveComponent>();
                    continue;
                }

                if (!c1.Run)
                {
                    c1.StartPosition = c3.GObj.Rigidbody.position;
                    c1.Run = true;

                    Debug.Log($"entity: {e.GetInternalId()} | запущено action смещение в: {c1.GoalPosition.x}, {c1.GoalPosition.y}");
                }

                if (c1.Run)
                {
                    var nextPosition = Vector2.MoveTowards(c3.GObj.Rigidbody.position, c1.GoalPosition, SPEED * Time.deltaTime);
                    c3.GObj.Rigidbody.MovePosition(nextPosition);

                    float sqrDistanceToGoal = (c3.GObj.Rigidbody.position - c1.GoalPosition).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c3.GObj.Rigidbody.position = c1.GoalPosition;

                        e.Unset<ActionMoveComponent>();
                    }
                }
            }
        }

        bool CheckObstacleCollision(Vector2 goalPosition)
        {
            foreach (var i in _obstacleEntities)
            {
                ref var wallEntity = ref _obstacleEntities.Entities[i];
                var c1 = _obstacleEntities.Get1[i];

                if (c1.GObj.Collider.OverlapPoint(goalPosition))
                {
                    return true;
                }
            }
            return false;
        }

        bool CheckCollision(Vector2 goalPosition, out EcsEntity target)
        {
            target = EcsEntity.Null;

            foreach (var i in _collisionEntities)
            {
                ref var ce = ref _collisionEntities.Entities[i];
                var c1 = _collisionEntities.Get1[i];

                if (c1.GObj.Collider.OverlapPoint(goalPosition))
                {
                    target = ce;
                    return true;
                }
            }
            return false;
        }
    }
}