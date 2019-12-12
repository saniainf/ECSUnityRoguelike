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

                    Debug.Log($"entity: {e.GetInternalId()} | запущена action атака: entity: {c1.Target.GetInternalId()}");
                }

                if (c1.Run && !c1.OnAtack && c2.GObj.Animator.GetFloat(AnimatorField.ActionTime.ToString()) > atackTime)
                {
                    c1.OnAtack = true;
                    _world.RLCreateEffect(c1.TargetPosition, SpriteEffect.Chop, 0.3f);
                    if (c1.PrimaryOrSecondaryWeapon)
                        c3.PrimaryWeaponItem.WeaponBehaviour.OnAtack(e, c1.Target);
                    else
                    {

                        c3.SecondaryWeaponItem.WeaponBehaviour.OnAtack(e, c1.Target);
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