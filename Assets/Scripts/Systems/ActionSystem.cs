using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<SpecifyComponent, PositionComponent, ActionPhaseComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;
        readonly EcsFilter<PositionComponent, WallComponent> _wallEntities = null;
        readonly EcsFilter<PositionComponent, EnemyComponent> _enemyEntities = null;
        readonly EcsFilter<PositionComponent, FoodComponent> _foodEntities = null;

        //TODO брать из настроек
        readonly float speed = 5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var rb = ref _actionPhaseEntities.Components2[i].Rigidbody;
                ref var specify = ref _actionPhaseEntities.Components1[i];

                specify.Speed = speed;

                if (specify.MoveDirection != MoveDirection.NONE)
                {
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
                            break;
                        case MoveDirection.RIGHT:
                            specify.EndPosition = new Vector2Int((int)rb.position.x + 1, (int)rb.position.y);
                            specify.MoveDirection = MoveDirection.NONE;
                            break;
                        default:
                            break;
                    }
                }

                if (specify.ActionType == ActionType.NONE)
                {
                    foreach (var j in _wallEntities)
                    {
                        bool canMove = true;
                        if (specify.EndPosition == _wallEntities.Components1[j].Coords)
                            canMove = false;

                        if (canMove)
                        {
                            specify.ActionType = ActionType.MOVE;
                        }
                        else
                        {
                            _world.GetComponent<TurnComponent>(in _actionPhaseEntities.Entities[i]).ReturnInput = true;
                            _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);
                            return;
                        }
                    }
                }

                if (specify.ActionType == ActionType.MOVE)
                {
                    Vector2 newPostion = Vector2.MoveTowards(rb.position, specify.EndPosition, specify.Speed * Time.deltaTime);
                    rb.MovePosition(newPostion);

                    //next phase
                    float sqrRemainingDistance = (rb.position - specify.EndPosition).sqrMagnitude;
                    if (sqrRemainingDistance < float.Epsilon)
                    {
                        //TODO enemy могут подбирать ?
                        foreach (var k in _foodEntities)
                        {
                            if (specify.EndPosition == _foodEntities.Components1[k].Coords)
                            {
                                _world.AddComponent<GameObjectRemoveEvent>(_foodEntities.Entities[k]);
                                //TODO добавить начесление очков
                            }
                        }
                        _actionPhaseEntities.Components2[i].Coords = specify.EndPosition;
                        rb.position = specify.EndPosition;
                        _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);
                    }
                }
            }
        }
    }
}