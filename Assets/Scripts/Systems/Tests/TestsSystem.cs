using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<InputPhaseComponent> _inputEntities = null;
        readonly EcsFilter<ActionPhaseComponent> _actionEntities = null;

        void IEcsInitSystem.Initialize()
        {
            for (int i = 0; i < 5; i++)
            {
                var e = _world.CreateEntityWith(out EnemyComponent enemyComponent, out DataSheetComponent dataSheetComponent);
                dataSheetComponent.Initiative = Random.Range(0, 20);
            }

            for (int i = 0; i < 5; i++)
            {
                var e = _world.CreateEntityWith(out PlayerComponent playerComponent, out DataSheetComponent dataSheetComponent);
                dataSheetComponent.Initiative = Random.Range(0, 20);
            }
        }

        void IEcsInitSystem.Destroy() { }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputEntities)
            {
                _inputEntities.Components1[i].PhaseEnd = true;
            }

            foreach (var i in _actionEntities)
            {
                _actionEntities.Components1[i].PhaseEnd = true;
            }
        }
    }
}