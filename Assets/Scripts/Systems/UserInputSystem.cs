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

        EcsFilter<Specify> _actionEntity = null;

        void IEcsRunSystem.Run()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            //EcsEntity entity = _injectFields.thisTurnEntity;
            //Specify specify = _world.GetComponent<Specify>(in entity);
            //Rigidbody2D rb = _world.GetComponent<Position>(in entity).Rigidbody;
            /*
            if (specify.Status == Status.ACTION)
            {
                Vector2 newPostion = Vector2.MoveTowards(rb.position, specify.EndPosition, 5f * Time.deltaTime);
                rb.MovePosition(newPostion);
                float sqrRemainingDistance = (rb.position - specify.EndPosition).sqrMagnitude;
                if (sqrRemainingDistance < float.Epsilon)
                    specify.Status = Status.TURNEND;
            }

            /*
            if (new Vector2(x, y).sqrMagnitude > 0.01f)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0f)
                    {
                        if (!specify.ActionRun)
                        {
                            specify.ActionRun = true;
                            specify.StartPosition = rb.position;
                            specify.EndPosition = new Vector2(rb.position.x + 1, rb.position.y);
                            specify.Direction = (specify.EndPosition - specify.StartPosition).normalized;
                            specify.Speed = 2f;
                        }
                    }
                    else
                    {
                        if (!specify.ActionRun)
                        {
                            specify.ActionRun = true;
                            specify.StartPosition = rb.position;
                            specify.EndPosition = new Vector2(rb.position.x - 1, rb.position.y);
                            specify.Direction = (specify.EndPosition - specify.StartPosition).normalized;
                            specify.Speed = 2f;
                        }
                    }
                }
                else
                {
                    if (y > 0f)
                    {
                        if (!specify.ActionRun)
                        {
                            specify.ActionRun = true;
                            specify.StartPosition = rb.position;
                            specify.EndPosition = new Vector2(rb.position.x, rb.position.y + 1);
                            specify.Direction = (specify.EndPosition - specify.StartPosition).normalized;
                            specify.Speed = 2f;
                        }
                    }
                    else
                    {
                        if (!specify.ActionRun)
                        {
                            specify.ActionRun = true;
                            specify.StartPosition = rb.position;
                            specify.EndPosition = new Vector2(rb.position.x, rb.position.y - 1);
                            specify.Direction = (specify.EndPosition - specify.StartPosition).normalized;
                            specify.Speed = 2f;
                        }
                    }
                }
            }*/
        }
    }
}