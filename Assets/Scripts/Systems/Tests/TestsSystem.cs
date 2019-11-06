using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldObjects _worldObjects = null;
        readonly WorldStatus _worldStatus = null;

        void IEcsInitSystem.Initialize()
        {
            LayoutProjectile();
        }

        void IEcsRunSystem.Run()
        {

        }

        void IEcsInitSystem.Destroy()
        {

        }

        void LayoutProjectile()
        {
            var go = VExt.LayoutSpriteObjects(_worldObjects.ResourcesPresets.PrefabSprite, 3, 3, "arrow", _worldStatus.ParentOtherObject, LayersName.Object.ToString(), _worldObjects.ArrowPreset.spriteSingle);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();
        }

    }
}