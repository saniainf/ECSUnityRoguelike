using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TurnSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<TurnComponent> _turnEntities = null;
        readonly EcsFilter<InputPhaseComponent, PhaseEndEvent> _inputPhaseEnd = null;
        readonly EcsFilter<ActionPhaseComponent, TurnComponent, PhaseEndEvent> _actionPhaseEnd = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputPhaseEnd)
            {
                _world.AddComponent<ActionPhaseComponent>(_inputPhaseEnd.Entities[i]);
                _world.RemoveComponent<InputPhaseComponent>(_inputPhaseEnd.Entities[i]);
            }

            foreach (var i in _actionPhaseEnd)
            {
                _world.RemoveComponent<ActionPhaseComponent>(_actionPhaseEnd.Entities[i]);

                if (_actionPhaseEnd.Components2[i].ReturnInput)
                {
                    _actionPhaseEnd.Components2[i].ReturnInput = false;
                    _world.AddComponent<InputPhaseComponent>(_actionPhaseEnd.Entities[i]);
                }
                else
                    nextTurnEnity(_actionPhaseEnd.Components2[i].Initiative);
            }
        }

        void nextTurnEnity(int thisInitiative)
        {
            List<int> initiative = new List<int>();

            foreach (var i in _turnEntities)
            {
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
                    _world.AddComponent<InputPhaseComponent>(_turnEntities.Entities[i]);
                }
            }
        }
    }
}