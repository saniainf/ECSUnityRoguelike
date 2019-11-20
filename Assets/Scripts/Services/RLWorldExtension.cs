using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// расширения для World класса
    /// </summary>
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
                c.Stats.HealthPoint = Mathf.Min(value, c.Stats.MaxHealthPoint);
        }

        public static void RLSetMaxHealth(this EcsWorld world, EcsEntity entity, int value)
        {
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                c.Stats.MaxHealthPoint = value;
        }

        public static int RLGetHealth(this EcsWorld world, EcsEntity entity)
        {
            int value = 0;
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                value = c.Stats.HealthPoint;
            return value;
        }

        public static int RLGetMaxHealth(this EcsWorld world, EcsEntity entity)
        {
            int value = 0;
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                value = c.Stats.MaxHealthPoint;
            return value;
        }

        public static void RLApplyDamage(this EcsWorld world, EcsEntity entity, int value)
        {
            var c = world.GetComponent<DataSheetComponent>(entity);
            if (c != null)
                c.Stats.HealthPoint -= value;
        }

        public static void RLCreateEffect(this EcsWorld world, Vector2 position, SpriteEffect effect, float lifeTime)
        {
            switch (effect)
            {
                case SpriteEffect.None:
                    break;
                case SpriteEffect.Chop:
                    var go = VExt.LayoutSpriteObjects(
                        ObjData.r_PrefabSprite,
                        position.x, position.y,
                        ObjData.t_GameObjectsOther,
                        LayersName.Effect.ToString(),
                        ObjData.p_ChopEffect.spriteSingle);
                    UnityEngine.Object.Destroy(go, lifeTime);
                    break;
                default:
                    break;
            }
        }
    }
}
