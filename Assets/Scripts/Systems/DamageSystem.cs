using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// нанесение урона объекту
    /// </summary>
    sealed class DamageSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<ApplyDamageComponent> _damageEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _damageEntities)
            {
                var c1 = _damageEntities.Get1[i];
                var data = c1.Target.Get<NPCDataSheetComponent>();

                if (data != null)
                {
                    data.Stats.HealthPoint -= c1.DamageValue;
                }

                _damageEntities.Entities[i].Destroy();
            }
        }
    }
}