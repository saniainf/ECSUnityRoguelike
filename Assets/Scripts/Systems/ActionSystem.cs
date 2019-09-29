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
                ref var entityPos = ref _actionPhaseEntities.Components2[i];
                ref var rb = ref entityPos.Rigidbody;
                ref var specify = ref _actionPhaseEntities.Components1[i];
                ref var entity = ref _actionPhaseEntities.Entities[i];

                specify.Speed = speed;

                if (specify.MoveDirection != MoveDirection.NONE)
                {
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
                }

                if (specify.ActionType == ActionType.NONE)
                {
                    foreach (var j in _wallEntities)
                    {
                        ref var wallPos = ref _wallEntities.Components1[j];
                        ref var wall = ref _wallEntities.Components2[j];

                        if (specify.EndPosition == wallPos.Coords)
                        {
                            if (wall.Solid)
                            {
                                _world.GetComponent<TurnComponent>(in entity).ReturnInput = true;
                                _world.AddComponent<PhaseEndEvent>(in entity);
                                return;
                            }
                            else
                            {
                                wall.HealthPoint -= 1;
                                if (!wall.Damage)
                                {
                                    SpriteRenderer sr = wallPos.Transform.gameObject.GetComponent<SpriteRenderer>();
                                    sr.sprite = wall.DamageSprite;
                                    wall.Damage = true;
                                }
                                if (wall.HealthPoint <= 0)
                                {
                                    _world.AddComponent<GameObjectRemoveEvent>(in _wallEntities.Entities[j]);
                                }
                                //chop
                                ref var animator = ref _world.GetComponent<AnimationComponent>(in entity).animator;
                                animator.SetTrigger("Chop");
                                specify.ActionType = ActionType.ANIMATION;
                                return;
                            }
                        }
                    }
                }

                if (specify.ActionType == ActionType.ANIMATION)
                {
                    ref var animator = ref _world.GetComponent<AnimationComponent>(in entity).animator;
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
                    {
                        _world.AddComponent<PhaseEndEvent>(in entity);
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