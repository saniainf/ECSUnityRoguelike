using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class SpriteEffectComponent : IEcsAutoReset
    {
        public SpriteEffect SpriteEffect = SpriteEffect.None;
        public Vector2 Position = Vector2.zero;
        public float LifeTime = 0f;
        public bool Run = false;

        void IEcsAutoReset.Reset()
        {
            SpriteEffect = SpriteEffect.None;
            Position = Vector2.zero;
            LifeTime = 0f;
            Run = false;
        }
    }
}