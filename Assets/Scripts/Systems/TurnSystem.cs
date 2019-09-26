using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Client
{
    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<PhaseEndEvent, TurnComponent> _phaseEndEvents = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _phaseEndEvents)
            {
                ref var entity = ref _phaseEndEvents.Entities[i];
                var thisPhase = _phaseEndEvents.Components2[i].Phase;

                switch (thisPhase)
                {
                    case Phase.STANDBY:
                        break;
                    case Phase.INPUT:
                        _world.RemoveComponent<InputPhaseComponent>(in entity);
                        _world.AddComponent<ActionPhaseComponent>(in entity);
                        _phaseEndEvents.Components2[i].Phase = Phase.ACTION;
                        break;
                    case Phase.ACTION:
                        _world.RemoveComponent<ActionPhaseComponent>(in entity);
                        _phaseEndEvents.Components2[i].Phase = Phase.TURNEND;
                        // next turnEntity
                        break;
                    case Phase.TURNEND:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}