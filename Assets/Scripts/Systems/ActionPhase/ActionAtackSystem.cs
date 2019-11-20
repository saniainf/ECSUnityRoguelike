using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление атакой чара в фазу действия
    /// </summary>
    [EcsInject]
    sealed class ActionAtackSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        EcsFilter<ActionAtackComponent, AnimationComponent, DataSheetComponent> _atackEntities = null;

        private float atackTime = 0.5f;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _atackEntities)
            {
                ref var e = ref _atackEntities.Entities[i];
                var c1 = _atackEntities.Components1[i];
                var c2 = _atackEntities.Components2[i];
                var c3 = _atackEntities.Components3[i];

                if (!c1.Run)
                {
                    c1.Run = true;
                    var c = _world.AddComponent<ActionAnimationComponent>(e);
                    c.Animation = AnimatorField.AnimationAtack;
                }

                if (c1.Run && !c1.OnAtack && c2.animator.GetFloat(AnimatorField.ActionTime.ToString()) > atackTime)
                {
                    c1.OnAtack = true;
                    _world.RLCreateEffect(c1.TargetPosition, SpriteEffect.Chop, 0.3f);
                    c3.WeaponItem.OnAtack(_world, e, c1.Target);
                }

                if (c1.Run && c1.OnAtack && !c2.animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    _world.RemoveComponent<ActionAtackComponent>(e);
                }
            }
        }
    }
}