using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;

        void OnEnable()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new GameWorldInitSystem())
                .Add(new TurnInitSystem());

            _systems
                .Add(new UserInputSystem())
                .Add(new ActionSystem())
                .Add(new ActionMoveSystem())
                .Add(new TurnSystem())
                .Add(new GameWorldSystem());

            _systems.Initialize();
        }

        void Update()
        {
            _systems.Run();

            _world.RemoveOneFrameComponents();
        }

        void OnDisable()
        {
            _systems.Dispose();
            _systems = null;
            _world.Dispose();
            _world = null;
        }
    }
}