using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class GameStartup : MonoBehaviour
    {
        public WorldObjects WorldObjects;

        EcsWorld _world;
        EcsSystems _systems;
        WorldStatus _worldStatus;

        void OnEnable()
        {
            VExt.WorldObjects = WorldObjects;
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _worldStatus = new WorldStatus();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .Add(new TestsSystem())
                .Add(new QueueSystem())
                .Add(new PhaseManagerSystem())
                .Add(new NextTurnSystem())
                .Add(new ZonesSystem())
                .Add(new LevelManagerSystem())
                .Add(new UserInputSystem())
                .Add(new AIEnemySystem())
                .Add(new ActionSystem())
                .Add(new ActionMoveSystem())
                .Add(new ActionAtackSystem())
                .Add(new ActionAnimationSystem())
                .Add(new CollectSystem())
                .Add(new EffectSystem())
                .Add(new AppearanceSystem())
                .Add(new GameOverSystem())
                .Add(new UISystem())
                .Add(new CameraSystem());

            _systems
                .Inject(_worldStatus)
                .Inject(WorldObjects);

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