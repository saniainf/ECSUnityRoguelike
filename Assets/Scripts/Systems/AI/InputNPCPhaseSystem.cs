using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ввод npc, когда его ход и фаза ввода
    /// </summary>
    
    sealed class InputNPCPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, GameObjectComponent, EnemyComponent> _inputPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputPhaseEntities)
            {
                var ec1 = _inputPhaseEntities.Get1[i];
                var ec2 = _inputPhaseEntities.Get2[i];
                Vector2 goalPosition = Vector2.zero;
                bool skip = true;

                //foreach (var j in _playerEntities)
                //{
                //    var pc1 = _playerEntities.Get1[j];

                //    if (ec2.Transform.position.y == pc1.Transform.position.y)
                //    {
                //        if (ec2.Transform.position.x - 1 == pc1.Transform.position.x)
                //        {
                //            goalPosition = new Vector2(ec2.Transform.position.x - 1, ec2.Transform.position.y);
                //        }
                //        if (ec2.Transform.position.x + 1 == pc1.Transform.position.x)
                //        {
                //            goalPosition = new Vector2(ec2.Transform.position.x + 1, ec2.Transform.position.y);
                //        }
                //    }
                //    else if (ec2.Transform.position.x == pc1.Transform.position.x)
                //    {
                //        if (ec2.Transform.position.y - 1 == pc1.Transform.position.y)
                //        {
                //            goalPosition = new Vector2(ec2.Transform.position.x, ec2.Transform.position.y - 1);
                //        }
                //        if (ec2.Transform.position.y + 1 == pc1.Transform.position.y)
                //        {
                //            goalPosition = new Vector2(ec2.Transform.position.x, ec2.Transform.position.y + 1);
                //        }
                //    }
                //    else
                //    {
                //        skip = true;
                //    }
                //}

                ref var entity = ref _inputPhaseEntities.Entities[i];
                var c = entity.Set<InputActionComponent>();
                c.GoalPosition = goalPosition;
                c.InputActionType = ActionType.Move;
                c.Skip = skip;
                ec1.PhaseEnd = true;
            }
        }
    }
}
