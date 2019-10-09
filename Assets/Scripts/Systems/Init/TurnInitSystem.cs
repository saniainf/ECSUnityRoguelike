using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TurnInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        private int levelNum = 1;

        void IEcsInitSystem.Initialize()
        {
            CreateWorld();
        }

        void IEcsInitSystem.Destroy() { }

        void IEcsRunSystem.Run()
        {
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                CreateWorld();
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                DestroyWorld();
            }
        }

        void CreateWorld()
        {
            _world.CreateEntityWith(out WorldCreateEvent _);
            _world.CreateEntityWith(out UIEnableEvent uIEnable);
            uIEnable.UIType = UIType.LevelRun;

            _world.CreateEntityWith(out UIDisableEvent uIDisable);
            uIDisable.UIType = UIType.LevelLoad;
        }

        void DestroyWorld()
        {
            _world.CreateEntityWith(out WorldDestroyEvent _);
            _world.CreateEntityWith(out UIDisableEvent uIDisable);
            uIDisable.UIType = UIType.LevelRun;

            _world.CreateEntityWith(out UIEnableEvent uIEnable);
            uIEnable.UIType = UIType.LevelLoad;
            uIEnable.LevelNumber = levelNum++;
        }
    }
}