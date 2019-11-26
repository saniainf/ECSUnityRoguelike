using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ввод npc, когда его ход и фаза ввода
    /// </summary>

    sealed class InputNPCPhaseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly EcsFilter<InputPhaseComponent, TurnComponent, EnemyComponent> _inputPhaseEntities = null;
        readonly EcsFilter<GameObjectComponent, PlayerComponent> _playerEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _inputPhaseEntities)
            {
                var e = _inputPhaseEntities.Entities[i];
                var c1 = _inputPhaseEntities.Get1[i];
                var c2 = _inputPhaseEntities.Get2[i];
                var cgo = _inputPhaseEntities.Entities[i].Get<GameObjectComponent>();
                Vector2 goalPosition = Vector2.zero;
                bool skip = false;

                foreach (var j in _playerEntities)
                {
                    var pc1 = _playerEntities.Get1[j];

                    if (cgo.Transform.position.y == pc1.Transform.position.y)
                    {
                        if (cgo.Transform.position.x - 1 == pc1.Transform.position.x)
                        {
                            goalPosition = new Vector2(cgo.Transform.position.x - 1, cgo.Transform.position.y);
                            Debug.Log($"entity: {e.GetInternalId()} | атакует противника слева");
                        }
                        if (cgo.Transform.position.x + 1 == pc1.Transform.position.x)
                        {
                            goalPosition = new Vector2(cgo.Transform.position.x + 1, cgo.Transform.position.y);
                            Debug.Log($"entity: {e.GetInternalId()} | атакует противника справа");
                        }
                    }
                    else if (cgo.Transform.position.x == pc1.Transform.position.x)
                    {
                        if (cgo.Transform.position.y - 1 == pc1.Transform.position.y)
                        {
                            goalPosition = new Vector2(cgo.Transform.position.x, cgo.Transform.position.y - 1);
                            Debug.Log($"entity: {e.GetInternalId()} | атакует противника снизу");
                        }
                        if (cgo.Transform.position.y + 1 == pc1.Transform.position.y)
                        {
                            goalPosition = new Vector2(cgo.Transform.position.x, cgo.Transform.position.y + 1);
                            Debug.Log($"entity: {e.GetInternalId()} | атакует противника сверху");
                        }
                    }
                    else
                    {
                        if (UnityEngine.Random.value < 0.7f)
                        {
                            skip = true;
                            Debug.Log($"entity: {e.GetInternalId()} | решил пропустить ход");
                        }
                        else
                        {
                            var d = UnityEngine.Random.Range(0, 3);
                            Debug.Log($"--random {d}--");
                            switch (d)
                            {
                                case 0:
                                    goalPosition = new Vector2(cgo.Transform.position.x - 1, cgo.Transform.position.y);
                                    break;
                                case 1:
                                    goalPosition = new Vector2(cgo.Transform.position.x + 1, cgo.Transform.position.y);
                                    break;
                                case 2:
                                    goalPosition = new Vector2(cgo.Transform.position.x, cgo.Transform.position.y - 1);
                                    break;
                                case 3:
                                    goalPosition = new Vector2(cgo.Transform.position.x, cgo.Transform.position.y + 1);
                                    break;
                                default:
                                    skip = true;
                                    break;
                            }

                            Debug.Log($"entity: {e.GetInternalId()} | решил пойти в {goalPosition.x}, {goalPosition.y}");
                        }
                    }
                }

                c1.PhaseEnd = true;
                c2.SkipTurn = skip;
                c2.GoalPosition = goalPosition;
                c2.ActionType = ActionType.Move;
            }
        }
    }
}
