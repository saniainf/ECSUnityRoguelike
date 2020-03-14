using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class GameObjectComponent : IEcsAutoReset
    {
        public Transform Transform = null;
        public PrefabComponentsShortcut GO = null;

        void IEcsAutoReset.Reset()
        {
            GO = null;
            Transform = null;
        }
    }
}