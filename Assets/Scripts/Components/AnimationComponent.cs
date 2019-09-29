using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class AnimationComponent : IEcsAutoResetComponent
    {
        public Animator animator = null;

        void IEcsAutoResetComponent.Reset()
        {
            animator = null;
        }
    }
}