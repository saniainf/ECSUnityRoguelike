using Leopotam.Ecs;

namespace Client
{
    sealed class SpriteEffectComponent : IEcsAutoResetComponent
    {
        public float LifeTime = 0f;

        void IEcsAutoResetComponent.Reset()
        {
            LifeTime = 0f;
        }
    }
}