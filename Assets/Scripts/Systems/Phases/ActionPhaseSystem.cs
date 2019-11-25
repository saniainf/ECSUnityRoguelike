using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// фаза определения действия чара, на основе фазы ввода и контроль этих действий
    /// </summary>

    sealed class ActionPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionPhaseComponent> _actionPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, InputActionComponent> _inputEntities = null;

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
                var c1 = _inputEntities.Get1[i];
                var c2 = _inputEntities.Get2[i];

                if (!c2.Skip)
                {
                    if (c2.InputActionType == ActionType.Move)
                    {
                        entity.Unset<InputActionComponent>();
                        RunMoveAction(entity, c2.GoalPosition);
                    }

                    if (c2.InputActionType == ActionType.UseActiveItem)
                    {
                        entity.Unset<InputActionComponent>();
                        RunMoveAction(entity, c2.GoalPosition);
                    }
                }
                else
                {
                    entity.Unset<InputActionComponent>();
                }
            }

            if (_moveEntities.GetEntitiesCount() == 0 && _animationEntities.GetEntitiesCount() == 0 && _atackEntities.GetEntitiesCount() == 0)
            {
                foreach (var i in _actionPhaseEntities)
                {
                    var c1 = _actionPhaseEntities.Get1[i];
                    c1.PhaseEnd = true;
                }
            }
        }

        void RunMoveAction(EcsEntity entity, Vector2 goalPosition)
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
                var c1 = _obstacleEntities.Get1[i];

                if (c1.GOcomps.Collider.OverlapPoint(goalPosition))
                {
                    result = true;
                    entity.Get<TurnComponent>().ReturnInput = true;
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
                var c1 = _collisionEntities.Get1[i];

                if (c1.GOcomps.Collider.OverlapPoint(goalPosition))
                {
                    result = true;

                    var c = entity.Set<ActionAtackComponent>();
                    c.TargetPosition = goalPosition;
                    c.Target = ce;
                }
            }

            return result;
        }

        void MoveEntity(EcsEntity entity, Vector2 goalPosition)
        {
            var c = entity.Set<ActionMoveComponent>();
            c.GoalInt = goalPosition.ToInt2();
            c.Speed = speed;
        }
    }
}
