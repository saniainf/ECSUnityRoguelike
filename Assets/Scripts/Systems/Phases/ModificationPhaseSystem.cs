using Leopotam.Ecs;

namespace Client
{
    sealed class ModificationPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ModificationPhaseComponent, NPCDataSheetComponent> _modificationEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _modificationEntities)
            {
                var e = _modificationEntities.Entities[i];
                var c1 = _modificationEntities.Get1[i];
                var c2 = _modificationEntities.Get2[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    c1.PhaseEnd = true;

                    foreach (var j in c2.StatusEffects)
                    {
                        switch (j.EffectType)
                        {
                            case StatusEffectType.Eating:
                                e.RLSetHealth(e.RLGetHealth() + j.Value);
                                j.Time -= 1;
                                break;
                            case StatusEffectType.Healing:
                                break;
                            case StatusEffectType.Bleeding:
                                break;
                            case StatusEffectType.Acid:
                                break;
                            default:
                                break;
                        }
                    }

                    c2.StatusEffects.RemoveAll(effect => effect.Time <= 0);
                }
            }
        }
    }
}