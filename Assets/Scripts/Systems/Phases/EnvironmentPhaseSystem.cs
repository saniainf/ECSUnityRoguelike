using Leopotam.Ecs;

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
                var c3 = _environmentEntities.Get3[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    c1.PhaseEnd = true;

                    if (c3.Buffs == null)
                        continue;

                    foreach (var j in c3.Buffs.Buffs)
                    {
                        switch (j.BuffType)
                        {
                            case BuffType.Heal:
                                break;
                            case BuffType.Damage:
                                _world.NewEntityWith(out DamageComponent damage);
                                damage.target = e;
                                damage.damageValue = j.Amount;
                                break;
                            case BuffType.DamageResist:
                                break;
                            case BuffType.AtackDamage:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}