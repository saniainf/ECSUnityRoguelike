using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    [EcsOneFrame]
    sealed class SpriteEffectCreateEvent : IEcsAutoResetComponent
    {
        public float LifeTime = 0f;
        public SpriteEffect SpriteEffect = SpriteEffect.NONE;
        public Vector2Int Position = Vector2Int.zero;

        void IEcsAutoResetComponent.Reset()
        {
            LifeTime = 0f;
            SpriteEffect = SpriteEffect.NONE;
        }
    }
}