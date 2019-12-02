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

        void IEcsRunSystem.Run()
        {
            KeyboardInput();
            //MouseInput();
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
                    cc1.GObj.SpriteRenderer.color = Color.white;

                    //todo здесь будут координаты точки удара из щаблона удара 
                    if (((targetPoint - playerPoint).sqrMagnitude == 1.0f) &&
                        (cc1.GObj.Collider.OverlapPoint(targetPoint)))
                    {
                        Debug.DrawLine(playerPoint, targetPoint, Color.red);
                        cc1.GObj.SpriteRenderer.color = Color.red;

                        if (Input.GetMouseButtonDown(0))
                        {
                            ic1.PhaseEnd = true;

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
                Vector2 goalPosition = Vector2.zero;


                if (horizontal != 0)
                    vertical = 0;

                if (horizontal != 0 || vertical != 0)
                {
                    InputComOneStepOnDirection oneStepOnDirection = null;

                    if (vertical > 0)
                    {
                        oneStepOnDirection = new InputComOneStepOnDirection(Direction.Up);
                        goalPosition = new Vector2(goc.Transform.position.x, goc.Transform.position.y + 1);
                    }

                    if (vertical < 0)
                    {
                        oneStepOnDirection = new InputComOneStepOnDirection(Direction.Down);
                        goalPosition = new Vector2(goc.Transform.position.x, goc.Transform.position.y - 1);
                    }

                    if (horizontal > 0)
                    {
                        oneStepOnDirection = new InputComOneStepOnDirection(Direction.Right);
                        goalPosition = new Vector2(goc.Transform.position.x + 1, goc.Transform.position.y);
                    }

                    if (horizontal < 0)
                    {
                        oneStepOnDirection = new InputComOneStepOnDirection(Direction.Left);
                        goalPosition = new Vector2(goc.Transform.position.x - 1, goc.Transform.position.y);
                    }

                    c1.PhaseEnd = true;
                    //var icom = new InputComOneStep(goalPosition);
                    c2.InputCommand = oneStepOnDirection;

                    Debug.Log($"entity: {e.GetInternalId()} | ввод с клавиатуры: сместить в {goalPosition.x}, {goalPosition.y}");
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    c1.PhaseEnd = true;
                    var icom = new InputComSkipTurn();
                    c2.InputCommand = icom;
                }
            }
        }
    }
}
