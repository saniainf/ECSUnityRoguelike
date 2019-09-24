using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class Position : IEcsAutoResetComponent
    {
        public Coords Coords;
        public Transform Transform;
        public Rigidbody2D Rigidbody;

        void IEcsAutoResetComponent.Reset()
        {
            Transform = null;
            Rigidbody = null;
        }
    }
}