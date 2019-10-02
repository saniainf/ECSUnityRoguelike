using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    enum MoveDirection
    {
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
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;

        readonly EcsFilter<PositionComponent, WallComponent>.Exclude<GameObjectRemoveEvent> _wallEntities = null;
        readonly EcsFilter<PositionComponent, EnemyComponent>.Exclude<GameObjectRemoveEvent> _enemyEntities = null;
        readonly EcsFilter<PositionComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;

        //TODO брать из настроек
        readonly float speed = 5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputEntities)
            {
                ref var entity = ref _inputEntities.Entities[i];
                var c1 = _inputEntities.Components1[i];
                var c2 = _inputEntities.Components2[i];

                SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                Vector2Int endPosition;

                switch (c2.MoveDirection)
                {
                    case MoveDirection.UP:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y + 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, endPosition);
                        break;
                    case MoveDirection.DOWN:
                        endPosition = new Vector2Int(c1.Coords.x, c1.Coords.y - 1);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, endPosition);
                        break;
                    case MoveDirection.LEFT:
                        endPosition = new Vector2Int(c1.Coords.x - 1, c1.Coords.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, endPosition);
                        sr.flipX = true;
                        break;
                    case MoveDirection.RIGHT:
                        endPosition = new Vector2Int(c1.Coords.x + 1, c1.Coords.y);
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
                        CreateAction(entity, endPosition);
                        sr.flipX = false;
                        break;
                    default:
                        break;
                }
            }

            if (_moveEntities.GetEntitiesCount() == 0 && _animationEntities.GetEntitiesCount() == 0)
            {
                foreach (var i in _actionPhaseEntities)
                {
                    _world.AddComponent<PhaseEndEvent>(_actionPhaseEntities.Entities[i]);
                }
            }
        }

        void CreateAction(EcsEntity entity, Vector2Int endPosition)
        {
            //check wall
            if (!CheckWallCollision(entity, endPosition) && !CheckEnemyCollision(entity, endPosition) && !CheckPlayerCollision(entity, endPosition))
            {
                CreateMoveEntity(entity, endPosition);
            }
        }

        bool CheckEnemyCollision(EcsEntity entity, Vector2Int endPosition)
        {
            bool result = false;

            foreach (var i in _enemyEntities)
            {
                ref var enemyEntity = ref _enemyEntities.Entities[i];
                var c1 = _enemyEntities.Components1[i];
                var c2 = _enemyEntities.Components2[i];

                if (c1.Coords == endPosition)
                {
                    result = true;

                    CreateAnimationEntity(entity, AnimationTriger.CHOP);

                    c2.HealthPoint -= 1;
                    if (c2.HealthPoint <= 0)
                    {
                        _world.AddComponent<GameObjectRemoveEvent>(enemyEntity);
                    }
                }
            }
            return result;
        }

        bool CheckPlayerCollision(EcsEntity entity, Vector2Int endPosition)
        {
            bool result = false;

            foreach (var i in _playerEntities)
            {
                ref var playerEntity = ref _playerEntities.Entities[i];
                var c1 = _playerEntities.Components1[i];
                var c2 = _playerEntities.Components2[i];

                if (c1.Coords == endPosition)
                {
                    result = true;

                    CreateAnimationEntity(entity, AnimationTriger.CHOP);
                    CreateAnimationEntity(playerEntity, AnimationTriger.HIT);

                    c2.HealthPoint -= 1;
                    if (c2.HealthPoint <= 0)
                    {
                        _world.AddComponent<GameObjectRemoveEvent>(playerEntity);
                    }
                }
            }
            return result;
        }

        bool CheckWallCollision(EcsEntity entity, Vector2Int endPosition)
        {
            bool result = false;

            foreach (var i in _wallEntities)
            {
                ref var wallEntity = ref _wallEntities.Entities[i];
                var c1 = _wallEntities.Components1[i];
                var c2 = _wallEntities.Components2[i];

                if (c1.Coords == endPosition)
                {
                    result = true;

                    if (c2.Solid)
                    {
                        _world.GetComponent<TurnComponent>(entity).ReturnInput = true;
                    }
                    else
                    {
                        if (!c2.Damage)
                        {
                            SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                            sr.sprite = c2.DamageSprite;
                            c2.Damage = true;
                        }

                        CreateAnimationEntity(entity, AnimationTriger.CHOP);

                        c2.HealthPoint -= 1;
                        if (c2.HealthPoint <= 0)
                        {
                            _world.AddComponent<GameObjectRemoveEvent>(wallEntity);
                        }
                    }
                }
            }
            return result;
        }

        void CreateAnimationEntity(EcsEntity entity, AnimationTriger animation)
        {
            var c = _world.AddComponent<ActionAnimationComponent>(entity);
            c.Animation = animation;
        }

        void CreateMoveEntity(EcsEntity entity, Vector2Int endPosition)
        {
            var c = _world.AddComponent<ActionMoveComponent>(entity);
            c.EndPosition = endPosition;
            c.Speed = speed;
        }
    }
}
