using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    enum MyEnum
    {

    }


    interface IInputCommand
    {
        void Execute(EcsEntity entity);
    }

    class InputComOneStep : IInputCommand
    {
        Vector2 goalPosition;

        public InputComOneStep(Vector2 goalPosition)
        {
            this.goalPosition = goalPosition;
        }

        void IInputCommand.Execute(EcsEntity entity)
        {
            var c = entity.Set<ActionMoveComponent>();
            c.GoalPosition = goalPosition.ToInt2();
        }
    }

    class InputComEmpty : IInputCommand
    {
        void IInputCommand.Execute(EcsEntity entity)
        {

        }
    }

    class InputComOneStepOnDirection : IInputCommand
    {
        float horizontal;
        float vertical;

        Vector2 goalPosition = Vector2.zero;

        public InputComOneStepOnDirection(float h, float v)
        {
            horizontal = h;
            vertical = v;

            if (horizontal != 0)
                vertical = 0;
        }

        void IInputCommand.Execute(EcsEntity entity)
        {
            var c1 = entity.Get<GameObjectComponent>();

            if (vertical > 0)
            {
                goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y + 1);
            }

            if (vertical < 0)
            {
                goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y - 1);
            }

            if (horizontal > 0)
            {
                goalPosition = new Vector2(c1.Transform.position.x + 1, c1.Transform.position.y);
            }

            if (horizontal < 0)
            {
                goalPosition = new Vector2(c1.Transform.position.x - 1, c1.Transform.position.y);
            }

            var c = entity.Set<ActionMoveComponent>();
            c.GoalPosition = goalPosition.ToInt2();
        }
    }

    class InputComAtack : IInputCommand
    {
        EcsEntity target;
        Vector2 targetPosition;

        public InputComAtack(EcsEntity target, Vector2 targetPos)
        {
            this.target = target;
            targetPosition = targetPos;
        }

        void IInputCommand.Execute(EcsEntity entity)
        {
            var c = entity.Set<ActionAtackComponent>();
            c.Target = target;
            c.TargetPosition = targetPosition;
        }
    }
}
