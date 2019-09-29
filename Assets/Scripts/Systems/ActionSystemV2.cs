using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionSystemV2 : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<SpecifyComponent, PositionComponent, ActionPhaseComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;
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
                ref var entityPos = ref _actionPhaseEntities.Components2[i];
                ref var rb = ref entityPos.Rigidbody;
                ref var specify = ref _actionPhaseEntities.Components1[i];
                ref var entity = ref _actionPhaseEntities.Entities[i];

                specify.Speed = speed;

                var a = _world.GetComponent<TurnComponent>(in _actionPhaseEntities.Entities[i]);
                if (specify.ActionType == ActionType.NONE)
                {
                    Debug.Log(a.Phase.ToString());
                    SpriteRenderer sr = entityPos.Transform.gameObject.GetComponent<SpriteRenderer>();

                    switch (specify.MoveDirection)
                    {
                        case MoveDirection.UP:
                            specify.EndPosition = new Vector2Int((int)rb.position.x, (int)rb.position.y + 1);
                            specify.MoveDirection = MoveDirection.NONE;
                            break;
                        case MoveDirection.DOWN:
                            specify.EndPosition = new Vector2Int((int)rb.position.x, (int)rb.position.y - 1);
                            specify.MoveDirection = MoveDirection.NONE;
                            break;
                        case MoveDirection.LEFT:
                            specify.EndPosition = new Vector2Int((int)rb.position.x - 1, (int)rb.position.y);
                            specify.MoveDirection = MoveDirection.NONE;
                            sr.flipX = true;
                            break;
                        case MoveDirection.RIGHT:
                            specify.EndPosition = new Vector2Int((int)rb.position.x + 1, (int)rb.position.y);
                            specify.MoveDirection = MoveDirection.NONE;
                            sr.flipX = false;
                            break;
                        default:
                            break;
                    }
                    var c1 = _world.AddComponent<ActionMoveComponent>(in _actionPhaseEntities.Entities[i]);
                    c1.EndPosition = specify.EndPosition;
                    c1.Rigidbody = rb;
                    c1.Speed = specify.Speed;
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
                        _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);
                    }
                }
            }
        }
    }
}