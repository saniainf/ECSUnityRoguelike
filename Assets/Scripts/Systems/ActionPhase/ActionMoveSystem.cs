using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// управление передвижением чара в фазу действия
    /// </summary>

    sealed class ActionMoveSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionMoveComponent, GameObjectComponent> _moveEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _moveEntities)
            {
                ref var e = ref _moveEntities.Entities[i];
                var c1 = _moveEntities.Get1[i];
                var c2 = _moveEntities.Get2[i];

                if (!c1.Run)
                {
                    c1.StartPosition = c2.GOcomps.Rigidbody.position;
                    c1.Run = true;
                }

                if (c1.Run)
                {
                    if (c1.SqrDistance > (c1.DestroyDistance * c1.DestroyDistance))
                    {
                        Debug.Log($"{c1.SqrDistance}    {c1.DestroyDistance * c1.DestroyDistance}");
                        e.Unset<ActionMoveComponent>();
                    }
                }

                if (c1.GoalInt != Vector2Int.zero)
                {
                    var nextPosition = Vector2.MoveTowards(c2.GOcomps.Rigidbody.position, c1.GoalInt, c1.Speed * Time.deltaTime);
                    c2.GOcomps.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.GOcomps.Rigidbody.position - c1.StartPosition).sqrMagnitude;

                    float sqrDistanceToGoal = (c2.GOcomps.Rigidbody.position - c1.GoalInt).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c2.GOcomps.Rigidbody.position = c1.GoalInt;

                        e.Unset<ActionMoveComponent>();
                    }
                }

                else if (c1.GoalFloat != Vector2.zero)
                {
                    var nextPosition = Vector2.MoveTowards(c2.GOcomps.Rigidbody.position, c1.GoalFloat, c1.Speed * Time.deltaTime);
                    c2.GOcomps.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.GOcomps.Rigidbody.position - c1.StartPosition).sqrMagnitude;

                    float sqrDistanceToGoal = (c2.GOcomps.Rigidbody.position - c1.GoalFloat).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c2.GOcomps.Rigidbody.position = c1.GoalFloat;
                        c2.Transform.position = c1.GoalFloat;

                        e.Unset<ActionMoveComponent>();
                    }
                }

                else if (c1.GoalDirection != Vector2.zero)
                {
                    var nextPosition = c2.GOcomps.Rigidbody.position + (c1.GoalDirection * (c1.Speed * Time.deltaTime));
                    c2.GOcomps.Rigidbody.MovePosition(nextPosition);
                    c1.SqrDistance = (c2.GOcomps.Rigidbody.position - c1.StartPosition).sqrMagnitude;
                }
            }
        }
    }
}