using Leopotam.Ecs;
using UnityEngine;

namespace Client
{

    sealed class TestsSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly WorldStatus _worldStatus = null;

        readonly EcsFilter<InputPhaseComponent, TurnComponent, PlayerComponent> _inputPhaseEntities = null;

        readonly EcsFilter<GameObjectComponent, DataSheetComponent>.Exclude<PlayerComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _player = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;

        readonly EcsFilter<TileOverlayComponent> _tileOverlays = null;

        Vector2 newTargetPoint;
        Vector2 targetPoint = Vector2.zero;

        void IEcsRunSystem.Run()
        {
            if (_inputPhaseEntities.GetEntitiesCount() > 0)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newTargetPoint = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

                if (_tileOverlays.GetEntitiesCount() == 0)
                {
                    CreateTileOverlay(newTargetPoint);
                    targetPoint = newTargetPoint;
                }
                else if (targetPoint != newTargetPoint)
                {
                    ClearTileOverlay();
                    CreateTileOverlay(newTargetPoint);
                    targetPoint = newTargetPoint;
                }
            }
            else
            {
                ClearTileOverlay();
            }

        }

        void IEcsInitSystem.Init()
        {

        }

        void ClearTileOverlay()
        {
            foreach (var i in _tileOverlays)
            {
                _tileOverlays.Entities[i].RLDestoryGO();
            }
        }

        void CreateTileOverlay(Vector2 target)
        {
            if (!CheckObstacleCollision(target))
            {
                var go = VExt.LayoutSpriteObjects(
                    ObjData.r_PrefabSprite,
                    target.x, target.y,
                    "tileOverlay",
                    ObjData.t_GameObjectsOther,
                    LayersName.TileOverlay.ToString(),
                    ObjData.p_Overlay.spriteSingle);

                _world.NewEntityWith(out GameObjectComponent goComponent, out TileOverlayComponent _);
                goComponent.Transform = go.transform;
                goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();
                goComponent.GObj.SpriteRenderer.color = TileOverlayColor(target);
            }
        }

        bool CheckObstacleCollision(Vector2 goalPosition)
        {
            foreach (var i in _obstacleEntities)
            {
                ref var wallEntity = ref _obstacleEntities.Entities[i];
                var c1 = _obstacleEntities.Get1[i];

                if (c1.GObj.Collider.OverlapPoint(goalPosition))
                {
                    return true;
                }
            }
            return false;
        }

        Color TileOverlayColor(Vector2 target)
        {
            var result = Color.white;

            foreach (var i in _inputPhaseEntities)
            {
                var goc = _inputPhaseEntities.Entities[i].Get<GameObjectComponent>();
                var ie = _inputPhaseEntities.Entities[i];

                var playerPoint = new Vector2(Mathf.Round(goc.Transform.position.x), Mathf.Round(goc.Transform.position.y));

                foreach (var j in _collisionEntities)
                {
                    var cc1 = _collisionEntities.Get1[j];
                    var ce = _collisionEntities.Entities[j];

                    if (cc1.GObj.Collider.OverlapPoint(target))
                    {
                        var playerColider = goc.GObj.Collider;
                        RaycastHit2D[] hit = new RaycastHit2D[1];

                        var count = playerColider.Raycast(target - playerPoint, hit);

                        if (count != 0)
                        {
                            if ((target - playerPoint).sqrMagnitude == 1.0f)
                            {
                                result = Color.red;
                            }
                            else
                            {
                                if (hit[0].collider == cc1.GObj.Collider)
                                {
                                    result = Color.yellow;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}