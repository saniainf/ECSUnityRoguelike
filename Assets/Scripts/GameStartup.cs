using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                .Add(new ZonesSystem())
                .Add(new LevelManagerSystem())
                .Add(new UserInputSystem())
                .Add(new GameWorldEventsSystem())
                .Add(new AIEnemySystem())
                .Add(new ActionSystem())
                .Add(new ActionMoveSystem())
                .Add(new ActionAnimationSystem())
                .Add(new CollectSystem())
                .Add(new TurnSystem())
                .Add(new EffectSystem())
                .Add(new InfluenceEventsSystem())
                .Add(new GameObjectEventsSystem())
                .Add(new AppearanceSystem())
                .Add(new UIEventsSystem())
                .Add(new CameraSystem());

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