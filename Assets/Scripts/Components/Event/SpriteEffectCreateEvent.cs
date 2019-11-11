using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [EcsOneFrame]
    sealed class SpriteEffectCreateEvent : IEcsAutoResetComponent
    {
        public float LifeTime = 0f;
        public SpriteEffect SpriteEffect = SpriteEffect.None;
        public Vector2 Position = Vector2.zero;

        void IEcsAutoResetComponent.Reset()
        {
            LifeTime = 0f;
            SpriteEffect = SpriteEffect.None;
        }
    }
}