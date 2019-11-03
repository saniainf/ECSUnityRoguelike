using Leopotam.Ecs;

namespace Client
{
    sealed class ActionAnimationComponent:IEcsAutoResetComponent
    {
        public AnimationTriger Animation = AnimationTriger.None;
        public bool Run = false;

        void IEcsAutoResetComponent.Reset()
        {
            Run = false;
        }
    }
}