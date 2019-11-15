using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class EffectSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<SpriteEffectComponent> _effectEntities = null;

        readonly Sprite chopEffect = Resources.LoadAll<Sprite>("Sprites/Scavengers_SpriteSheet")[55];
        readonly GameObject prefabSprite = Resources.Load<GameObject>("Prefabs/PrefabSprite");

        void IEcsRunSystem.Run()
        {
            foreach (var i in _effectEntities)
            {
                ref var entity = ref _effectEntities.Entities[i];
                var c1 = _effectEntities.Components1[i];

                if (!c1.Run)
                {
                    c1.Run = true;

                    GameObject go = null;

                    switch (c1.SpriteEffect)
                    {
                        case SpriteEffect.None:
                            break;
                        case SpriteEffect.Chop:
                            go = VExt.LayoutSpriteObjects(prefabSprite, c1.Position.x, c1.Position.y, _worldStatus.ParentOtherObject, LayersName.Effect.ToString(), chopEffect);
                            break;
                        default:
                            break;
                    }

                    var c2 = _world.AddComponent<GameObjectComponent>(entity);
                    c2.Transform = go.transform;
                }
                else
                {
                    c1.LifeTime -= Time.deltaTime;

                    if (c1.LifeTime <= 0)
                    {
                        _world.RLRemoveGOEntity(entity);
                    }
                }
            }
        }
    }
}