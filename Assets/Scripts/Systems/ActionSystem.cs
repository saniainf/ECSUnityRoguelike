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

    enum ActionType
    {
        NONE,
        MOVE,
    }

    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, ActionPhaseComponent, InputDirectionComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;
        readonly EcsFilter<PositionComponent, WallComponent> _wallEntities = null;
        readonly EcsFilter<PositionComponent, EnemyComponent> _enemyEntities = null;
        readonly EcsFilter<PositionComponent, FoodComponent> _foodEntities = null;

        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;

        //TODO брать из настроек
        readonly float speed = 5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var entity = ref _actionPhaseEntities.Entities[i];
                ref var c1 = ref _actionPhaseEntities.Components1[i];
                ref var c2 = ref _actionPhaseEntities.Components2[i];
                ref var c3 = ref _actionPhaseEntities.Components3[i];

                SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                Vector2Int endPosition = Vector2Int.zero;

                switch (c3.MoveDirection)
                {
                    case MoveDirection.UP:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y + 1);
                        CreateMoveEntity(entity, endPosition);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        break;
                    case MoveDirection.DOWN:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y - 1);
                        CreateMoveEntity(entity, endPosition);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        break;
                    case MoveDirection.LEFT:
                        endPosition = new Vector2Int(c1.Coords.x - 1, c1.Coords.y);
                        CreateMoveEntity(entity, endPosition);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        sr.flipX = true;
                        break;
                    case MoveDirection.RIGHT:
                        endPosition = new Vector2Int(c1.Coords.x + 1, c1.Coords.y);
                        CreateMoveEntity(entity, endPosition);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        sr.flipX = false;
                        break;
                    default:
                        break;
                }
            }

            if (_moveEntities.GetEntitiesCount() == 0)
            {
                foreach (var i in _actionPhaseEntities)
                {
                    _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);
                }
            }
        }

        void CreateMoveEntity(EcsEntity entity, Vector2Int endPosition)
        {
            var c = _world.AddComponent<ActionMoveComponent>(in entity);
            c.EndPosition = endPosition;
            c.Speed = speed;
        }
    }
}
