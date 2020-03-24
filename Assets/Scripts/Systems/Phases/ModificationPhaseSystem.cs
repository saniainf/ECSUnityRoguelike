using Leopotam.Ecs;

namespace Client
{
    sealed class ModificationPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        EntitiesPresetsInject _entitiesPresetsInject = null;

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
                        string key = j.EffectType.ToString();
                        _entitiesPresetsInject.StatusEffectHandlers.TryGetValue(key, out StatusEffectHandler handler);
                        handler.OnModificatonPhase(e, j);
                        handler.OnModificatonPhase(_world, e, j);
                        handler.OnTick(e, j);
                    }

                    c2.StatusEffects.RemoveAll(effect => effect.Time <= 0);
                }
            }
        }
    }
}