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

        readonly EcsFilter<ActionPhaseComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;
        readonly EcsFilter<PositionComponent, InputDirectionComponent>.Exclude<GameObjectRemoveEvent> _inputEntities = null;
        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;

        readonly EcsFilter<PositionComponent, WallComponent> _wallEntities = null;

        //TODO брать из настроек
        readonly float speed = 5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputEntities)
            {
                ref var entity = ref _inputEntities.Entities[i];
                ref var c1 = ref _inputEntities.Components1[i];
                ref var c2 = ref _inputEntities.Components2[i];

                SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                Vector2Int endPosition = Vector2Int.zero;

                switch (c2.MoveDirection)
                {
                    case MoveDirection.UP:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y + 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CheckNewPositon(entity, endPosition);
                        break;
                    case MoveDirection.DOWN:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y - 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CheckNewPositon(entity, endPosition);
                        break;
                    case MoveDirection.LEFT:
                        endPosition = new Vector2Int(c1.Coords.x - 1, c1.Coords.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CheckNewPositon(entity, endPosition);
                        sr.flipX = true;
                        break;
                    case MoveDirection.RIGHT:
                        endPosition = new Vector2Int(c1.Coords.x + 1, c1.Coords.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CheckNewPositon(entity, endPosition);
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
                    _world.AddComponent<PhaseEndEvent>(_actionPhaseEntities.Entities[i]);
                }
            }
        }

        void CheckNewPositon(EcsEntity entity, Vector2Int endPosition)
        {
            foreach (var i in _wallEntities)
            {
                ref var c1 = ref _wallEntities.Components1[i];
                ref var c2 = ref _wallEntities.Components2[i];

                if (endPosition == c1.Coords)
                {
                    _world.AddComponent<PhaseEndEvent>(entity);
                    _world.GetComponent<TurnComponent>(entity).ReturnInput = true;
                    return;
                }
            }
            CreateMoveEntity(entity, endPosition);
        }

        void CreateMoveEntity(EcsEntity entity, Vector2Int endPosition)
        {
            var c = _world.AddComponent<ActionMoveComponent>(entity);
            c.EndPosition = endPosition;
            c.Speed = speed;
        }
    }
}
