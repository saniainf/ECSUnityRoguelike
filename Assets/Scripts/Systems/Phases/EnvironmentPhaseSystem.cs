using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EnvironmentPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<EnvironmentPhaseComponent, TurnComponent, DataSheetComponent> _environmentEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _environmentEntities)
            {
                var e = _environmentEntities.Entities[i];
                var c1 = _environmentEntities.Get1[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    c1.PhaseEnd = true;
                    

                }
            }
        }
    }
}