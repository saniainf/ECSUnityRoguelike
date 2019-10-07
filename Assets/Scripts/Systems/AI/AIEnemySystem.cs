using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class AIEnemySystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, PositionComponent, EnemyComponent>.Exclude<GameObjectRemoveEvent> _inputPhaseEntities = null;
        readonly EcsFilter<PositionComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;

        void IEcsRunSystem.Run()
        {
            var direction = Random.value > 0.7f ? VExt.NextEnum<MoveDirection>() : MoveDirection.NONE;

            foreach (var i in _inputPhaseEntities)
            {
                var ec2 = _inputPhaseEntities.Components2[i];

                foreach (var j in _playerEntities)
                {
                    var pc1 = _playerEntities.Components1[j];

                    if (ec2.Coords.y == pc1.Coords.y)
                    {
                        if (ec2.Coords.x - 1 == pc1.Coords.x)
                        {
                            direction = MoveDirection.LEFT;
                        }
                        if (ec2.Coords.x + 1 == pc1.Coords.x)
                        {
                            direction = MoveDirection.RIGHT;
                        }
                    }
                    if (ec2.Coords.x == pc1.Coords.x)
                    {
                        if (ec2.Coords.y - 1 == pc1.Coords.y)
                        {
                            direction = MoveDirection.DOWN;
                        }
                        if (ec2.Coords.y + 1 == pc1.Coords.y)
                        {
                            direction = MoveDirection.UP;
                        }
                    }

                }

                ref var entity = ref _inputPhaseEntities.Entities[i];
                var c = _world.AddComponent<InputDirectionComponent>(entity);
                c.MoveDirection = direction;
                _world.AddComponent<PhaseEndEvent>(entity);
            }
        }
    }
}
