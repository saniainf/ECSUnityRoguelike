using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class GameStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        WorldStatus _worldStatusInject;
        EntitiesPresetsInject _entitiesPresetsInject;

        [SerializeField]
        private EntitiesPresets presets;

        void OnEnable()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _worldStatusInject = new WorldStatus();
            _entitiesPresetsInject = new EntitiesPresetsInject(presets);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                //.Add(new TestsSystem())
                .Add(new ProjectileSystem())

                .Add(new QueueSystem())
                .Add(new PhaseManagerSystem())
                .Add(new NextTurnSystem())
                .Add(new ZonesSystem())
                .Add(new LevelManagerSystem())
                .Add(new TargetSystem())
                .Add(new InputUserPhaseSystem())
                .Add(new InputNPCPhaseSystem())
                .Add(new ActionPhaseSystem())
                .Add(new ActionMoveSystem())
                .Add(new ActionAttackSystem())
                .Add(new ActionAnimationSystem())
                .Add(new EnvironmentPhaseSystem())
                .Add(new ModificationPhaseSystem())
                .Add(new SpellSystem())
                .Add(new DamageSystem())
                .Add(new AppearanceSystem())
                .Add(new GameOverSystem())
                .Add(new EffectSystem())
                .Add(new UISystem())
                .Add(new CameraSystem());

            _systems
                .Inject(_worldStatusInject)
                .Inject(_entitiesPresetsInject);


            _systems.Init();
        }

        void Update()
        {
            _systems.Run();
        }

        void OnDisable()
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}