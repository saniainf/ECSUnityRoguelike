﻿using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    /// <summary>
    /// расширения для World класса
    /// </summary>
    static class RLWorldExtension
    {
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
            dataComponent.Buffs = new NPCBuffs();
        }

        public static void RLCreatePlayer(this EcsWorld world, Vector2 position, NPCDataSheet data)
        {
            var go = VExt.LayoutAnimationObject(ObjData.r_PrefabPhysicsAnimation, position, "player", ObjData.t_GameObjectsRoot, LayersName.Character.ToString(), ObjData.p_PlayerPreset.Animation);
            var e = world.NewEntityWithGameObject(go, true);
            e.Set<PlayerComponent>();
            var dataComponent = e.Set<DataSheetComponent>();
            dataComponent.Stats = data.NPCStats;
            dataComponent.PrimaryWeapon = data.PriamaryWeapon;
            dataComponent.SecondaryWeapon = data.SecondaryWeapon;
            dataComponent.Buffs = new NPCBuffs();
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
