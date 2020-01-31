using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// менеджер фаз хода чара
    /// </summary>

    sealed class PhaseManagerSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, TurnComponent> _inputPhaseEntities = null;
        readonly EcsFilter<ActionPhaseComponent, TurnComponent> _actionPhaseEntities = null;
        readonly EcsFilter<EnvironmentPhaseComponent, TurnComponent> _environmentPhaseEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputPhaseEntities)
            {
                ref var e = ref _inputPhaseEntities.Entities[i];
                var c1 = _inputPhaseEntities.Get1[i];
                var c2 = _inputPhaseEntities.Get2[i];

                if (c1.PhaseEnd)
                {
                    e.Unset<InputPhaseComponent>();

                    if (!c2.SkipTurn)
                    {
                        e.Set<ActionPhaseComponent>();
                    }
                    else
                    {
                        e.Unset<TurnComponent>();
                    }

                }
            }

            foreach (var i in _actionPhaseEntities)
            {
                ref var e = ref _actionPhaseEntities.Entities[i];
                var c1 = _actionPhaseEntities.Get1[i];
                var c2 = _actionPhaseEntities.Get2[i];

                if (c1.PhaseEnd)
                {
                    e.Unset<ActionPhaseComponent>();

                    if (c2.ReturnInput)
                    {
                        e.Set<InputPhaseComponent>();
                        c2.ReturnInput = false;
                    }
                    else
                    {
                        e.Set<EnvironmentPhaseComponent>();
                    }
                }
            }

            foreach (var i in _environmentPhaseEntities)
            {
                ref var e = ref _environmentPhaseEntities.Entities[i];
                var c1 = _environmentPhaseEntities.Get1[i];
                var c2 = _environmentPhaseEntities.Get2[i];

                if (c1.PhaseEnd)
                {
                    e.Unset<EnvironmentPhaseComponent>();

                    e.Unset<TurnComponent>();
                }
            }
        }
    }
}