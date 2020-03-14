using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EnvironmentPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<EnvironmentPhaseComponent, NPCDataSheetComponent, GameObjectComponent, PlayerComponent> _playerEntities = null;
        readonly EcsFilter<EnvironmentPhaseComponent, NPCDataSheetComponent, EnemyComponent> _npcEntities = null;
        readonly EcsFilter<GameObjectComponent, CollectItemComponent> _collectItemEntities = null;

        void IEcsRunSystem.Run()
        {
            npcTurn();
            playerTurn();
        }

        void npcTurn()
        {
            foreach (var i in _npcEntities)
            {
                var e = _npcEntities.Entities[i];
                var c1 = _npcEntities.Get1[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    c1.PhaseEnd = true;
                }
            }
        }

        void playerTurn()
        {
            foreach (var i in _playerEntities)
            {
                var pe = _playerEntities.Entities[i];
                var pc1 = _playerEntities.Get1[i];
                var pc3 = _playerEntities.Get3[i];

                if (!pc1.Run)
                {
                    pc1.Run = true;
                    pc1.PhaseEnd = true;

                    foreach (var j in _collectItemEntities)
                    {
                        var ce = _collectItemEntities.Entities[j];
                        var cc1 = _collectItemEntities.Get1[j];
                        var cc2 = _collectItemEntities.Get2[j];

                        if (pc3.GO.Collider.OverlapPoint(cc1.Transform.position))
                        {
                            if (cc2.Spell != null)
                                pe.RLApplySpell(cc2.Spell);
                            ce.RLDestoryGO();
                        }
                    }
                }
            }
        }
    }
}