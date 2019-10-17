using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [EcsInject]
    sealed class LevelManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ZoneExitComponent> _zoneExitEntities = null;

        private int levelNum = 1;
        private float loadLevelTime = 2f;
        private float loadLevelCurrentTime = 0f;
        private bool loadLevel = false;

        void IEcsInitSystem.Initialize()
        {
            loadLevel = true;
            loadLevelCurrentTime = loadLevelTime;
            LevelLoad();
        }

        void IEcsRunSystem.Run()
        {
            foreach (var i in _zoneExitEntities)
            {
                var c1 = _zoneExitEntities.Components1[i];

                if (c1.ZoneStepOn && !loadLevel)
                {
                    loadLevel = true;
                    loadLevelCurrentTime = loadLevelTime;
                    LevelLoad();
                }
            }

            if (loadLevel)
            {
                loadLevelCurrentTime -= Time.deltaTime;
                if (loadLevelCurrentTime <= 0f)
                {
                    loadLevel = false;
                    LevelRun();
                }
            }

        }

        void LevelRun()
        {
            _world.CreateEntityWith(out WorldCreateEvent _);
            _world.CreateEntityWith(out UIEnableEvent uIEnable);
            uIEnable.UIType = UIType.LevelRun;

            _world.CreateEntityWith(out UIDisableEvent uIDisable);
            uIDisable.UIType = UIType.LevelLoad;
        }

        void LevelLoad()
        {
            _world.CreateEntityWith(out WorldDestroyEvent _);
            _world.CreateEntityWith(out UIDisableEvent uIDisable);
            uIDisable.UIType = UIType.LevelRun;

            _world.CreateEntityWith(out UIEnableEvent uIEnable);
            uIEnable.UIType = UIType.LevelLoad;
            uIEnable.LevelNumber = levelNum++;
        }

        void IEcsInitSystem.Destroy()
        {

        }
    }
}