using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectComponent : IEcsAutoReset
    {
        public Transform Transform = null;
        public PrefabComponentsShortcut GObj = null;

        void IEcsAutoReset.Reset()
        {
            GObj = null;
            Transform = null;
        }
    }
}