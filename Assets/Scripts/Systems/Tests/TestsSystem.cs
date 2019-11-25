using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    
    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<GameObjectComponent, DataSheetComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _player = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        /*
        readonly EcsWorld _world = null;
        readonly WorldObjects _worldObjects = null;
        readonly WorldStatus _worldStatus = null;


        void IEcsInitSystem.Init()
        {

        }

        void IEcsRunSystem.Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var i in _player)
                {
                    var c1 = _player.Get1[i];

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
            _world.NewEntityWith(out GameObjectCreateEvent gameObjectCreateEvent, out ActionMoveComponent actionMoveComponent);

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
                var c1 = _collisionEntities.Get1[i];

                c1.GOcomps.SpriteRenderer.color = Color.white;

                if (c1.GOcomps.Collider.OverlapPoint(worldPos3D))
                {
                    var targetPoint = c1.GOcomps.Rigidbody.position;
                    var playerPoint = _player.Get1[0].GOcomps.Rigidbody.position;
                    var playerColider = _player.Get1[0].GOcomps.Collider;
                    RaycastHit2D[] hit = new RaycastHit2D[1];

                    var count = playerColider.Raycast(targetPoint - playerPoint, hit);

                    if (count != 0)
                    {
                        Debug.DrawLine(playerPoint, hit[0].point);
                        if (hit[0].collider == c1.GOcomps.Collider)
                        {
                            c1.GOcomps.SpriteRenderer.color = Color.red;
                        }
                    }
                }
            }
        }

        void IEcsInitSystem.Init()
        {

        }

    }
}