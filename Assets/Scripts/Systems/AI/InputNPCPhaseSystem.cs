using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ���� npc, ����� ��� ��� � ���� �����
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
                EcsEntity target = EcsEntity.Null;

                Debug.Log($"entity: {e.GetInternalId()} | ������ ��� ������");

                foreach (var j in _playerEntities)
                {
                    var pc1 = _playerEntities.Get1[j];
                    var pe = _playerEntities.Entities[j];

                    Debug.Log($"entity: {e.GetInternalId()} | ��������� ��� �� ����� entity: {pe.GetInternalId()}");

                    Vector2 checkPoint = cgo.Transform.position;
                    checkPoint.x = cgo.Transform.position.x - 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        target = pe;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | ������� entity: {pe.GetInternalId()} �����");
                        continue;
                    }

                    checkPoint.x = cgo.Transform.position.x + 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        target = pe;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | ������� entity: {pe.GetInternalId()} ������");
                        continue;
                    }

                    checkPoint = cgo.Transform.position;
                    checkPoint.y = cgo.Transform.position.y + 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        target = pe;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | ������� entity: {pe.GetInternalId()} ������");
                        continue;
                    }

                    checkPoint.y = cgo.Transform.position.y - 1;
                    if (pc1.GObj.Collider.OverlapPoint(checkPoint))
                    {
                        goalPosition = checkPoint;
                        target = pe;
                        atackPlayer = true;
                        Debug.Log($"entity: {e.GetInternalId()} | ������� entity: {pe.GetInternalId()} �����");
                        continue;
                    }
                }

                if (atackPlayer)
                {
                    c2.InputCommand = new InputComAtackCloseCell(target, goalPosition);
                    c1.PhaseEnd = true;
                    continue;
                }

                if (!atackPlayer && UnityEngine.Random.value < 0.7f)
                {
                    Debug.Log($"entity: {e.GetInternalId()} | ����� ���������� ���");
                    c2.InputCommand = new InputComEmpty();
                    c1.PhaseEnd = true;
                    continue;
                }

                if (!atackPlayer && !skip)
                {
                    float v = 0f;
                    float h = 0f;

                    switch (UnityEngine.Random.Range(0, 4))
                    {
                        case 0:
                            h -= 1;
                            break;
                        case 1:
                            h += 1;
                            break;
                        case 2:
                            v -= 1;
                            break;
                        case 3:
                            v += 1;
                            break;
                        default:
                            skip = true;
                            break;
                    }
                    Debug.Log($"entity: {e.GetInternalId()} | ������");

                    c1.PhaseEnd = true;
                    c2.InputCommand = new InputComOneStepOnDirection(h, v);
                }
            }
        }
    }
}
