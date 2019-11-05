using Leopotam.Ecs;

namespace Client
{
    sealed class ActionAnimationComponent:IEcsAutoResetComponent
    {
        public AnimatorField Animation = AnimatorField.None;
        public bool Run = false;

        void IEcsAutoResetComponent.Reset()
        {
            Run = false;
        }
    }
}