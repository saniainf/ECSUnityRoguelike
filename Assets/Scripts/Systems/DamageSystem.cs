using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// нанесение урона объекту
    /// </summary>
    sealed class DamageSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<DamageComponent> _damageEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _damageEntities)
            {
                var c1 = _damageEntities.Get1[i];
                c1.target.RLApplyDamage(c1.damageValue);
                _damageEntities.Entities[i].Destroy();
            }
        }
    }
}