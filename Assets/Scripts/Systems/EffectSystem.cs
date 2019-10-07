using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class EffectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<SpriteEffectCreateEvent> _createEffectEntities = null;
        readonly EcsFilter<SpriteEffectComponent> _effectEntities = null;

        readonly Sprite chopEffect = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet")[55];
        readonly GameObject prefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");

        void IEcsRunSystem.Run()
        {
            foreach (var i in _effectEntities)
            {
                ref var entity = ref _effectEntities.Entities[i];
                var c1 = _effectEntities.Components1[i];

                c1.LifeTime -= Time.deltaTime;

                if (c1.LifeTime <= 0)
                {
                    _world.AddComponent<GameObjectRemoveEvent>(entity);
                }
            }

            foreach (var i in _createEffectEntities)
            {
                ref var entity = ref _createEffectEntities.Entities[i];
                var c1 = _createEffectEntities.Components1[i];
                GameObject go = null;

                switch (c1.SpriteEffect)
                {
                    case SpriteEffect.NONE:
                        break;
                    case SpriteEffect.CHOP:
                        go = VExt.LayoutSpriteObjects(prefabSprite, c1.Position.x, c1.Position.y, LayersName.Effect.ToString(), chopEffect);
                        break;
                    default:
                        break;
                }

                _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out SpriteEffectComponent effect);
                gameObjectCreateEvent.Transform = go.transform;
                effect.LifeTime = c1.LifeTime;
            }
        }
    }
}