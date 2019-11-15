using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

namespace Client
{
    public static class RLWorldExtension
    {
        public static void RLRemoveGOEntity(this EcsWorld world, EcsEntity entity, float time = 0)
        {
            var c = world.GetComponent<GameObjectComponent>(entity);
            if (c != null)
                UnityEngine.Object.Destroy(c.Transform.gameObject, time);
            world.RemoveEntity(entity);
        }

        public static void RLSetHealth(this EcsWorld world, EcsEntity entity, int value)
        {
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                c.HealthPoint = Mathf.Min(value, c.MaxHealthPoint);
        }

        public static void RLSetMaxHealth(this EcsWorld world, EcsEntity entity, int value)
        {
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                c.MaxHealthPoint = value;
        }

        public static int RLGetHealth(this EcsWorld world, EcsEntity entity)
        {
            int value = 0;
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                value = c.HealthPoint;
            return value;
        }

        public static int RLGetMaxHealth(this EcsWorld world, EcsEntity entity)
        {
            int value = 0;
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                value = c.MaxHealthPoint;
            return value;
        }
    }
}
