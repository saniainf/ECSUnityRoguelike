using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UserInputSystem : IEcsRunSystem
    {
        EcsWorld _world = null;
        InjectFields _injectFields = null;

        EcsFilter<Action> _actionEntity = null;

        void IEcsRunSystem.Run()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            EcsEntity entity = _injectFields.thisTurnEntity;
            Action action = _world.GetComponent<Action>(in entity);
            Rigidbody2D rb = _world.GetComponent<Position>(in entity).Rigidbody;

            if (action.ActionRun)
            {
                Vector2 newPostion = Vector2.MoveTowards(rb.position, action.EndPosition, 5f * Time.deltaTime);
                rb.MovePosition(newPostion);
                float sqrRemainingDistance = (rb.position - action.EndPosition).sqrMagnitude;
                if (sqrRemainingDistance < float.Epsilon)
                    action.ActionRun = false;
            }


            if (new Vector2(x, y).sqrMagnitude > 0.01f)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0f)
                    {
                        if (!action.ActionRun)
                        {
                            action.ActionRun = true;
                            action.StartPosition = rb.position;
                            action.EndPosition = new Vector2(rb.position.x + 1, rb.position.y);
                            action.Direction = (action.EndPosition - action.StartPosition).normalized;
                            action.Speed = 2f;
                        }
                    }
                    else
                    {
                        if (!action.ActionRun)
                        {
                            action.ActionRun = true;
                            action.StartPosition = rb.position;
                            action.EndPosition = new Vector2(rb.position.x - 1, rb.position.y);
                            action.Direction = (action.EndPosition - action.StartPosition).normalized;
                            action.Speed = 2f;
                        }
                    }
                }
                else
                {
                    if (y > 0f)
                    {
                        if (!action.ActionRun)
                        {
                            action.ActionRun = true;
                            action.StartPosition = rb.position;
                            action.EndPosition = new Vector2(rb.position.x, rb.position.y + 1);
                            action.Direction = (action.EndPosition - action.StartPosition).normalized;
                            action.Speed = 2f;
                        }
                    }
                    else
                    {
                        if (!action.ActionRun)
                        {
                            action.ActionRun = true;
                            action.StartPosition = rb.position;
                            action.EndPosition = new Vector2(rb.position.x, rb.position.y - 1);
                            action.Direction = (action.EndPosition - action.StartPosition).normalized;
                            action.Speed = 2f;
                        }
                    }
                }
            }
        }
    }
}