using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
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
                var c1 = _inputPhaseEntities.Components1[i];
                if (c1.PhaseEnd)
                {
                    _world.RemoveComponent<InputPhaseComponent>(e);
                    _world.AddComponent<ActionPhaseComponent>(e);
                }
            }

            foreach (var i in _actionPhaseEntities)
            {
                ref var e = ref _actionPhaseEntities.Entities[i];
                var c1 = _actionPhaseEntities.Components1[i];
                var c2 = _actionPhaseEntities.Components2[i];

                if (c1.PhaseEnd)
                {
                    _world.RemoveComponent<ActionPhaseComponent>(e);

                    if (c2.ReturnInput)
                    {
                        _world.AddComponent<InputPhaseComponent>(e);
                        c2.ReturnInput = false;
                    }
                    else
                    {
                        _world.RemoveComponent<TurnComponent>(e);
                    }
                }
            }
        }
    }
}