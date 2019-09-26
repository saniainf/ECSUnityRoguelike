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
        readonly EcsFilter<SpecifyComponent, InputPhaseComponent> _inputPhaseEntities = null;

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
                    _inputPhaseEntities.Components1[i].MoveDirection = direction;
                    _world.AddComponent<PhaseEndEvent>(in _inputPhaseEntities.Entities[i]);
                }
            }
        }
    }
}