using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// фаза выполнения команды ввода
    /// </summary>

    sealed class ActionPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionPhaseComponent, TurnComponent> _actionPhaseEntities = null;

        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;
        readonly EcsFilter<ActionAtackComponent> _atackEntities = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var e = ref _actionPhaseEntities.Entities[i];
                var c1 = _actionPhaseEntities.Get1[i];
                var c2 = _actionPhaseEntities.Get2[i];

                if (!c1.Run)
                {
                    c2.InputCommand.Execute(e);
                    c1.Run = true;
                }
            }

            if (_moveEntities.GetEntitiesCount() == 0 && _animationEntities.GetEntitiesCount() == 0 && _atackEntities.GetEntitiesCount() == 0)
            {
                foreach (var i in _actionPhaseEntities)
                {
                    var c1 = _actionPhaseEntities.Get1[i];
                    c1.PhaseEnd = true;
                }
            }
        }
    }
}
