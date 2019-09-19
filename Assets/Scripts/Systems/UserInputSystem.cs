using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UserInputSystem : IEcsRunSystem
    {
        EcsWorld _world = null;
        InjectFields _injectFields = null;

        void IEcsRunSystem.Run()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            EcsEntity entity = _injectFields.thisTurnEntity;

            if (new Vector2(x, y).sqrMagnitude > 0.01f)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0f)
                    {
                        Rigidbody2D rb = _world.GetComponent<Position>(in entity).Rigidbody;

                        //Rigidbody2D rb = _world.GetComponent<Position>(in entity).Rigidbody;
                        //rb.MovePosition(new Vector2(rb.position.x + 1, rb.position.y));
                        //_player.Components1[0].animator.SetTrigger("PlayerChop");
                    }
                    else
                    {
                        Rigidbody2D rb = _world.GetComponent<Position>(in entity).Rigidbody;
                        rb.MovePosition(new Vector2(rb.position.x - 1, rb.position.y));
                        //_player.Components1[0].animator.SetTrigger("PlayerHit");
                    }
                }
                else
                {
                    Debug.Log(y > 0f ? "Up" : "Down");
                }

                //foreach (var i in _snakeFilter)
                //{
                //    var snake = _snakeFilter.Components1[i];
                //    if (!AreDirectionsOpposite(direction, snake.Direction))
                //    {
                //        snake.Direction = direction;
                //    }
                //}
            }
        }

        IEnumerator SmoothMovement(Rigidbody2D rb, Vector2 end)
        {
            float sqrRemainingDistance = (rb.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb.position, end, 1f * Time.deltaTime);

                rb.MovePosition(newPostion);

                sqrRemainingDistance = (rb.position - end).sqrMagnitude;

                yield return null;
            }
        }
    }
}