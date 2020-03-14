using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ���������� ��������� ���� � ���� ��������
    /// </summary>
    
    sealed class ActionAnimationSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<ActionAnimationComponent, GameObjectComponent> _animationEntities = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _animationEntities)
            {
                ref var e = ref _animationEntities.Entities[i];
                var c1 = _animationEntities.Get1[i];
                var c2 = _animationEntities.Get2[i];

                if (!c1.Run)
                {
                    c2.GO.Animator.SetTrigger(c1.Animation.ToString());
                    c1.Run = true;
                }
                else if (!c2.GO.Animator.GetBool(AnimatorField.ActionRun.ToString()))
                {
                    e.Unset<ActionAnimationComponent>();
                }
            }
        }
    }
}