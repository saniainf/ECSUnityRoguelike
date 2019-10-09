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
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;

        //readonly EcsFilter<PositionComponent, WallComponent>.Exclude<GameObjectRemoveEvent> _wallEntities = null;
        //readonly EcsFilter<PositionComponent, EnemyComponent>.Exclude<GameObjectRemoveEvent> _enemyEntities = null;
        //readonly EcsFilter<PositionComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;

        readonly EcsFilter<PositionComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<PositionComponent, ObstacleComponent> _obstacleEntities = null;

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
                    case MoveDirection.NONE:
                        _world.RemoveComponent<InputDirectionComponent>(in entity);
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
            if (!CheckObstacleCollision(entity, endPosition) && !CheckCollision(entity, endPosition))
            {
                CreateMoveEntity(entity, endPosition);
            }
        }

        bool CheckObstacleCollision(EcsEntity entity, Vector2Int endPosition)
        {
            bool result = false;

            foreach (var i in _obstacleEntities)
            {
                ref var wallEntity = ref _obstacleEntities.Entities[i];
                var c1 = _obstacleEntities.Components1[i];

                if (c1.Coords == endPosition)
                {
                    result = true;
                    _world.GetComponent<TurnComponent>(entity).ReturnInput = true;
                }
            }
            return result;
        }

        bool CheckCollision(EcsEntity entity, Vector2Int endPosition)
        {
            bool result = false;

            foreach (var i in _collisionEntities)
            {
                ref var ce = ref _collisionEntities.Entities[i];
                var c1 = _collisionEntities.Components1[i];
                var dsc = _world.GetComponent<DataSheetComponent>(entity);

                if (c1.Coords == endPosition)
                {
                    result = true;

                    CreateAnimationEntity(entity, AnimationTriger.Chop);
                    CreateEffect(new Vector2Int(endPosition.x, endPosition.y), SpriteEffect.Chop, 0.3f);

                    _world.EnsureComponent<ImpactEvent>(ce, out _).HitValue += dsc.HitDamage;
                }
            }

            return result;
        }

        void CreateEffect(Vector2Int position, SpriteEffect effect, float lifeTime)
        {
            _world.CreateEntityWith(out SpriteEffectCreateEvent spriteEffect);
            spriteEffect.Position = position;
            spriteEffect.SpriteEffect = effect;
            spriteEffect.LifeTime = lifeTime;
        }

        void CreateAnimationEntity(EcsEntity entity, AnimationTriger animation)
        {
            var c = _world.EnsureComponent<ActionAnimationComponent>(entity, out _);
            c.Animation = animation;
        }

        void CreateMoveEntity(EcsEntity entity, Vector2Int endPosition)
        {
            var c = _world.EnsureComponent<ActionMoveComponent>(entity, out _);
            c.EndPosition = endPosition;
            c.Speed = speed;
        }
    }
}
