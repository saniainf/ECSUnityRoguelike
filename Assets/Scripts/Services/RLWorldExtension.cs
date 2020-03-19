using UnityEngine;
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// расширения для World класса
    /// </summary>
    static class RLWorldExtension
    {
        public static void RLApplyDamage(this EcsWorld world, EcsEntity target, EcsEntity caster, int value)
        {
            world.NewEntityWith(out ApplyDamageComponent damage);
            damage.Target = target;
            damage.Caster = caster;
            damage.DamageValue = value;
        }

        public static void RLApplySpell(this EcsWorld world, EcsEntity target, EcsEntity caster, SpellPreset preset)
        {
            world.NewEntityWith(out ApplySpellComponent spell);
            spell.Target = target;
            spell.Caster = caster;
            spell.Spell = preset;
        }

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
            var data = e.Set<NPCDataSheetComponent>();
            data.Stats = new NPCStats(enemyPreset);
            data.PrimaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
            data.SecondaryWeapon = new NPCWeapon(enemyPreset.PrimaryWeaponItem, new WB_DamageOnContact());
            data.StatusEffects = new List<StatusEffect>();
        }


        public static EcsEntity NewEntityWithGameObject(this EcsWorld world, GameObject go, bool nameID = false)
        {
            var e = world.NewEntityWith(out GameObjectComponent goComponent);
            goComponent.Transform = go.transform;
            goComponent.GO = go.GetComponent<PrefabComponentsShortcut>();
            if (nameID)
            {
                goComponent.GO.NPCNameText.text = e.GetInternalId().ToString();
            }
            return e;
        }

        public static EcsEntity RLNewLevelObject(this EcsWorld world, LevelTilePreset preset, Vector2 pos)
        {
            var go = VExt.NewGameObject(preset.GameObject, pos);
            var e = world.NewEntityWithGameObject(go, false);

            if (preset.Obstacle)
                e.Set<ObstacleComponent>();
            if (preset.ExitPoint)
                e.Set<ExitPointComponent>();

            return e;
        }

        public static EcsEntity RLNewLevelObject(this EcsWorld world, PlayerPreset preset, Vector2 pos)
        {
            var go = VExt.NewGameObject(preset.GameObject, pos);
            var e = world.NewEntityWithGameObject(go, false);
            e.Set<PlayerComponent>();
            var data = e.Set<NPCDataSheetComponent>();
            data.Stats = Service<NPCDataSheet>.Get().NPCStats;
            data.PrimaryWeapon = Service<NPCDataSheet>.Get().PriamaryWeapon;
            data.SecondaryWeapon = Service<NPCDataSheet>.Get().SecondaryWeapon;
            data.StatusEffects = new List<StatusEffect>();
            return e;
        }

        public static EcsEntity RLNewLevelObject(this EcsWorld world, CollectingItemPreset preset, Vector2 pos)
        {
            var go = VExt.NewGameObject(preset.GameObject, pos);
            var e = world.NewEntityWithGameObject(go, false);
            var c = e.Set<CollectItemComponent>();
            c.Spell = preset.Spell;
            return e;
        }
    }
}
