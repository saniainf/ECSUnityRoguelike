using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ввод игрока, когда его ход и фаза ввода
    /// </summary>
    [EcsInject]
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
                var ic2 = _inputPhaseEntities.Components2[i];

                var playerPoint = new Vector2(Mathf.Round(ic2.Transform.position.x), Mathf.Round(ic2.Transform.position.y));
                var targetPoint = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

                if ((targetPoint - playerPoint).sqrMagnitude == 1.0f)
                {
                    foreach (var j in _collisionEntities)
                    {
                        var cc1 = _collisionEntities.Components1[j];
                        cc1.GOcomps.SpriteRenderer.color = Color.white;

                        if (cc1.GOcomps.Collider.OverlapPoint(targetPoint))
                        {
                            Debug.DrawLine(playerPoint, targetPoint, Color.red);
                            cc1.GOcomps.SpriteRenderer.color = Color.red;
                        }
                    }

                }
            }
        }

        void KeyboardInput()
        {
            float horizontal = (int)Input.GetAxis("Horizontal");
            float vertical = (int)Input.GetAxis("Vertical");

            if (horizontal != 0)
                vertical = 0;

            if (horizontal != 0 || vertical != 0)
            {
                MoveDirection direction;

                if (vertical == 0)
                    direction = horizontal > 0 ? MoveDirection.Right : MoveDirection.Left;
                else
                    direction = vertical > 0 ? MoveDirection.Up : MoveDirection.Down;

                foreach (var i in _inputPhaseEntities)
                {
                    ref var e = ref _inputPhaseEntities.Entities[i];
                    var c1 = _inputPhaseEntities.Components1[i];
                    var c = _world.AddComponent<InputActionComponent>(e);
                    c.MoveDirection = direction;
                    c.InputAction = InputType.Move;
                    c1.PhaseEnd = true;
                }
            }
        }
    }
}