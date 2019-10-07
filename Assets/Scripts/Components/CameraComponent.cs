using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    sealed class CameraComponent : IEcsAutoResetComponent
    {
        public Transform Transform = null;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
        }
    }
}