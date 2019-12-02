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
                bool atackPlayer = false;

                Debug.Log($"entity: {e.GetInternalId()} | решает что делать");

                foreach (var j in _playerEntities)
                {
                    var pc1 = _playerEntities.Get1[j];
                    var pe = _playerEntities.Entities[j];

                    Debug.Log($"entity: {e.GetInternalId()} | проверяет нет ли рядом entity: {pe.GetInternalId()}");

                    Vector2 checkPoint = cgo.Transform.position;
                    checkPoint.x = cgo.Transform.position.x - 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | атакует entity: {pe.GetInternalId()} слева");
                        continue;
                    }

                    checkPoint.x = cgo.Transform.position.x + 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | атакует entity: {pe.GetInternalId()} справа");
                        continue;
                    }

                    checkPoint = cgo.Transform.position;
                    checkPoint.y = cgo.Transform.position.y + 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | атакует entity: {pe.GetInternalId()} сверху");
                        continue;
                    }

                    checkPoint.y = cgo.Transform.position.y - 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | атакует entity: {pe.GetInternalId()} снизу");
                        continue;
                    }
                }

                if (!atackPlayer && UnityEngine.Random.value < 0.7f)
                {
                    skip = true;
                    Debug.Log($"entity: {e.GetInternalId()} | решил пропустить ход");
                }

                if (!atackPlayer && !skip)
                {
                    switch (UnityEngine.Random.Range(0, 4))
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

                c1.PhaseEnd = true;
                c2.SkipTurn = skip;
                var icom = new InputComOneStep(goalPosition);
                c2.InputCommand = icom;
            }
        }
    }
}
