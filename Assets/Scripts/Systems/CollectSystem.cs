using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class CollectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, ActionPhaseComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent, CollectItemComponent> _collectItemEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                ref var pe = ref _playerEntities.Entities[i];
                var pc1 = _playerEntities.Components1[i];

                foreach (var j in _collectItemEntities)
                {
                    ref var ce = ref _collectItemEntities.Entities[j];
                    var cc1 = _collectItemEntities.Components1[j];
                    var cc2 = _collectItemEntities.Components2[j];

                    if (pc1.Transform.position == cc1.Transform.position)
                    {
                        switch (cc2.Type)
                        {
                            case CollectItemType.Heal:
                                _world.EnsureComponent<CollectEvent>(pe, out _).HealValue = cc2.Value;
                                _world.RemoveGOEntity(ce);
                                break;
                            case CollectItemType.BoostHP:
                                _world.EnsureComponent<CollectEvent>(pe, out _).BoostHealthValue = cc2.Value;
                                _world.RemoveGOEntity(ce);
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