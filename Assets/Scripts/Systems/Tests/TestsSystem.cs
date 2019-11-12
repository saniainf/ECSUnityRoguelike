using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<PositionComponent, PlayerComponent> _player = null;
        readonly EcsFilter<PositionComponent, ObstacleComponent> _obstacleEntities = null;

        /*
        readonly EcsWorld _world = null;
        readonly WorldObjects _worldObjects = null;
        readonly WorldStatus _worldStatus = null;


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

        void LayoutProjectile(float x, float y)
        {
            var go = VExt.LayoutSpriteObjects(_worldObjects.ResourcesPresets.PrefabSprite, x, y, "arrow", _worldStatus.ParentOtherObject, LayersName.Object.ToString(), _worldObjects.ArrowPreset.spriteSingle);
            _world.CreateEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out ActionMoveComponent actionMoveComponent);

            gameObjectCreateEvent.Transform = go.transform;
            gameObjectCreateEvent.Rigidbody = go.GetComponent<Rigidbody2D>();

            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var endPosition = worldPos;
            var direction = new Vector2(worldPos.x, worldPos.y) - new Vector2(x, y).normalized;

            actionMoveComponent.GoalDirection = new Vector2(1, 0);
            actionMoveComponent.Speed = 10f;
        }
        */

        void IEcsRunSystem.Run()
        {
            var worldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var worldPos2D = new Vector2(worldPos3D.x, worldPos3D.y);

            foreach (var i in _player)
            {
                var pc1 = _player.Components1[i];
                Debug.DrawRay(pc1.Rigidbody.position, (worldPos2D - pc1.Rigidbody.position));

                RaycastHit2D[] hit2D = Physics2D.RaycastAll(pc1.Rigidbody.position, (worldPos2D - pc1.Rigidbody.position));

                foreach (var j in _collisionEntities)
                {
                    var cc1 = _collisionEntities.Components1[j];
                    var sr = cc1.Transform.gameObject.GetComponent<SpriteRenderer>();

                    foreach (var item in hit2D)
                    {
                        if (item.collider == cc1.Collider)
                        {
                            sr.color = Color.red;
                        }
                    }

                }
            }

        }

        void IEcsInitSystem.Destroy()
        {

        }

        void IEcsInitSystem.Initialize()
        {

        }

    }
}