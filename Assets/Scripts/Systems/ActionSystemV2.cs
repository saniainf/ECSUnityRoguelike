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
    sealed class ActionSystemV2 : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, ActionPhaseComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;
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
                ref var rb = ref c1.Rigidbody;

                if (c2.ActionType == ActionType.NONE)
                {
                    SpriteRenderer sr = c1.Transform.gameObject.GetComponent<SpriteRenderer>();
                    Vector2Int endPosition;

                    switch (c1.MoveDirection)
                    {
                        case MoveDirection.UP:
                            endPosition = new Vector2Int((int)rb.position.x, (int)rb.position.y + 1);
                            c1.MoveDirection = MoveDirection.NONE;
                            break;
                        case MoveDirection.DOWN:
                            endPosition = new Vector2Int((int)rb.position.x, (int)rb.position.y - 1);
                            c1.MoveDirection = MoveDirection.NONE;
                            break;
                        case MoveDirection.LEFT:
                            endPosition = new Vector2Int((int)rb.position.x - 1, (int)rb.position.y);
                            c1.MoveDirection = MoveDirection.NONE;
                            sr.flipX = true;
                            break;
                        case MoveDirection.RIGHT:
                            endPosition = new Vector2Int((int)rb.position.x + 1, (int)rb.position.y);
                            c1.MoveDirection = MoveDirection.NONE;
                            sr.flipX = false;
                            break;
                        default:
                            break;
                    }
                    var c = _world.AddComponent<ActionMoveComponent>(in entity);
                    c.EndPosition = endPosition;
                    c.Rigidbody = rb;
                    c.Speed = speed;
                    specify.ActionType = ActionType.MOVE;
                }


                if (specify.ActionType == ActionType.MOVE)
                {
                    bool itsOk = true;
                    foreach (var k in _moveEntities)
                    {
                        itsOk = false;
                    }
                    if (itsOk)
                    {
                        _actionPhaseEntities.Components2[i].Coords = specify.EndPosition;
                        specify.ActionType = ActionType.NONE;
                        _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);
                    }
                }
            }
        }
    }
}