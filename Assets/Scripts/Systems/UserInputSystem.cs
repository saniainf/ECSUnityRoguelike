using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class UserInputSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, PlayerComponent> _inputPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            float horizontal = (int)Input.GetAxis("Horizontal");
            float vertical = (int)Input.GetAxis("Vertical");

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
            {
                MoveDirection direction;

                if (vertical == 0)
                    direction = horizontal > 0 ? MoveDirection.RIGHT : MoveDirection.LEFT;
                else
                    direction = vertical > 0 ? MoveDirection.UP : MoveDirection.DOWN;

                foreach (var i in _inputPhaseEntities)
                {
                    ref var e = ref _inputPhaseEntities.Entities[i];
                    var c1 = _inputPhaseEntities.Components1[i];
                    var c = _world.AddComponent<InputDirectionComponent>(e);
                    c.MoveDirection = direction;

                    c1.PhaseEnd = true;
                }
            }
        }
    }
}