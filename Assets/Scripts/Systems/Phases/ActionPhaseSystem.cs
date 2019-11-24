using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ���� ����������� �������� ����, �� ������ ���� ����� � �������� ���� ��������
    /// </summary>
    [EcsInject]
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

        //TODO ����� �� ��������
        readonly float speed = 7f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputEntities)
            {
                ref var entity = ref _inputEntities.Entities[i];
                var c1 = _inputEntities.Components1[i];
                var c2 = _inputEntities.Components2[i];

                if (c2.InputAction == InputType.Move)
                {
                    Vector2 goalPosition;

                    switch (c2.MoveDirection)
                    {
                        case MoveDirection.Up:
                            goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y + 1);
                            _world.RemoveComponent<InputActionComponent>(in entity);
                            RunMoveAction(entity, goalPosition);
                            break;
                        case MoveDirection.Down:
                            goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y - 1);
                            _world.RemoveComponent<InputActionComponent>(in entity);
                            RunMoveAction(entity, goalPosition);
                            break;
                        case MoveDirection.Left:
                            goalPosition = new Vector2(c1.Transform.position.x - 1, c1.Transform.position.y);
                            _world.RemoveComponent<InputActionComponent>(in entity);
                            RunMoveAction(entity, goalPosition);
                            c1.GOcomps.SpriteRenderer.flipX = true;
                            break;
                        case MoveDirection.Right:
                            goalPosition = new Vector2(c1.Transform.position.x + 1, c1.Transform.position.y);
                            _world.RemoveComponent<InputActionComponent>(in entity);
                            RunMoveAction(entity, goalPosition);
                            c1.GOcomps.SpriteRenderer.flipX = false;
                            break;
                        case MoveDirection.None:
                            _world.RemoveComponent<InputActionComponent>(in entity);
                            break;
                        default:
                            break;
                    }
                }

                if (c2.InputAction == InputType.UseActiveItem)
                {

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
                var c1 = _obstacleEntities.Components1[i];

                if (c1.GOcomps.Collider.OverlapPoint(goalPosition))
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

                if (c1.GOcomps.Collider.OverlapPoint(goalPosition))
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
