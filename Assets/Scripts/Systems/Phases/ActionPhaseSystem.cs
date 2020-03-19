using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ActionPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionPhaseComponent, TurnComponent> _actionPhaseEntities = null;

        readonly EcsFilter<ActionMoveComponent> _moveEntities = null;
        readonly EcsFilter<ActionAnimationComponent> _animationEntities = null;
        readonly EcsFilter<ActionAttackComponent> _attackEntities = null;
        readonly EcsFilter<ProjectileComponent> _projectileEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _actionPhaseEntities)
            {
                ref var e = ref _actionPhaseEntities.Entities[i];
                var c1 = _actionPhaseEntities.Get1[i];

                if (!c1.Run)
                {
                    _actionPhaseEntities.Get2[i].InputCommand.Execute(e);
                    c1.Run = true;
                }
            }

            if (_moveEntities.IsEmpty() && _animationEntities.IsEmpty() && _attackEntities.IsEmpty() && _projectileEntities.IsEmpty())
            {
                foreach (var i in _actionPhaseEntities)
                {
                    _actionPhaseEntities.Get1[i].PhaseEnd = true;
                }
            }
        }
    }
}
