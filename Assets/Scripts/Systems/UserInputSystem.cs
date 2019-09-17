using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UserInputSystem : IEcsRunSystem
    {
        EcsWorld _world = null;
        TestInject _testInject = null;
        EcsFilter<Player> _player = null;

        void IEcsRunSystem.Run()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (new Vector2(x, y).sqrMagnitude > 0.01f)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0f)
                    {
                        _testInject.a = 8;
                        //_player.Components1[0].animator.SetTrigger("PlayerChop");
                    }
                    else
                    {
                        _testInject.a = 0;
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

            foreach (int i in _player)
            {
                if (_testInject.a == 8)
                    _player.Components1[i].animator.SetTrigger("PlayerChop");
            }
        }
    }
}