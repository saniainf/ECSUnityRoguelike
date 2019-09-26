using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<SpecifyComponent, PositionComponent, ActionPhaseComponent>.Exclude<GameObjectRemoveEvent> _actionPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var rb = ref _actionPhaseEntities.Components2[i].Rigidbody;
                ref var specify = ref _actionPhaseEntities.Components1[i];
                var speed = 5f;

                switch (specify.MoveDirection)
                {
                    case MoveDirection.UP:
                        specify.EndPosition = new Vector2(rb.position.x, rb.position.y + 1);
                        specify.Speed = speed;
                        specify.MoveDirection = MoveDirection.NONE;
                        break;
                    case MoveDirection.DOWN:
                        specify.EndPosition = new Vector2(rb.position.x, rb.position.y - 1);
                        specify.Speed = speed;
                        specify.MoveDirection = MoveDirection.NONE;
                        break;
                    case MoveDirection.LEFT:
                        specify.EndPosition = new Vector2(rb.position.x - 1, rb.position.y);
                        specify.Speed = speed;
                        specify.MoveDirection = MoveDirection.NONE;
                        break;
                    case MoveDirection.RIGHT:
                        specify.EndPosition = new Vector2(rb.position.x + 1, rb.position.y);
                        specify.Speed = speed;
                        specify.MoveDirection = MoveDirection.NONE;
                        break;
                    case MoveDirection.NONE:
                        break;
                    default:
                        break;
                }

                Vector2 newPostion = Vector2.MoveTowards(rb.position, specify.EndPosition, specify.Speed * Time.deltaTime);
                rb.MovePosition(newPostion);
                float sqrRemainingDistance = (rb.position - specify.EndPosition).sqrMagnitude;

                //next phase
                if (sqrRemainingDistance < float.Epsilon)
                    _world.AddComponent<PhaseEndEvent>(in _actionPhaseEntities.Entities[i]);

            }
         }
    }
}