using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление атакой чара в фазу действия
    /// </summary>

    sealed class ActionAttackSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        EcsFilter<ActionAttackComponent, GameObjectComponent, NPCDataSheetComponent> _attackingEntities = null;

        private float attackTime = 0.5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _attackingEntities)
            {
                ref var e = ref _attackingEntities.Entities[i];
                var c1 = _attackingEntities.Get1[i];
                var c2 = _attackingEntities.Get2[i];
                var c3 = _attackingEntities.Get3[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    var c = e.Set<ActionAnimationComponent>();
                    c.Animation = AnimatorField.AnimationAttack;
                }

                if (c1.Run && !c1.OnAttack && c2.GO.Animator.GetFloat(AnimatorField.ActionTime.ToString()) > attackTime)
                {
                    c1.OnAttack = true;
                    if (c1.PrimaryOrSecondaryWeapon)
                    {
                        _world.RLCreateEffect(c1.TargetPosition, c3.PrimaryWeapon.HitEffect);
                        _world.RLApplyDamage(c1.Target, e, c3.PrimaryWeapon.Damage);
                    }
                    else
                    {
                        var go = VExt.LayoutSpriteObject(
                            ObjData.r_PrefabPhysicsSprite,
                            c2.GO.Rigidbody.position,
                            ObjData.t_GameObjectsOther,
                            SortingLayer.Effect.ToString(),
                            c3.SecondaryWeapon.ProjectileSprite);

                        _world.NewEntityWith(out GameObjectComponent goComponent, out ProjectileComponent projectileComponent);
                        goComponent.Transform = go.transform;
                        goComponent.GO = go.GetComponent<PrefabComponentsShortcut>();

                        projectileComponent.StartPosition = c2.GO.Rigidbody.position;
                        projectileComponent.GoalPosition = c1.TargetPosition;
                        projectileComponent.Caster = e;
                        projectileComponent.Target = c1.Target;
                        projectileComponent.Weapon = c3.SecondaryWeapon;
                    }
                }

                if (c1.Run && c1.OnAttack && !c2.GO.Animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    e.Unset<ActionAttackComponent>();
                }
            }
        }
    }
}