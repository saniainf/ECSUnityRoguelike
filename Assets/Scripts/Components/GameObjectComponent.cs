using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectComponent : IEcsAutoReset
    {
        public Transform Transform = null;
        public PrefabComponentsShortcut GOcomps = null;

        void IEcsAutoReset.Reset()
        {
            GOcomps = null;
            Transform = null;
        }
    }
}