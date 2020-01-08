using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// расширения для World класса
    /// </summary>
    static class RLWorldExtension
    {
        public static void RLDestoryGO(this EcsEntity entity, float time = 0)
        {
            var c = entity.Get<GameObjectComponent>();
            if (c != null)
                UnityEngine.Object.Destroy(c.Transform.gameObject, time);
            entity.Destroy();
        }

        public static void RLSetHealth(this EcsEntity entity, int value)
        {
            var c = entity.Get<DataSheetComponent>();
            if (c != null)
                c.Stats.HealthPoint = Mathf.Min(value, c.Stats.MaxHealthPoint);
        }

        public static void RLSetMaxHealth(this EcsEntity entity, int value)
        {
            var c = entity.Get<DataSheetComponent>();
            if (c != null)
                c.Stats.MaxHealthPoint = value;
        }

        public static int RLGetHealth(this EcsEntity entity)
        {
            int value = 0;
            var c = entity.Get<DataSheetComponent>();
            if (c != null)
                value = c.Stats.HealthPoint;
            return value;
        }

        public static int RLGetMaxHealth(this EcsEntity entity)
        {
            int value = 0;
            var c = entity.Get<DataSheetComponent>();
            if (c != null)
                value = c.Stats.MaxHealthPoint;
            return value;
        }

        public static void RLApplyDamage(this EcsEntity entity, int value)
        {
            var c = entity.Get<DataSheetComponent>();
            if (c != null)
                c.Stats.HealthPoint -= value;
        }

        public static void RLCreateEffect(this EcsWorld world, Vector2 position, EffectPreset effectPreset)
        {
            var go = VExt.LayoutSpriteObject(ObjData.r_PrefabSprite, position, ObjData.t_GameObjectsOther, LayersName.Effect.ToString(), effectPreset.spriteSingle);
            var e = world.NewEntityWithGameObject(go);
            e.Set<EffectComponent>().Duration = effectPreset.duration;
        }

        public static void RLCreateEnemy(this EcsWorld world, Vector2 position, EnemyPreset enemyPreset)
        {
            var go = VExt.LayoutAnimationObject(ObjData.r_PrefabPhysicsAnimation, position, enemyPreset.PresetName, ObjData.t_GameObjectsRoot, LayersName.Character.ToString(), enemyPreset.Animation);
            var e = world.NewEntityWithGameObject(go, true);
            e.Set<EnemyComponent>();
            var dataComponent = e.Set<DataSheetComponent>();
            dataComponent.Stats = new NPCStats(enemyPreset);
            dataComponent.PrimaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
            dataComponent.SecondaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
        }

        public static EcsEntity NewEntityWithGameObject(this EcsWorld world, GameObject go, bool nameID = false)
        {
            var e = world.NewEntityWith(out GameObjectComponent goComponent);
            goComponent.Transform = go.transform;
            goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();
            if (nameID)
            {
                goComponent.GObj.NPCNameText.text = e.GetInternalId().ToString();
            }
            return e;
        }
    }
}
