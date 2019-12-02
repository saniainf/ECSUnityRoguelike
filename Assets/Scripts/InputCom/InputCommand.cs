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

    class InputComSkipTurn : IInputCommand
    {
        void IInputCommand.Execute(EcsEntity entity)
        {

        }
    }

    class InputComOneStepOnDirection : IInputCommand
    {
        Direction direction;
        Vector2 goalPosition = Vector2.zero;

        public InputComOneStepOnDirection(Direction direction)
        {
            this.direction = direction;
        }

        void IInputCommand.Execute(EcsEntity entity)
        {
            var c1 = entity.Get<GameObjectComponent>();

            switch (direction)
            {
                case Direction.Up:
                    goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y + 1);
                    break;
                case Direction.Down:
                    goalPosition = new Vector2(c1.Transform.position.x, c1.Transform.position.y - 1);
                    break;
                case Direction.Left:
                    goalPosition = new Vector2(c1.Transform.position.x - 1, c1.Transform.position.y);
                    break;
                case Direction.Right:
                    goalPosition = new Vector2(c1.Transform.position.x + 1, c1.Transform.position.y);
                    break;
                default:
                    break;
            }

            var c = entity.Set<ActionMoveComponent>();
            c.GoalPosition = goalPosition.ToInt2();
        }
    }
}
