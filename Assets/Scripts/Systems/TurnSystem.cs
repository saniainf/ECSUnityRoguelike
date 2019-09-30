using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    enum Phase : int
    {
        STANDBY,
        INPUT,
        ACTION
    }

    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<PhaseEndEvent, TurnComponent>.Exclude<GameObjectRemoveEvent> _phaseEndEvents = null;
        readonly EcsFilter<TurnComponent> _turnEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _phaseEndEvents)
            {
                ref var entity = ref _phaseEndEvents.Entities[i];
                ref var c2 = ref _phaseEndEvents.Components2[i];

                switch (c2.Phase)
                {
                    case Phase.INPUT:
                        _world.AddComponent<ActionPhaseComponent>(in entity);
                        c2.Phase = Phase.ACTION;
                        break;
                    case Phase.ACTION:
                        c2.Phase = Phase.STANDBY;
                        //next turnEntity
                        if (c2.ReturnInput)
                        {
                            _world.AddComponent<InputPhaseComponent>(in entity);
                            c2.Phase = Phase.INPUT;
                            c2.ReturnInput = false;
                            break;
                        }
                        nextTurnEnity(c2.Initiative);
                        break;
                    default:
                        break;
                }
            }
        }

        void nextTurnEnity(int thisInitiative)
        {
            List<int> initiative = new List<int>();

            foreach (var i in _turnEntities)
            {
                if (_turnEntities.Components1[i].Phase != Phase.STANDBY)
                    return;

                initiative.Add(_turnEntities.Components1[i].Initiative);
            }

            var greatOf = initiative.FindAll(i => i > thisInitiative);
            int min = 0;

            if (greatOf.Count > 0)
                min = greatOf.Min();
            else
                min = initiative.Min();

            foreach (var i in _turnEntities)
            {
                if (_turnEntities.Components1[i].Initiative == min)
                {
                    _world.AddComponent<InputPhaseComponent>(in _turnEntities.Entities[i]);
                    _turnEntities.Components1[i].Phase = Phase.INPUT;
                }
            }
        }
    }
}