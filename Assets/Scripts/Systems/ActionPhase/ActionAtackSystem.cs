using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление атакой чара в фазу действия
    /// </summary>

    sealed class ActionAtackSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        EcsFilter<ActionAtackComponent, GameObjectComponent, DataSheetComponent> _atackEntities = null;

        private float atackTime = 0.5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _atackEntities)
            {
                ref var e = ref _atackEntities.Entities[i];
                var c1 = _atackEntities.Get1[i];
                var c2 = _atackEntities.Get2[i];
                var c3 = _atackEntities.Get3[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    var c = e.Set<ActionAnimationComponent>();
                    c.Animation = AnimatorField.AnimationAtack;
                }

                if (c1.Run && !c1.OnAtack && c2.GObj.Animator.GetFloat(AnimatorField.ActionTime.ToString()) > atackTime)
                {
                    c1.OnAtack = true;
                    if (c1.PrimaryOrSecondaryWeapon)
                    {
                        _world.RLCreateEffect(c1.TargetPosition, c3.PrimaryWeapon.HitEffect);
                        c3.PrimaryWeapon.Behaviour.OnAtack(e, c1.Target);
                    }
                    else
                    {
                        var go = VExt.LayoutSpriteObject(
                            ObjData.r_PrefabPhysicsSprite,
                            c2.GObj.Rigidbody.position,
                            ObjData.t_GameObjectsOther,
                            LayersName.Effect.ToString(),
                            c3.SecondaryWeapon.ProjectileSprite);

                        _world.NewEntityWith(out GameObjectComponent goComponent, out ProjectileComponent projectileComponent);
                        goComponent.Transform = go.transform;
                        goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();

                        projectileComponent.StartPosition = c2.GObj.Rigidbody.position;
                        projectileComponent.GoalPosition = c1.TargetPosition;
                        projectileComponent.Caster = e;
                        projectileComponent.Target = c1.Target;
                        projectileComponent.Weapon = c3.SecondaryWeapon;
                    }
                }

                if (c1.Run && c1.OnAtack && !c2.GObj.Animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    e.Unset<ActionAtackComponent>();
                }
            }
        }
    }
}