using UnityEngine;
using Leopotam.Ecs;
using LeopotamGroup.Globals;

namespace Client
{
    /// <summary>
    /// расширения для World класса
    /// </summary>
    static class RLWorldExtension
    {
        public static void RLCreateEffect(this EcsWorld world, Vector2 position, EffectPreset effectPreset)
        {
            var go = VExt.LayoutSpriteObject(ObjData.r_PrefabSprite, position, ObjData.t_GameObjectsOther, SortingLayer.Effect.ToString(), effectPreset.spriteSingle);
            var e = world.NewEntityWithGameObject(go);
            e.Set<EffectComponent>().Duration = effectPreset.duration;
        }

        public static void RLCreateEnemy(this EcsWorld world, Vector2 position, EnemyPreset enemyPreset)
        {
            var go = VExt.LayoutAnimationObject(ObjData.r_PrefabPhysicsAnimation, position, enemyPreset.PresetName, ObjData.t_GameObjectsRoot, SortingLayer.Character.ToString(), enemyPreset.Animation);
            var e = world.NewEntityWithGameObject(go, true);
            e.Set<EnemyComponent>();
            var dataComponent = e.Set<DataSheetComponent>();
            dataComponent.Stats = new NPCStats(enemyPreset);
            dataComponent.PrimaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
            dataComponent.SecondaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
            dataComponent.Buffs = new NPCBuffs();
        }

        public static void RLCreatePlayer(this EcsWorld world, Vector2 position)
        {
            var go = VExt.LayoutAnimationObject(ObjData.r_PrefabPhysicsAnimation, position, "player", ObjData.t_GameObjectsRoot, SortingLayer.Character.ToString(), ObjData.p_PlayerPreset.Animation);
            var e = world.NewEntityWithGameObject(go, true);
            e.Set<PlayerComponent>();
            var dataComponent = e.Set<DataSheetComponent>();
            dataComponent.Stats = Service<NPCDataSheet>.Get().NPCStats;
            dataComponent.PrimaryWeapon = Service<NPCDataSheet>.Get().PriamaryWeapon;
            dataComponent.SecondaryWeapon = Service<NPCDataSheet>.Get().SecondaryWeapon;
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

        public static EcsEntity RLNewLevelTile(this EcsWorld world, LevelTilePreset preset, Vector2 pos)
        {
            var go = VExt.NewGameObject(preset.GameObject, pos);
            var e = world.NewEntityWith(out GameObjectComponent c);
            c.GObj = go.GetComponent<PrefabComponentsShortcut>();
            return e;
        }
    }
}
