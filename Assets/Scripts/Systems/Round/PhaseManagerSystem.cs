using Leopotam.Ecs;

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

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputPhaseEntities)
            {
                ref var e = ref _inputPhaseEntities.Entities[i];
                var c1 = _inputPhaseEntities.Get1[i];

                if (c1.PhaseEnd)
                {
                    e.Unset<InputPhaseComponent>();
                    e.Set<ActionPhaseComponent>();
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
                        e.Unset<TurnComponent>();
                    }
                }
            }
        }
    }
}