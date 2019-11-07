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

        readonly EcsFilter<PositionComponent, PlayerComponent> _player = null;

        void IEcsInitSystem.Initialize()
        {

        }

        void IEcsRunSystem.Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var i in _player)
                {
                    var c1 = _player.Components1[i];

                    LayoutProjectile(c1.Coords.x, c1.Coords.y);
                }
            }
        }

        void IEcsInitSystem.Destroy()
        {

        }

        void LayoutProjectile(int x, int y)
        {
            var go = VExt.LayoutSpriteObjects(_worldObjects.ResourcesPresets.PrefabSprite, x, y, "arrow", _worldStatus.ParentOtherObject, LayersName.Object.ToString(), _worldObjects.ArrowPreset.spriteSingle);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out ActionMoveComponent actionMoveComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int endPosition = Vector2Int.RoundToInt(worldPos);

            actionMoveComponent.EndPosition = endPosition;
        }

    }
}