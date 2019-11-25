using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление сбором предметов на карте
    /// </summary>
    
    sealed class CollectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<GameObjectComponent, CollectItemComponent> _collectItemEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _playerEntities)
            {
                ref var pe = ref _playerEntities.Entities[i];
                var pc1 = _playerEntities.Get1[i];
                var pc2 = _playerEntities.Get2[i];

                foreach (var j in _collectItemEntities)
                {
                    ref var ce = ref _collectItemEntities.Entities[j];
                    var cc1 = _collectItemEntities.Get1[j];
                    var cc2 = _collectItemEntities.Get2[j];

                    if (pc1.GOcomps.Collider.OverlapPoint(cc1.Transform.position))
                    {
                        cc2.CollectItem.OnCollect(pe);
                        ce.RLDestoryGO();
                    }
                }
            }
        }
    }
}