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
        readonly EcsFilter<TargetTileComponent> _targetTiles = null;

        void IEcsRunSystem.Run()
        {
            KeyboardInput();
            MouseInput();
        }

        void MouseInput()
        {
            foreach (var i in _targetTiles)
            {
                var tc = _targetTiles.Get1[i];

                switch (tc.AttackType)
                {
                    case AttackType.None:
                        break;
                    case AttackType.Melee:
                        if (Input.GetMouseButtonDown(0))
                            MeleeAttack(tc);
                        break;
                    case AttackType.Range:
                        if (Input.GetMouseButtonDown(1))
                            RangeAttack(tc);
                        break;
                    default:
                        break;
                }
            }
        }

        void MeleeAttack(TargetTileComponent target)
        {
            foreach (var i in _inputPhaseEntities)
            {
                var ic1 = _inputPhaseEntities.Get1[i];
                var ic2 = _inputPhaseEntities.Get2[i];

                ic2.InputCommand = new InputComAttackMelee(target.Target, target.TargetPos);
                ic1.PhaseEnd = true;
            }
        }

        void RangeAttack(TargetTileComponent target)
        {
            foreach (var i in _inputPhaseEntities)
            {
                var ic1 = _inputPhaseEntities.Get1[i];
                var ic2 = _inputPhaseEntities.Get2[i];

                ic2.InputCommand = new InputComAttackRange(target.Target, target.TargetPos);
                ic1.PhaseEnd = true;
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
