using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class CollectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, ActionPhaseComponent, PlayerComponent>.Exclude<GameObjectRemoveEvent> _playerEntities = null;
        readonly EcsFilter<PositionComponent, CollectItemComponent> _collectItemEntities = null;

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

                    if (pc1.Coords == cc1.Coords)
                    {
                        switch (cc2.Type)
                        {
                            case CollectItemType.Heal:
                                _world.EnsureComponent<CollectEvent>(pe, out _).HealValue = cc2.Value;
                                _world.AddComponent<GameObjectRemoveEvent>(ce);
                                break;
                            case CollectItemType.BoostHP:
                                _world.EnsureComponent<CollectEvent>(pe, out _).BoostHealthValue = cc2.Value;
                                _world.AddComponent<GameObjectRemoveEvent>(ce);
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