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
        readonly EcsFilter<InputPhaseComponent, GameObjectComponent, PlayerComponent> _inputPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, DataSheetComponent>.Exclude<PlayerComponent> _collisionEntities = null;

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
                var ie = _inputPhaseEntities.Entities[i];

                var playerPoint = new Vector2(Mathf.Round(ic2.Transform.position.x), Mathf.Round(ic2.Transform.position.y));
                var targetPoint = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

                foreach (var j in _collisionEntities)
                {
                    var cc1 = _collisionEntities.Get1[j];
                    cc1.GOcomps.SpriteRenderer.color = Color.white;

                    //todo здесь будут координаты точки удара из щаблона удара 
                    if (((targetPoint - playerPoint).sqrMagnitude == 1.0f) &&
                        (cc1.GOcomps.Collider.OverlapPoint(targetPoint)))
                    {
                        Debug.DrawLine(playerPoint, targetPoint, Color.red);
                        cc1.GOcomps.SpriteRenderer.color = Color.red;

                        if (Input.GetMouseButtonDown(0))
                        {
                            var c = ie.Set<InputActionComponent>();
                            c.GoalPosition = targetPoint;
                            c.InputActionType = ActionType.UseActiveItem;
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

                float horizontal = (int)Input.GetAxis("Horizontal");
                float vertical = (int)Input.GetAxis("Vertical");
                Vector2 goalPosition = Vector2.zero; 

                if (horizontal != 0)
                    vertical = 0;

                if (horizontal != 0 || vertical != 0)
                {
                    if (vertical > 0)
                        goalPosition = new Vector2(c2.Transform.position.x, c2.Transform.position.y + 1);

                    if (vertical < 0)
                        goalPosition = new Vector2(c2.Transform.position.x, c2.Transform.position.y - 1);

                    if (horizontal > 0)
                        goalPosition = new Vector2(c2.Transform.position.x + 1, c2.Transform.position.y);

                    if (horizontal < 0)
                        goalPosition = new Vector2(c2.Transform.position.x - 1, c2.Transform.position.y);

                    var c = e.Set<InputActionComponent>();
                    c.GoalPosition = goalPosition;
                    c.InputActionType = ActionType.Move;
                    c1.PhaseEnd = true;
                }
            }
        }
    }
}
