using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class ActionAtackSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        EcsFilter<ActionAtackComponent, AnimationComponent, DataSheetComponent> _atackEntities = null;

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
                    c.Animation = AnimationTriger.AnimationAtack;
                }
                else if (c2.animator.GetBool(AnimationTriger.ActionOnAtack.ToString()))
                {
                    CreateEffect(new Vector2Int(c1.TargetPosition.x, c1.TargetPosition.y), SpriteEffect.Chop, 0.3f);
                    _world.EnsureComponent<ImpactEvent>(c1.Target, out _).HitValue += c3.HitDamage;
                }
                else if (!c2.animator.GetBool(AnimationTriger.ActionRun.ToString()))
                {
                    _world.RemoveComponent<ActionAtackComponent>(e);
                }
            }
        }

        void CreateEffect(Vector2Int position, SpriteEffect effect, float lifeTime)
        {
            _world.CreateEntityWith(out SpriteEffectCreateEvent spriteEffect);
            spriteEffect.Position = position;
            spriteEffect.SpriteEffect = effect;
            spriteEffect.LifeTime = lifeTime;
        }
    }
}