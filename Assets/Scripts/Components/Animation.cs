using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class Animation : IEcsAutoResetComponent
    {
        public Animator animator;

        void IEcsAutoResetComponent.Reset()
        {
            animator = null;
        }
    }
}