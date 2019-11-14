using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [EcsInject]
    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _player = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

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

                    LayoutProjectile(c1.Transform.position.x, c1.Transform.position.y);
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

            foreach (var i in _collisionEntities)
            {
                var c1 = _collisionEntities.Components1[i];

                c1.SpriteRenderer.color = Color.white;

                if (c1.Collider.OverlapPoint(worldPos3D))
                {
                    var targetPoint = c1.Rigidbody.position;
                    var playerPoint = _player.Components1[0].Rigidbody.position;
                    var playerColider = _player.Components1[0].Collider;
                    RaycastHit2D[] hit = new RaycastHit2D[1];

                    var count = playerColider.Raycast(targetPoint - playerPoint, hit);

                    if (count != 0)
                    {
                        Debug.DrawLine(playerPoint, hit[0].point);
                        if (hit[0].collider == c1.Collider)
                        {
                            c1.SpriteRenderer.color = Color.red;
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