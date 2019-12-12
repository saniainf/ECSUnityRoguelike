using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class ProjectileSystem : IEcsRunSystem
    {
        const float SPEED = 7f;

        readonly EcsWorld _world = null;

        readonly EcsFilter<ProjectileComponent, GameObjectComponent> _projectileEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _projectileEntities)
            {
                ref var e = ref _projectileEntities.Entities[i];
                var c1 = _projectileEntities.Get1[i];
                var c2 = _projectileEntities.Get2[i];

                if (!c1.Run)
                {
                    c1.StartPosition = c2.GObj.Rigidbody.position;
                    c1.Run = true;
                }

                if (c1.Run)
                {
                    var nextPosition = Vector2.MoveTowards(c2.GObj.Rigidbody.position, c1.GoalPosition, SPEED * Time.deltaTime);
                    c2.GObj.Rigidbody.MovePosition(nextPosition);

                    float sqrDistanceToGoal = (c2.GObj.Rigidbody.position - c1.GoalPosition).sqrMagnitude;
                    if (sqrDistanceToGoal < float.Epsilon)
                    {
                        c1.Weapon.Behaviour.OnAtack(c1.Caster, c1.Target);

                        e.RLDestoryGO();
                    }
                }
            }
        }
    }
}