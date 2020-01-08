using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TargetSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<InputPhaseComponent, TurnComponent, PlayerComponent> _inputPlayerEntities = null;
        readonly EcsFilter<GameObjectComponent, DataSheetComponent>.Exclude<PlayerComponent> _collisionEntities = null;
        readonly EcsFilter<GameObjectComponent, ObstacleComponent> _obstacleEntities = null;
        readonly EcsFilter<TargetTileComponent> _targetTiles = null;

        Vector2 newTargetPoint;
        Vector2 targetPoint = Vector2.zero;

        void IEcsRunSystem.Run()
        {
            if (!_inputPlayerEntities.IsEmpty())
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newTargetPoint = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

                if (_targetTiles.IsEmpty())
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

        void ClearTileOverlay()
        {
            foreach (var i in _targetTiles)
            {
                _targetTiles.Entities[i].RLDestoryGO();
            }
        }

        void CreateTileOverlay(Vector2 target)
        {
            if (!CheckObstacleCollision(target))
            {
                var go = VExt.LayoutSpriteObject(
                    ObjData.r_PrefabSprite,
                    target.x, target.y,
                    "tileOverlay",
                    ObjData.t_GameObjectsOther,
                    LayersName.TileOverlay.ToString(),
                    ObjData.p_Overlay.spriteSingle);

                _world.NewEntityWith(out GameObjectComponent goComponent, out TargetTileComponent targetTile);
                goComponent.Transform = go.transform;
                goComponent.GObj = go.GetComponent<PrefabComponentsShortcut>();

                SetupTargetTile(target, ref goComponent, ref targetTile);
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

        void SetupTargetTile(Vector2 target, ref GameObjectComponent goc, ref TargetTileComponent targetTile)
        {
            foreach (var i in _inputPlayerEntities)
            {
                var pgoc = _inputPlayerEntities.Entities[i].Get<GameObjectComponent>();
                var pe = _inputPlayerEntities.Entities[i];

                var playerPoint = new Vector2(Mathf.Round(pgoc.Transform.position.x), Mathf.Round(pgoc.Transform.position.y));

                foreach (var j in _collisionEntities)
                {
                    var cc1 = _collisionEntities.Get1[j];
                    var ce = _collisionEntities.Entities[j];

                    if (cc1.GObj.Collider.OverlapPoint(target))
                    {
                        var playerColider = pgoc.GObj.Collider;
                        RaycastHit2D[] hit = new RaycastHit2D[1];

                        var count = playerColider.Raycast(target - playerPoint, hit);

                        if (count != 0)
                        {
                            if ((target - playerPoint).sqrMagnitude == 1.0f)
                            {
                                Debug.DrawLine(playerPoint, target, Color.red, 1f);
                                goc.GObj.SpriteRenderer.color = Color.red;
                                targetTile.Target = ce;
                                targetTile.TargetPos = target;
                                targetTile.AtackType = AtackType.Melee;
                            }
                            else
                            {
                                if (hit[0].collider == cc1.GObj.Collider)
                                {
                                    Debug.DrawLine(playerPoint, target, Color.yellow, 1f);
                                    goc.GObj.SpriteRenderer.color = Color.yellow;
                                    targetTile.Target = ce;
                                    targetTile.TargetPos = target;
                                    targetTile.AtackType = AtackType.Range;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}