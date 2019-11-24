using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ввод npc, когда его ход и фаза ввода
    /// </summary>
    [EcsInject]
    sealed class InputNPCPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, GameObjectComponent, EnemyComponent> _inputPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;

        void IEcsRunSystem.Run()
        {
            var direction = Random.value > 0.7f ? VExt.NextEnum<MoveDirection>() : MoveDirection.None;

            foreach (var i in _inputPhaseEntities)
            {
                var ec1 = _inputPhaseEntities.Components1[i];
                var ec2 = _inputPhaseEntities.Components2[i];

                foreach (var j in _playerEntities)
                {
                    var pc1 = _playerEntities.Components1[j];

                    if (ec2.Transform.position.y == pc1.Transform.position.y)
                    {
                        if (ec2.Transform.position.x - 1 == pc1.Transform.position.x)
                        {
                            direction = MoveDirection.Left;
                        }
                        if (ec2.Transform.position.x + 1 == pc1.Transform.position.x)
                        {
                            direction = MoveDirection.Right;
                        }
                    }
                    if (ec2.Transform.position.x == pc1.Transform.position.x)
                    {
                        if (ec2.Transform.position.y - 1 == pc1.Transform.position.y)
                        {
                            direction = MoveDirection.Down;
                        }
                        if (ec2.Transform.position.y + 1 == pc1.Transform.position.y)
                        {
                            direction = MoveDirection.Up;
                        }
                    }

                }

                ref var entity = ref _inputPhaseEntities.Entities[i];
                var c = _world.AddComponent<InputActionComponent>(entity);
                c.MoveDirection = direction;
                c.InputAction = InputType.Move;
                ec1.PhaseEnd = true;
            }
        }
    }
}
