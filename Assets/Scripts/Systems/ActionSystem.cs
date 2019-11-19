using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    enum MoveDirection
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionPhaseComponent> _actionPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, InputDirectionComponent> _inputEntities = null;

        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;
        readonly EcsFilter<ActionAtackComponent> _atackEntities = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        //TODO брать из настроек
        readonly float speed = 7f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputEntities)
            {
                ref var entity = ref _inputEntities.Entities[i];
                var c1 = _inputEntities.Components1[i];
                var c2 = _inputEntities.Components2[i];

                SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                Vector2 goalPosition;

                switch (c2.MoveDirection)
                {
                    case MoveDirection.UP:
                        goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y + 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, goalPosition);
                        break;
                    case MoveDirection.DOWN:
                        goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y - 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, goalPosition);
                        break;
                    case MoveDirection.LEFT:
                        goalPosition = new Vector2(c1.Transform.position.x - 1, c1.Transform.position.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, goalPosition);
                        sr.flipX = true;
                        break;
                    case MoveDirection.RIGHT:
                        goalPosition = new Vector2(c1.Transform.position.x + 1, c1.Transform.position.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, goalPosition);
                        sr.flipX = false;
                        break;
                    case MoveDirection.NONE:
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        break;
                    default:
                        break;
                }
            }

            if (_moveEntities.GetEntitiesCount() == 0 && _animationEntities.GetEntitiesCount() == 0 && _atackEntities.GetEntitiesCount() == 0)
            {
                foreach (var i in _actionPhaseEntities)
                {
                    var c1 = _actionPhaseEntities.Components1[i];
                    c1.PhaseEnd = true;
                }
            }
        }

        void CreateAction(EcsEntity entity, Vector2 goalPosition)
        {
            if (!CheckObstacleCollision(entity, goalPosition) && !CheckCollision(entity, goalPosition))
            {
                MoveEntity(entity, goalPosition);
            }
        }

        bool CheckObstacleCollision(EcsEntity entity, Vector2 goalPosition)
        {
            bool result = false;

            foreach (var i in _obstacleEntities)
            {
                ref var wallEntity = ref _obstacleEntities.Entities[i];
                var c1 = _obstacleEntities.Components1[i];

                if (c1.Collider.OverlapPoint(goalPosition))
                {
                    result = true;
                    _world.GetComponent<TurnComponent>(entity).ReturnInput = true;
                }
            }
            return result;
        }

        bool CheckCollision(EcsEntity entity, Vector2 goalPosition)
        {
            bool result = false;

            foreach (var i in _collisionEntities)
            {
                ref var ce = ref _collisionEntities.Entities[i];
                var c1 = _collisionEntities.Components1[i];

                if (c1.Collider.OverlapPoint(goalPosition))
                {
                    result = true;

                    var c = _world.AddComponent<ActionAtackComponent>(entity);
                    c.TargetPosition = goalPosition;
                    c.Target = ce;
                }
            }

            return result;
        }

        void MoveEntity(EcsEntity entity, Vector2 goalPosition)
        {
            var c = _world.EnsureComponent<ActionMoveComponent>(entity, out _);
            c.GoalInt = goalPosition.ToInt2();
            c.Speed = speed;
        }
    }
}
