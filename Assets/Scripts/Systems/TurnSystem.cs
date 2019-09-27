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
        readonly EcsFilter<PhaseEndEvent, TurnComponent, SpecifyComponent>.Exclude<GameObjectRemoveEvent> _phaseEndEvents = null;
        readonly EcsFilter<TurnComponent, SpecifyComponent> _turnEntities = null;

        void IEcsRunSystem.Run()
        {
            bool nextEntity = false;
            int thisInitiative = 0;

            foreach (var i in _phaseEndEvents)
            {
                ref var entity = ref _phaseEndEvents.Entities[i];
                var thisPhase = _phaseEndEvents.Components2[i].Phase;

                switch (thisPhase)
                {
                    case Phase.INPUT:
                        _world.RemoveComponent<InputPhaseComponent>(in entity);
                        _world.AddComponent<ActionPhaseComponent>(in entity);
                        _phaseEndEvents.Components2[i].Phase = Phase.ACTION;
                        break;
                    case Phase.ACTION:
                        _world.RemoveComponent<ActionPhaseComponent>(in entity);
                        _phaseEndEvents.Components2[i].Phase = Phase.STANDBY;
                        ResetSpecifyField(ref _phaseEndEvents.Components3[i]);
                        // next turnEntity
                        if (_phaseEndEvents.Components2[i].ReturnInput)
                        {
                            _world.AddComponent<InputPhaseComponent>(in entity);
                            _phaseEndEvents.Components2[i].Phase = Phase.INPUT;
                            _phaseEndEvents.Components2[i].ReturnInput = false;
                            break;
                        }
                        nextEntity = true;
                        thisInitiative = _phaseEndEvents.Components3[i].Initiative;
                        break;
                    default:
                        break;
                }
            }

            //TODO проверить что все енти в standby
            if (nextEntity)
            {
                nextTurnEnity(thisInitiative);
            }
        }

        void ResetSpecifyField(ref SpecifyComponent specify)
        {
            specify.EndPosition = Vector2Int.zero;
            specify.ActionType = ActionType.NONE;
            specify.Speed = 0f;
            specify.MoveDirection = MoveDirection.NONE;
        }


        void nextTurnEnity(int thisInitiative)
        {
            //var specifies = _turnEntities.Components2.ToList().FindAll(x => x.Initiative > thisInitiative);
            //int min = specifies.Min(i => i.Initiative);

            List<int> initiative = new List<int>();

            foreach (var i in _turnEntities)
            {
                initiative.Add(_turnEntities.Components2[i].Initiative);
            }

            var greatOf = initiative.FindAll(i => i > thisInitiative);
            int min = 0;

            if (greatOf.Count > 0)
                min = greatOf.Min();
            else
                min = initiative.Min();

            foreach (var i in _turnEntities)
            {
                if (_turnEntities.Components2[i].Initiative == min)
                {
                    _world.AddComponent<InputPhaseComponent>(in _turnEntities.Entities[i]);
                    _turnEntities.Components1[i].Phase = Phase.INPUT;
                }
            }
        }
    }
}