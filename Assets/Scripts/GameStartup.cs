using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class GameStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        WorldStatus _worldStatus;

        void OnEnable()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _worldStatus = new WorldStatus();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                //.Add(new TestsSystem())
                .Add(new QueueSystem())
                .Add(new PhaseManagerSystem())
                .Add(new NextTurnSystem())
                .Add(new ZonesSystem())
                .Add(new LevelManagerSystem())
                .Add(new InputUserPhaseSystem())
                .Add(new InputNPCPhaseSystem())
                .Add(new ActionPhaseSystem())
                .Add(new ActionMoveSystem())
                .Add(new ActionAtackSystem())
                .Add(new ActionAnimationSystem())
                .Add(new CollectSystem())
                .Add(new AppearanceSystem())
                .Add(new GameOverSystem())
                .Add(new UISystem())
                .Add(new CameraSystem());

            _systems
                .Inject(_worldStatus);

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