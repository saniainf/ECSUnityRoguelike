using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class EffectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<EffectComponent> _effectEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _effectEntities)
            {
                var c1 = _effectEntities.Get1[i];

                if (c1.Run)
                {
                    c1.Duration -= Time.deltaTime;
                    if (c1.Duration <= 0)
                        _effectEntities.Entities[i].RLDestoryGO();
                }
                else
                {
                    c1.Run = true;
                }
            }
        }
    }
}