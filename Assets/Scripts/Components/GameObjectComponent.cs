using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectComponent : IEcsAutoResetComponent
    {
        public Transform Transform = null;
        public PrefabComponentsShortcut GOcomps = null;

        void IEcsAutoResetComponent.Reset()
        {
            GOcomps = null;
            Transform = null;
        }
    }
}