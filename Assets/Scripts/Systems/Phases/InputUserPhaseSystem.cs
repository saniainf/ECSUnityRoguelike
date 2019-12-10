using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ввод игрока, когда его ход и фаза ввода
    /// </summary>
    sealed class InputUserPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, TurnComponent, PlayerComponent> _inputPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, DataSheetComponent>.Exclude<PlayerComponent> _collisionEntities = null;

        readonly EcsFilter<TargetTileComponent> _targetTiles = null;

        bool atackMeele = false;

        void IEcsRunSystem.Run()
        {
            KeyboardInput();
            MouseInput();
        }

        void MouseInput()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var i in _inputPhaseEntities)
            {
                var ic1 = _inputPhaseEntities.Get1[i];
                var ic2 = _inputPhaseEntities.Get2[i];
                var goc = _inputPhaseEntities.Entities[i].Get<GameObjectComponent>();
                var ie = _inputPhaseEntities.Entities[i];

                var playerPoint = new Vector2(Mathf.Round(goc.Transform.position.x), Mathf.Round(goc.Transform.position.y));
                var targetPoint = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

                foreach (var j in _collisionEntities)
                {
                    var cc1 = _collisionEntities.Get1[j];
                    var ce = _collisionEntities.Entities[j];

                    if (cc1.GObj.Collider.OverlapPoint(targetPoint))
                    {
                        var playerColider = goc.GObj.Collider;
                        RaycastHit2D[] hit = new RaycastHit2D[1];

                        var count = playerColider.Raycast(targetPoint - playerPoint, hit);

                        if (count != 0)
                        {
                            if ((targetPoint - playerPoint).sqrMagnitude == 1.0f)
                            {
                                Debug.DrawLine(playerPoint, targetPoint, Color.red);

                                if (Input.GetMouseButtonDown(0))
                                {
                                    ic2.InputCommand = new InputComAtack(ce, cc1.Transform.position);
                                    ic1.PhaseEnd = true;
                                }
                            }
                            else
                            {
                                Debug.DrawLine(playerPoint, hit[0].point, Color.green);
                                if (hit[0].collider == cc1.GObj.Collider)
                                {
                                    if (Input.GetMouseButtonDown(1))
                                    {
                                        ic2.InputCommand = new InputComAtack(ce, cc1.Transform.position);
                                        ic1.PhaseEnd = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void KeyboardInput()
        {
            foreach (var i in _inputPhaseEntities)
            {
                ref var e = ref _inputPhaseEntities.Entities[i];
                var c1 = _inputPhaseEntities.Get1[i];
                var c2 = _inputPhaseEntities.Get2[i];
                var goc = _inputPhaseEntities.Entities[i].Get<GameObjectComponent>();

                float horizontal = (int)Input.GetAxis("Horizontal");
                float vertical = (int)Input.GetAxis("Vertical");

                if (horizontal != 0 || vertical != 0)
                {
                    c2.InputCommand = new InputComOneStepOnDirection(horizontal, vertical);
                    c1.PhaseEnd = true;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    c2.InputCommand = new InputComEmpty();
                    c1.PhaseEnd = true;
                }
            }
        }
    }
}
