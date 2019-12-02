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

        readonly EcsFilter<ActionPhaseComponent, TurnComponent> _actionPhaseEntities = null;

        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;
        readonly EcsFilter<ActionAtackComponent> _atackEntities = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        //TODO брать из настроек
        readonly float speed = 7f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var e = ref _actionPhaseEntities.Entities[i];
                var c1 = _actionPhaseEntities.Get1[i];
                var c2 = _actionPhaseEntities.Get2[i];

                if (!c1.Run)
                {
                    if (c2.ActionType == ActionType.Move)
                    {
                        RunMoveAction(e, c2.GoalPosition);
                    }

                    if (c2.ActionType == ActionType.UseActiveItem)
                    {
                        RunMoveAction(e, c2.GoalPosition);
                    }

                    c1.Run = true;
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

                if (c1.GObj.Collider.OverlapPoint(goalPosition))
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

                if (c1.GObj.Collider.OverlapPoint(goalPosition))
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
            c.GoalPosition = goalPosition.ToInt2();
            c.Speed = speed;
        }
    }
}
