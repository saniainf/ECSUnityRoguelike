using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EffectComponent : IEcsAutoReset
    {
        public float Duration = 0f;
        public bool Run = false;

        void IEcsAutoReset.Reset()
        {
            Duration = 0f;
            Run = false;
        }
    }
}